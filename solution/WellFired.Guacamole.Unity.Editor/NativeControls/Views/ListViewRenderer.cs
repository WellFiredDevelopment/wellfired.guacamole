using System;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Attributes;
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
		private Texture2D ScrollBarBackgroundTexture { get; set; }
		private GUIStyle ScrollBarStyle { get; set; }
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
			
			if (ScrollBarStyle == null)
				ScrollBarStyle = new GUIStyle();

			ScrollBarStyle.focused.background = ScrollBarBackgroundTexture;
			ScrollBarStyle.active.background = ScrollBarBackgroundTexture;
			ScrollBarStyle.hover.background = ScrollBarBackgroundTexture;
			ScrollBarStyle.normal.background = ScrollBarBackgroundTexture;
		}

		public override void Render(UIRect renderRect)
		{
			if (ScrollBarBackgroundTexture == null)
				CreateScrollBarBackgroundTexture();
			
			base.Render(renderRect);

			var listView = Control as ListView;

			Debug.Assert(listView != null, "listView != null");

			CreateStyleWith(listView);

			if (renderRect.ToUnityRect().Contains(UnityEngine.Event.current.mousePosition) && UnityEngine.Event.current.type == EventType.ScrollWheel)
			{
				var scrollDelta = SizingHelper.GetImportantValue(listView.Orientation, UnityEngine.Event.current.delta.x, UnityEngine.Event.current.delta.y);
				scrollDelta = ListViewHelper.CorrectScroll(listView.Orientation, scrollDelta);
				listView.ScrollOffset += scrollDelta;
			}

			var offset = (float) Control.CornerRadius;
			var smallest = (int) (Mathf.Min(offset, Mathf.Min(renderRect.Width*0.5f, renderRect.Height*0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			Style.border = new RectOffset(smallest, smallest, smallest, smallest);
			
			EditorGUI.LabelField(renderRect.ToUnityRect(), string.Empty, Style);

			if (!listView.CanScroll || !listView.ShouldShowScrollBar)
				return;

			var scrollBarRect = renderRect;
			float sizeRatio;
			int portionAvailableToMove;
			float scrollRatio;
			switch (listView.Orientation)
			{
				case OrientationOptions.Horizontal:
					scrollBarRect.Y += scrollBarRect.Height - listView.ScrollBarSize;
					scrollBarRect.Height = listView.ScrollBarSize;
					sizeRatio = listView.RectRequest.Width / (float) listView.TotalContentSize;
					scrollBarRect.Height = (int)(scrollBarRect.Width * sizeRatio);
					portionAvailableToMove = renderRect.Width - scrollBarRect.Width;
					scrollRatio = -listView.ScrollOffset / ListViewHelper.MaxScrollFor(listView);
					scrollBarRect.X += (int)(portionAvailableToMove * scrollRatio);
					break;
				case OrientationOptions.Vertical:
					scrollBarRect.X += scrollBarRect.Width - listView.ScrollBarSize;
					scrollBarRect.Width = listView.ScrollBarSize;
					sizeRatio = listView.RectRequest.Height / (float) listView.TotalContentSize;
					scrollBarRect.Height = (int)(scrollBarRect.Height * sizeRatio);
					portionAvailableToMove = renderRect.Height - scrollBarRect.Height;
					scrollRatio = -listView.ScrollOffset / ListViewHelper.MaxScrollFor(listView);
					scrollBarRect.Y += (int)(portionAvailableToMove * scrollRatio);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			offset = (float) listView.ScrollBarCornerRadius;
			smallest = (int) (Mathf.Min(offset, Mathf.Min(scrollBarRect.Width*0.5f, scrollBarRect.Height*0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			ScrollBarStyle.border = new RectOffset(smallest, smallest, smallest, smallest);
			
			EditorGUI.LabelField(scrollBarRect.ToUnityRect(), string.Empty, ScrollBarStyle);
		}

		public override void ResetStyle()
		{
			Style = null;
			ScrollBarStyle = null;
		}

		public override bool PushMaskStack(UIRect maskRect)
		{
			base.PushMaskStack(maskRect);
			
			var listView = Control as ListView;
			Debug.Assert(listView != null, "listView != null");
			
			if (listView.ShouldShowScrollBar)
			{
				switch (listView.Orientation)
				{
					case OrientationOptions.Horizontal:
						maskRect.Height -= listView.ScrollBarSize;
						break;
					case OrientationOptions.Vertical:
						maskRect.Width -= listView.ScrollBarSize;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			var unityRect = maskRect.ToUnityRect();
			GUI.BeginGroup(unityRect);
			return true;
		}

		public override void PopMaskStack()
		{
			base.PopMaskStack();
			GUI.EndGroup();
		}

		private void CreateScrollBarBackgroundTexture()
		{
			var listView = Control as ListView;
			Debug.Assert(listView != null, "listView != null");
			ScrollBarBackgroundTexture = Texture2DExtensions.CreateRoundedTexture(64, 64, listView.ScrollBarBackgroundColor, listView.ScrollBarOutlineColor, listView.ScrollBarCornerRadius, listView.ScrollBarCornerMask);
		}
	}
}