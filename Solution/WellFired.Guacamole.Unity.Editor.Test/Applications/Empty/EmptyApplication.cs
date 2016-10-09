using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Unity.Editor.Test.Applications.Empty
{
	[UsedImplicitly]
	public class EmptyApplication : LaunchableApplication
	{
		[UsedImplicitly]
		public static Application Launch()
		{
			return Launch<EmptyWindow>(new UIRect(50, 50, 600, 200), new UISize(260, 30));
		}
	}
}