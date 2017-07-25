using System;
using System.Reflection;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.InitializationContext;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.DataBinding;
using Logger = WellFired.Guacamole.Unity.Editor.Diagnostics.Logger;
using ILogger = WellFired.Guacamole.Diagnostics.ILogger;

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
		[SerializeField] private bool _allowMultiple;
		private ILogger _logger;

		public Type MainContent
		{
			get { return _mainContent ?? (_mainContent = Type.GetType(_mainContentString)); }
			set
			{
				_mainContent = value;
				_mainContentString = _mainContent.AssemblyQualifiedName;
			}
		}

		public ILogger Logger => _logger ?? (_logger = new Logger());

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

		public bool AllowMultiple
		{
			get { return _allowMultiple; }
			set { _allowMultiple = value; }
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

		[PublicAPI]
		public bool AllowMultiple
		{
			get { return ScriptableObject.AllowMultiple; }
			set { ScriptableObject.AllowMultiple = value; }
		}

		internal ApplicationInitializationContextScriptableObject ScriptableObject { get; }
	}
}