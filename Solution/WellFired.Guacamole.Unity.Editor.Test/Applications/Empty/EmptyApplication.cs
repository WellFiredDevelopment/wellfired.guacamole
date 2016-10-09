using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Unity.Editor.Test.Applications.Empty
{
	[UsedImplicitly]
	public class EmptyApplication : LaunchableApplication
	{
		[UsedImplicitly]
		public static IApplication Launch()
		{
			return Launch<EmptyWindow>(new UIRect(50, 50, 600, 300), new UISize(260, 60));
		}
	}
}