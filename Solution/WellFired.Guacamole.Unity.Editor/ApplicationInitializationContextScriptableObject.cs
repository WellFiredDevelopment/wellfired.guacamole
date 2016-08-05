using System;
using System.Reflection;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.InitializationContext;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.DataBinding;

namespace WellFired.Guacamole.Unity.Editor
{
	[Serializable]
	internal class ApplicationInitializationContextScriptableObject : ScriptableObject, IInitializationContext
	{
		[SerializeField]
		private string _mainContentString;

		[SerializeField]
		private Type _mainContent;
		public Type MainContent
		{
			get { return _mainContent ?? (_mainContent = Type.GetType(_mainContentString)); }
		    set 
			{ 
				_mainContent = value; 
				_mainContentString = _mainContent.AssemblyQualifiedName;
			}
		}

		[SerializeField]
		private UIRect _uirect;
	    // ReSharper disable once InconsistentNaming
		public UIRect UIRect
		{
			get { return _uirect; }
			set { _uirect = value; }
		}

		[SerializeField]
		private UISize _minSize;
		public UISize MinSize
		{
			get { return _minSize; }
			set { _minSize = value; }
		}

		[SerializeField]
		private UISize _maxSize;
		public UISize MaxSize
		{
			get { return _maxSize; }
			set { _maxSize = value; }
		}

		[SerializeField]
		private string _title;
		public string Title
		{
			get { return _title; }
			set { _title = value; }
        }

	    [SerializeField]
        private ObservableScriptableObject _persistantData;
	    public ObservableScriptableObject PersistantData
        {
            get { return _persistantData; }
            set { _persistantData = value; }
        }

        [UsedImplicitly, Obfuscation(Feature = "renaming")]
        public void OnEnable() { hideFlags = HideFlags.HideAndDontSave; }

		#region IInitializationContext implementation
		public void ValidateSetup()
        {
            if (MainContent == null)
				throw new InitializationContextInvalid();
		}
		#endregion
	}

	public class ApplicationInitializationContext
	{
        [PublicAPI]
		public Type MainContent
		{
			get { return ScriptableObject.MainContent; }
			set { ScriptableObject.MainContent = value; }
		}

        // ReSharper disable once InconsistentNaming
        [PublicAPI]
        public UIRect UIRect
		{
			get { return ScriptableObject.UIRect; }
			set { ScriptableObject.UIRect = value; }
		}

        [PublicAPI]
        public UISize MinSize
		{
			get { return ScriptableObject.MinSize; }
			set { ScriptableObject.MinSize = value; }
		}

        [PublicAPI]
        public UISize MaxSize
		{
			get { return ScriptableObject.MaxSize; }
			set { ScriptableObject.MaxSize = value; }
		}

        [PublicAPI]
        public string Title
		{
			get { return ScriptableObject.Title; }
			set { ScriptableObject.Title = value; }
        }

        [PublicAPI]
        public ObservableScriptableObject PersistantData
        {
            get { return ScriptableObject.PersistantData; }
            set { ScriptableObject.PersistantData = value; }
        }

        internal ApplicationInitializationContextScriptableObject ScriptableObject
		{ get; }

	    public ApplicationInitializationContext()
		{
			ScriptableObject = UnityEngine.ScriptableObject.CreateInstance<ApplicationInitializationContextScriptableObject>();
		}
	}
}