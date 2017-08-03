using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unity.Editor.Unit.Applications.Empty
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