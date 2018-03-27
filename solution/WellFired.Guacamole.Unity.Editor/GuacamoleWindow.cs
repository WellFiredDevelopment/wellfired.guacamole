using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Data;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.DataStorage.Data.Serialization;
using WellFired.Guacamole.DataStorage.Storages;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.InitializationContext;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Renderer;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.Platform;
using WellFired.Guacamole.Views;
using WellFired.Guacamole.WindowContext;
using Debug = System.Diagnostics.Debug;
using Logger = WellFired.Guacamole.Diagnostics.Logger;

namespace WellFired.Guacamole.Unity.Editor
{
	[UsedImplicitly]
	public class GuacamoleWindow : EditorWindow, IWindow
	{
		private const string GuamoleApplicationName = "Guacamole";

		[SerializeField] private Window _window;

		private InitializationContext _initializationContext;
		private ContextStorage _contextStorage;
		private CancellationTokenSource _tokenSource;

		private Exception _exception;
		private float _prevLayoutTime;
		private const float MaxLayoutInterval = 1.0f / 90.0f; // Clamp the update at 90 fps for silky smooth lists.

		public bool CloseAfterNextUpdate { private get; set; }

		public string Title
		{
			get => titleContent.text;
			set => titleContent.text = value;
		}

		public UIRect Rect
		{
			get => position.ToUIRect();
			set => position = value.ToUnityRect();
		}

		public UISize MinSize
		{
			get => minSize.ToUISize();
			set => minSize = value.ToUnityVector2();
		}

		public UISize MaxSize
		{
			get => maxSize.ToUISize();
			set => maxSize = value.ToUnityVector2();
		}

		public bool AllowMultiple => _initializationContext.AllowMultiple;

		public Window MainContent
		{
			get
			{
				if (_window == null)
					ResetForSomeReason();

				return _window;
			}
		}

		public void Launch(IInitializationContext initializationContext)
		{
			_initializationContext = initializationContext as InitializationContext;
			if (_initializationContext == null)
				throw new InitializationContextNull();

			initializationContext.ValidateSetup();

			Title = _initializationContext.Title;
			MinSize = _initializationContext.MinSize;
			MaxSize = _initializationContext.MaxSize;
			Rect = _initializationContext.UIRect;

			if (MaxSize == UISize.Min)
				MaxSize = UISize.Of(100000);

			ResetForSomeReason();
		}

		[UsedImplicitly]
		[Obfuscation(Feature = "renaming")]
		public void OnEnable()
		{
			_contextStorage = new ContextStorage(
				new ComputerDataStorage(GuamoleApplicationName, GuamoleApplicationName, Platforms.Platform.Unity),
				new JSONSerializer(new ContextCustomSerialization())
			);

			EditorApplication.update += Update;
			
			//Ideally, context should be saved only if the window was opened when Unity Editor quitted.
			//I did not find a way to detect that. So instead, we always save the context when a window is disabled.
			//When a windows is enable for enough time for Unity to load all the windows that was disabled when it quits,
			//we can clean up the contexts.
			_tokenSource = new CancellationTokenSource();
			TaskEx.Run(() => DelayContextStorageCleanup(_tokenSource.Token), _tokenSource.Token);
		}
		
		private async Task DelayContextStorageCleanup(CancellationToken cancellationToken)
		{
			await TaskEx.Delay(10000, cancellationToken);

			if (cancellationToken.IsCancellationRequested)
				return;
			
			_contextStorage.CleanUpStoredContexts();
		}

		[UsedImplicitly]
		[Obfuscation(Feature = "renaming")]
		public void OnDisable()
		{
			try
			{
				if (_window?.WindowCloseCommand != null && _window.WindowCloseCommand.CanExecute)
				{
					_window.WindowCloseCommand.Execute();
					_window.WindowCloseCommand.CanExecute = false;
				}

				if (_initializationContext == null) return;
				
				_initializationContext.UIRect = Rect;
				_tokenSource.Cancel();
				_contextStorage.Save(name, _initializationContext.Context);
			}
			finally
			{
				// ReSharper disable once DelegateSubtraction
				EditorApplication.update -= Update;
			}
		}

		private void Update()
		{
			if (CloseAfterNextUpdate)
			{
				Close();
				return;
			}

			if (_exception != null)
			{
				Close();
				DisplayUserError(_exception);
				return;
			}

			Repaint();
		}

		private void DisplayUserError(Exception exception)
		{
			if (exception is TargetInvocationException targetInvocationException)
			{
				exception = targetInvocationException.InnerException;
				Debug.Assert(exception != null, nameof(exception) + " != null");
			}

			if (exception is GuacamoleUserFacingException facingException)
			{
				EditorUtility.DisplayDialog(
					"Guacamole Crashed. :(",
					$"Your Guacamole window has crashed with the error : \n\n{facingException.UserFacingError()}",
					"Close");
			}
			
			Logger.LogError("Exception occurred : " + _exception.Message + "\n" + _exception.StackTrace);
		}

		[UsedImplicitly]
		[Obfuscation(Feature = "renaming")]
		public void OnGUI()
		{
			if (_exception != null)
				return;

			if (UnityEngine.Event.current.type == EventType.Layout)
				try
				{
					if (Time.realtimeSinceStartup - _prevLayoutTime > MaxLayoutInterval)
					{
						var layoutRect = Rect;
						layoutRect.Location = UILocation.Min;
						MainContent.Layout(layoutRect);
						_prevLayoutTime = Time.realtimeSinceStartup;
					}
				}
				catch (Exception e)
				{
					_exception = e;
					return;
				}
			try
			{
				MainContent.Render(Rect);
			}
			catch (Exception e)
			{
				_exception = e;
			}
		}

		private void ResetForSomeReason()
		{
			NativeRendererHelper.LaunchedAssembly = Assembly.GetExecutingAssembly();

			if (_initializationContext == null)
			{
				_initializationContext = new InitializationContext(_contextStorage.Load(name));
				_contextStorage.Delete(name);

				Rect = _initializationContext.UIRect;
			}

			var contentType = _initializationContext.MainContentType;
			var viewModelType = _initializationContext.MainViewModelType;

			var platformProvider = new UnityPlatformProvider(_initializationContext.ApplicationName, _initializationContext.CompanyName);
			var logger = new Diagnostics.Logger();

			var constructorInfo = contentType?.GetConstructor(new[] {typeof(Guacamole.Diagnostics.ILogger), typeof(INotifyPropertyChanged), typeof(IPlatformProvider)});
			if (constructorInfo != null)
				_window = (Window) constructorInfo.Invoke(new object[] {
					logger,
					_initializationContext.PersistantData,
					platformProvider
				});
			else
			{
				var paramLessCsonstructorInfo = contentType?.GetConstructor(new[] {typeof(ILogger), typeof(IPlatformProvider)});
				if (paramLessCsonstructorInfo != null)
					_window = (Window) paramLessCsonstructorInfo.Invoke(new object[] {
						logger,
						platformProvider
					});
			}

			if (viewModelType != default(Type))
			{
				var viewModel = (IBasicViewModel) Activator.CreateInstance(viewModelType);
				viewModel.Inject(
					logger,
					(INotifyPropertyChanged) _initializationContext.PersistantData,
					platformProvider);

				_window.BindingContext = viewModel;
			}

			if (_window == null)
				throw new GuacamoleWindowCantBeCreated();

			name = _window.Id;
		}

		public bool MatchesMainContent(Type mainContent)
		{
			return _initializationContext.MainContentType == mainContent;
		}
	}
}