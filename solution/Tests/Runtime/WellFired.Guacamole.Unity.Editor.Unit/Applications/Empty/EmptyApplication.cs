using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;

namespace WellFired.Guacamole.Unity.Editor.Unit.Applications.Empty
{
	[UsedImplicitly]
	public class EmptyApplication : LaunchableApplication
	{
		[UsedImplicitly]
		public static IApplication Launch()
		{
			return Launch<EmptyWindow>(UIRect.With(50, 50, 600, 300), UISize.Of(260, 60));
		}
	}
}