using System.ComponentModel;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unity.Editor.Tests.Applications.Empty
{
	[UsedImplicitly]
	public class EmptyWindow : Window
	{
		public EmptyWindow(ILogger logger, INotifyPropertyChanged persistantData) 
			: base(logger, persistantData)
		{
			Padding = UIPadding.Of(5);
			Content = new TextEntry { Id = "AcceptanceLabel" };
		}
	}
}