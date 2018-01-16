using System;
using UnityEngine;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.InitializationContext;
using WellFired.Guacamole.WindowContext;

namespace WellFired.Guacamole.Unity.Editor
{
	public class InitializationContext : IInitializationContext
	{
		private readonly Context _context;
		private Type _mainContentType;
		private Type _mainViewModelType;

		public Type MainContentType
		{
			get
			{
				if (_mainContentType == null && !string.IsNullOrEmpty(Context.MainContentTypeString))
					_mainContentType = Type.GetType(Context.MainContentTypeString);

				return _mainContentType;
			}
		}
		
		public Type MainViewModelType
		{
			get
			{
				if (_mainViewModelType == null && !string.IsNullOrEmpty(Context.MainViewModelTypeString))
					_mainViewModelType = Type.GetType(Context.MainViewModelTypeString);

				return _mainViewModelType;
			}
		}

		public UISize MaxSize => Context.MaxSize;

		public UISize MinSize => Context.MinSize;

		public string Title => Context.Title;

		public string ApplicationName => Context.ApplicationName;

		public UIRect UIRect
		{
			get => Context.UIRect;
			set => Context.UIRect = value;
		}

		public bool AllowMultiple => Context.AllowMultiple;
		
		public ScriptableObject PersistantData { get; set; }

		public Context Context
		{
			get { return _context; }
		}

		public InitializationContext(Context context)
		{
			_context = context;
		}

		public void ValidateSetup()
		{
			if (MainContentType == null)
				throw new InitializationContextInvalid();
		}
	}
}