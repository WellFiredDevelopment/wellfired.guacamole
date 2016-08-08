using System;
using System.ComponentModel;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.InitializationContext;
using WellFired.Guacamole.Renderer;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Unity.Editor
{
    public class GuacamoleWindow : EditorWindow, IWindow
    {
        [SerializeField] private ApplicationInitializationContextScriptableObject _applicationInitializationContextScriptableObject;
        [SerializeField] private Window _window;

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
                if(_window == null)
                    ResetForSomeReason();

                return _window;
            }
        }

        public void Launch(IInitializationContext initializationContext)
        {
            initializationContext.ValidateSetup();

            ApplicationInitializationContextScriptableObject =
                initializationContext as ApplicationInitializationContextScriptableObject;

            if(ApplicationInitializationContextScriptableObject == null)
                throw new InitializationContextNull();

            Title = ApplicationInitializationContextScriptableObject.Title;
            Rect = ApplicationInitializationContextScriptableObject.UIRect;
            MinSize = ApplicationInitializationContextScriptableObject.MinSize;
            MaxSize = ApplicationInitializationContextScriptableObject.MaxSize;

            if(MaxSize == UISize.Min)
                MaxSize = new UISize(100000, 100000);

            ResetForSomeReason();
        }


        [UsedImplicitly, Obfuscation(Feature = "renaming")]
        public void OnEnable()
        {
            UnityEngine.Debug.Log("Enable");
            Guacamole.Diagnostics.Logger.RegisterLogger(Diagnostics.Logger.UnityLogger);
        }

        [UsedImplicitly, Obfuscation(Feature = "renaming")]
        public void OnDisable()
        {
            UnityEngine.Debug.Log("Disable");
            Guacamole.Diagnostics.Logger.UnregisterLogger(Diagnostics.Logger.UnityLogger);
        }

        [UsedImplicitly, Obfuscation(Feature = "renaming")]
        // ReSharper disable once InconsistentNaming
        public void OnGUI()
        {
            if(Event.current.type == EventType.Layout)
            {
                try
                {
                    var layoutRect = Rect;
                    layoutRect.Location = UILocation.Min;
                    MainContent.Layout(layoutRect);
                }
                catch(Exception e)
                {
                    Debug.Log("Exception was thrown whilst performing Layout : " + e);
                }
            }

            try
            {
                MainContent.Render(Rect);
            }
            catch(Exception e)
            {
                Debug.Log("Exception was thrown whilst performing Repaint : " + e);
            }
        }

        private void ResetForSomeReason()
        {
            NativeRendererHelper.LaunchedAssembly = Assembly.GetExecutingAssembly();

            var contentType = ApplicationInitializationContextScriptableObject.MainContent;

            var constructorInfo = contentType.GetConstructor(new[] { typeof(INotifyPropertyChanged) });
            if(constructorInfo != null)
                _window = (Window) constructorInfo.Invoke(new object[] {ApplicationInitializationContextScriptableObject.PersistantData});
            else
            {
                var paramLessCsonstructorInfo = contentType.GetConstructor(new Type[] { });
                if (paramLessCsonstructorInfo != null)
                    _window = (Window)paramLessCsonstructorInfo.Invoke(new object[] { });
            }

            if(_window == null)
                throw new GuacamoleWindowCantBeCreated();
        }
    }
}