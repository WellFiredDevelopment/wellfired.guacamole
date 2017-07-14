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
			if(ApplicationInitializationContextScriptableObject != null)
				Logger.RegisterLogger(ApplicationInitializationContextScriptableObject.Logger);
			EditorApplication.update += Update;
		}

		[UsedImplicitly]
		[Obfuscation(Feature = "renaming")]
		public void OnDisable()
		{
			if(ApplicationInitializationContextScriptableObject != null)
				Logger.UnregisterLogger(ApplicationInitializationContextScriptableObject.Logger);
			// ReSharper disable once DelegateSubtraction
			EditorApplication.update -= Update;
		}

		private void Update()
		{
			if (_exception != null)
			{
				Close();
				EditorUtility.DisplayDialog(
					"Exception was thrown",
					$"The window has thrown an exception, the error is \n\n {_exception.Message} \n\n callstack was \n\n {_exception.StackTrace}",
					"Close");
				return;
			}
			
			Repaint();
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
					var layoutRect = Rect;
					layoutRect.Location = UILocation.Min;
					MainContent.Layout(layoutRect);
				}
				catch (Exception e)
				{
					_exception = e;
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

		public void RaiseEventFor(string controlId, IEvent raisedEvent)
		{
			MainContent.RaiseEventFor(controlId, raisedEvent);
		}
	}
}