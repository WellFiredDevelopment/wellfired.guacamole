using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unity.Editor.Tests.Applications.Empty
{
	[UsedImplicitly]
	public class EmptyWindow : Window
	{
		public EmptyWindow()
		{
			Padding = UIPadding.Of(5);
			Content = new TextEntry { Id = "AcceptanceLabel" };
		}
	}
}