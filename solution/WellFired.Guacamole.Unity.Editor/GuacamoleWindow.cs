using System;
using System.ComponentModel;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.InitializationContext;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.Renderer;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.Platform;
using WellFired.Guacamole.Views;
using Debug = System.Diagnostics.Debug;
using Logger = WellFired.Guacamole.Diagnostics.Logger;

namespace WellFired.Guacamole.Unity.Editor
{
	[UsedImplicitly]
	public class GuacamoleWindow : EditorWindow, IWindow
	{
		[SerializeField] private ApplicationInitializationContextScriptableObject _applicationInitializationContextScriptableObject;
		[SerializeField] private Window _window;

		private Exception _exception;
		private float _prevLayoutTime;
		private const float MaxLayoutInterval = 1.0f / 90.0f; // Clamp the update at 90 fps for silky smooth lists.

		public bool CloseAfterNextUpdate { get; set; }

		private ApplicationInitializationContextScriptableObject ApplicationInitializationContextScriptableObject
		{
			get { return _applicationInitializationContextScriptableObject; }
			set { _applicationInitializationContextScriptableObject = value; }
		}

		public string Title
		{
			get { return titleContent.text; }
			set { titleContent.text = value; }
		}

		public UIRect Rect
		{
			get { return position.ToUIRect(); }
			set { position = value.ToUnityRect(); }
		}

		public UISize MinSize
		{
			get { return minSize.ToUISize(); }
			set { minSize = value.ToUnityVector2(); }
		}

		public UISize MaxSize
		{
			get { return maxSize.ToUISize(); }
			set { maxSize = value.ToUnityVector2(); }
		}

		public bool AllowMultiple => _applicationInitializationContextScriptableObject.AllowMultiple;

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
			initializationContext.ValidateSetup();

			ApplicationInitializationContextScriptableObject = initializationContext as ApplicationInitializationContextScriptableObject;

			if (ApplicationInitializationContextScriptableObject == null)
				throw new InitializationContextNull();

			Title = ApplicationInitializationContextScriptableObject.Title;
			MinSize = ApplicationInitializationContextScriptableObject.MinSize;
			MaxSize = ApplicationInitializationContextScriptableObject.MaxSize;
			Rect = ApplicationInitializationContextScriptableObject.UIRect;

			if (MaxSize == UISize.Min)
				MaxSize = UISize.Of(100000);

			ResetForSomeReason();
		}

		[UsedImplicitly]
		[Obfuscation(Feature = "renaming")]
		public void OnEnable()
		{
			Logger.RegisterLogger(Diagnostics.Logger.UnityLogger);
			EditorApplication.update += Update;
		}

		[UsedImplicitly]
		[Obfuscation(Feature = "renaming")]
		public void OnDisable()
		{
			Logger.UnregisterLogger(Diagnostics.Logger.UnityLogger);
			// ReSharper disable once DelegateSubtraction
			EditorApplication.update -= Update;
		}

		private void Update()
		{
			if (CloseAfterNextUpdate)
			{
				CloseWindow();
				return;
			}
			
			if (_exception != null)
			{
				CloseWindow();
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
			
			var facingException = exception as GuacamoleUserFacingException;
			EditorUtility.DisplayDialog(
				"Guacamole Crashed. :(",
				facingException != null
					? $"Your Guacamole window has crashed with the error : \n\n{facingException.UserFacingError()}"
					: $"The window has thrown an exception, the error is \n\n{exception.Message} \n\ncallstack was \n\n{exception.StackTrace}",
				"Close");
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

			var contentType = ApplicationInitializationContextScriptableObject.MainContent;
			var viewModelType = ApplicationInitializationContextScriptableObject.MainViewModel;
			
			var platformProvider = new UnityPlatformProvider(_applicationInitializationContextScriptableObject.ApplicationName);

			var constructorInfo = contentType.GetConstructor(new[] {typeof(Guacamole.Diagnostics.ILogger), typeof(INotifyPropertyChanged), typeof(IPlatformProvider)});
			if (constructorInfo != null)
				_window = (Window) constructorInfo.Invoke(new object[]
				{
					ApplicationInitializationContextScriptableObject.Logger,
					ApplicationInitializationContextScriptableObject.PersistantData,
					platformProvider
				});
			else
			{
				var paramLessCsonstructorInfo = contentType.GetConstructor(new[] {typeof(ILogger), typeof(IPlatformProvider)});
				if (paramLessCsonstructorInfo != null)
					_window = (Window) paramLessCsonstructorInfo.Invoke(new object[]
					{
						ApplicationInitializationContextScriptableObject.Logger,
						platformProvider
					});
			}

			if (viewModelType != default(Type))
			{
				var viewModel = (IBasicViewModel)Activator.CreateInstance(viewModelType);
				viewModel.Inject(
					ApplicationInitializationContextScriptableObject.Logger,
					(INotifyPropertyChanged)ApplicationInitializationContextScriptableObject.PersistantData,
					platformProvider);

				_window.BindingContext = viewModel;
			}

			if (_window == null)
				throw new GuacamoleWindowCantBeCreated();
		}
		
		private void CloseWindow()
		{
			if (_window.WindowCloseCommand != null && _window.WindowCloseCommand.CanExecute)
			{
				_window.WindowCloseCommand.Execute();
			}
			
			Close();
		}

		public bool MatchesMainContent(Type mainContent)
		{
			return ApplicationInitializationContextScriptableObject.MainContent == mainContent;
		}
	}
}