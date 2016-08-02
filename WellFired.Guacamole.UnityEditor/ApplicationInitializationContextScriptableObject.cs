using System;
using UnityEngine;

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
			get 
			{
				if (_mainContent == null) 
				{
					_mainContent = Type.GetType(_mainContentString);
				}

				return _mainContent; 
			}
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

		public void OnEnable() { hideFlags = HideFlags.HideAndDontSave; }

		#region IInitializationContext implementation
		public void ValidateSetup()
        {
            Debug.Log("1.5");
            if (MainContent == null)
				throw new InitializationContextInvalid();
		}
		#endregion
	}

	public class ApplicationInitializationContext
	{
		public Type MainContent
		{
			get { return ScriptableObject.MainContent; }
			set { ScriptableObject.MainContent = value; }
		}

	    // ReSharper disable once InconsistentNaming
		public UIRect UIRect
		{
			get { return ScriptableObject.UIRect; }
			set { ScriptableObject.UIRect = value; }
		}

		public UISize MinSize
		{
			get { return ScriptableObject.MinSize; }
			set { ScriptableObject.MinSize = value; }
		}

		public UISize MaxSize
		{
			get { return ScriptableObject.MaxSize; }
			set { ScriptableObject.MaxSize = value; }
		}
			
		public string Title
		{
			get { return ScriptableObject.Title; }
			set { ScriptableObject.Title = value; }
		}
		
		internal ApplicationInitializationContextScriptableObject ScriptableObject
		{
			get; private set;
		}

	    public ApplicationInitializationContext()
		{
			ScriptableObject = UnityEngine.ScriptableObject.CreateInstance<ApplicationInitializationContextScriptableObject>();
		}
	}
}