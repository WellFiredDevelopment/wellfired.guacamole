using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;
using Debug = System.Diagnostics.Debug;

[assembly: CustomRenderer(typeof(ListView), typeof(ListViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class ListViewRenderer : BaseRenderer
	{
		private GUIStyle Style { get; set; }

		private void CreateStyleWith([NotNull] IView listView)
		{
			if (Style == null)
				Style = new GUIStyle();

			Style.focused.background = BackgroundTexture;
			Style.active.background = BackgroundTexture;
			Style.hover.background = BackgroundTexture;
			Style.normal.background = BackgroundTexture;

			Style.padding = listView.Padding.ToRectOffset();
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			var listView = Control as ListView;

			Debug.Assert(listView != null, "listView != null");

			CreateStyleWith(listView);

			if (renderRect.ToUnityRect().Contains(UnityEngine.Event.current.mousePosition) &&
			    UnityEngine.Event.current.type == EventType.ScrollWheel)
			{
				var scrollDelta = SizingHelper.GetImportantValue(listView.Orientation, UnityEngine.Event.current.delta.x, UnityEngine.Event.current.delta.y);
				scrollDelta = ListViewHelper.CorrectScroll(listView.Orientation, scrollDelta);
				listView.ScrollOffset += scrollDelta;
			}

			var offset = (float) Control.CornerRadius;
			var smallest = (int) (Mathf.Min(offset, Mathf.Min(renderRect.Width*0.5f, renderRect.Height*0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			Style.border = new RectOffset(smallest, smallest, smallest, smallest);
		}

		public override void ResetStyle()
		{
			Style = null;
		}

		public override bool PushMaskStack(UIRect maskRect)
		{
			base.PushMaskStack(maskRect);
			var unityRect = maskRect.ToUnityRect();
			GUI.BeginGroup(unityRect);
			return true;
		}

		public override void PopMaskStack()
		{
			base.PopMaskStack();
			GUI.EndGroup();
		}
	}
}