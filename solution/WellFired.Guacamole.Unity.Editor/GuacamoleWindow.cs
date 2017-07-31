using System;
using System.ComponentModel;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Event;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.InitializationContext;
using WellFired.Guacamole.Renderer;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Views;
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
		private const float MaxLayoutInterval = 1.0f / 30.0f;

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
			Rect = ApplicationInitializationContextScriptableObject.UIRect;
			MinSize = ApplicationInitializationContextScriptableObject.MinSize;
			MaxSize = ApplicationInitializationContextScriptableObject.MaxSize;

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

		private void DisplayUserError(System.Exception exception)
		{
			var targetInvocationException = exception as TargetInvocationException;
			if (targetInvocationException != null)
				exception = targetInvocationException.InnerException;
			
			var facingException = exception as GuacamoleUserFacingException;
			EditorUtility.DisplayDialog(
				"Guacamole Crashed. :(",
				facingException != null
					? $"Your Guacamole window has crashed with the error : \n\n{facingException.UserFacingError()}"
					: $"The window has thrown an exception, the error is \n\n{_exception.Message} \n\ncallstack was \n\n{_exception.StackTrace}",
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
				catch (System.Exception e)
				{
					_exception = e;
					return;
				}

			try
			{
				MainContent.Render(Rect);
			}
			catch (System.Exception e)
			{
				_exception = e;
			}
		}

		private void ResetForSomeReason()
		{
			NativeRendererHelper.LaunchedAssembly = Assembly.GetExecutingAssembly();

			var contentType = ApplicationInitializationContextScriptableObject.MainContent;

			var constructorInfo = contentType.GetConstructor(new[] {typeof(Guacamole.Diagnostics.ILogger), typeof(INotifyPropertyChanged)});
			if (constructorInfo != null)
				_window = (Window) constructorInfo.Invoke(new object[]
				{
					ApplicationInitializationContextScriptableObject.Logger,
					ApplicationInitializationContextScriptableObject.PersistantData
				});
			else
			{
				var paramLessCsonstructorInfo = contentType.GetConstructor(new[] {typeof(ILogger)});
				if (paramLessCsonstructorInfo != null)
					_window = (Window) paramLessCsonstructorInfo.Invoke(new object[]
					{
						ApplicationInitializationContextScriptableObject.Logger
					});
			}

			if (_window == null)
				throw new GuacamoleWindowCantBeCreated();
		}

		public bool MatchesMainContent(Type mainContent)
		{
			return ApplicationInitializationContextScriptableObject.MainContent == mainContent;
		}
	}
}