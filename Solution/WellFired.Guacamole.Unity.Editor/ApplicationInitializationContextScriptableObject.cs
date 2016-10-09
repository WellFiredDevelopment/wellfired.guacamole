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
		[SerializeField] private Type _mainContent;
		[SerializeField] private string _mainContentString;

		[SerializeField] private UISize _maxSize;

		[SerializeField] private UISize _minSize;

		[SerializeField] private ObservableScriptableObject _persistantData;

		[SerializeField] private string _title;

		[SerializeField] private UIRect _uirect;

		public Type MainContent
		{
			get { return _mainContent ?? (_mainContent = Type.GetType(_mainContentString)); }
			set
			{
				_mainContent = value;
				_mainContentString = _mainContent.AssemblyQualifiedName;
			}
		}

		// ReSharper disable once InconsistentNaming
		public UIRect UIRect
		{
			get { return _uirect; }
			set { _uirect = value; }
		}

		public UISize MinSize
		{
			get { return _minSize; }
			set { _minSize = value; }
		}

		public UISize MaxSize
		{
			get { return _maxSize; }
			set { _maxSize = value; }
		}

		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		public ObservableScriptableObject PersistantData
		{
			get { return _persistantData; }
			set { _persistantData = value; }
		}

		#region IInitializationContext implementation

		public void ValidateSetup()
		{
			if (MainContent == null)
				throw new InitializationContextInvalid();
		}

		#endregion

		[UsedImplicitly]
		[Obfuscation(Feature = "renaming")]
		public void OnEnable()
		{
			hideFlags = HideFlags.HideAndDontSave;
		}
	}

	public class ApplicationInitializationContext
	{
		public ApplicationInitializationContext()
		{
			ScriptableObject = UnityEngine.ScriptableObject.CreateInstance<ApplicationInitializationContextScriptableObject>();
		}

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

		internal ApplicationInitializationContextScriptableObject ScriptableObject { get; }
	}
}