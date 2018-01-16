using System;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Views;
using WellFired.Guacamole.WindowContext;

namespace WellFired.Guacamole.Unity.Editor
{
	public class LaunchableApplication
	{
		/// <summary>
		/// Will launch a window with the passed parameters
		/// </summary>
		/// <param name="uiRect">The initial size of the window to be launched</param>
		/// <param name="minSize">The minimum size this window can become</param>
		/// <param name="title">The title of this window</param>
		/// <param name="allowMultiple">Can we allow multiple of these windows to be opened?</param>
		/// <param name="applicationName">The application name is used internally to cache application specific data</param>
		/// <param name="persistantType">The type of Persistent data we want to provide to this window. Guacamole will handle instantiation and passing the data. Ensure you have a parameterless constructor on this type</param>
		/// <typeparam name="TWindow">The type of window to Launch. Should be of type Window</typeparam>
		/// <returns></returns>
		protected static IApplication Launch<TWindow>(UIRect uiRect, UISize minSize, string title = null, bool allowMultiple = true, string applicationName = "Guacamole", Type persistantType = null) where TWindow : Window
		{
			var application = new Application();

			var context = new Context
			{
				MainContentType = typeof(TWindow),
				UIRect = uiRect,
				MinSize = minSize,
				Title = title,
				AllowMultiple = allowMultiple,
				ApplicationName = applicationName
			};

			return application.Launch(new InitializationContext(context), persistantType);
		}
		
		/// <summary>
		/// Will launch a window with the passed parameters, This method will also cause Guacamole to construct an object of Type TViewModel and inject any systemic services. This ViewModel will be
		/// automatically assigned to the window as a binding context.
		/// </summary>
		/// <param name="uiRect">The initial size of the window to be launched</param>
		/// <param name="minSize">The minimum size this window can become</param>
		/// <param name="title">The title of this window</param>
		/// <param name="allowMultiple">Can we allow multiple of these windows to be opened?</param>
		/// <param name="applicationName">The application name is used internally to cache application specific data</param>
		/// <param name="persistantType">The type of Persistent data we want to provide to this window. Guacamole will handle instantiation and passing the data. Ensure you have a parameterless constructor on this type</param>
		/// <typeparam name="TWindow">The type of window to Launch. Should be of type Window</typeparam>
		/// <typeparam name="TViewModel">The type of ViewModel to construct, Should implement the IBasicViewModel interface</typeparam>
		/// <returns></returns>
		protected static IApplication Launch<TWindow, TViewModel>(UIRect uiRect, UISize minSize, string title = null, bool allowMultiple = true, string applicationName = "Guacamole", Type persistantType = null) 
			where TWindow : Window where TViewModel : IBasicViewModel
		{
			var application = new Application();

			var context = new Context
			{
				MainContentType = typeof(TWindow),
				MainViewModelType = typeof(TViewModel),
				UIRect = uiRect,
				MinSize = minSize,
				Title = title,
				AllowMultiple = allowMultiple,
				ApplicationName = applicationName
			};

			return application.Launch(new InitializationContext(context), persistantType);
		}
	}
}