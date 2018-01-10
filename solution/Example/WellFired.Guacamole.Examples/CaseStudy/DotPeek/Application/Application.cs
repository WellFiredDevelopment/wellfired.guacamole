using UnityEditor;
using WellFired.Guacamole.Data;
using JetBrains.Annotations;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Application
{
	[UsedImplicitly]
	public class DotPeekApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/CaseStudy/DotPeek")]
		[UsedImplicitly]
		public static void OpenWindow()
		{
			Launch<DotPeekWindow>(
				uiRect: UIRect.With(50, 50, 400, 600),
				minSize: UISize.Of(400, 600),
				title: "Dot Peek",
				allowMultiple: false,
				applicationName: "DotPeek",
				persistantType: typeof(PersistentData));
		}
	}
}