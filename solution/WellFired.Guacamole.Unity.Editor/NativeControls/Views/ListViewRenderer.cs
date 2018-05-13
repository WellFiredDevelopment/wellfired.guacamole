using System;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(ListView), typeof(ListViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class ListViewRenderer : BaseRenderer
	{
		private bool _draggingScrollBar;
		private Vector2 _previousMousePosition;
		private Texture2D ScrollBarBackgroundTexture { get; set; }
		private GUIStyle ScrollBarStyle { get; set; }

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);
			
			if (ScrollBarBackgroundTexture == null)
				CreateScrollBarBackgroundTexture();

			var listView = (ListView)Control;
			HandleScroll(UnityRect, listView);
			EditorGUI.LabelField(UnityRect, string.Empty, Style);

			if (!listView.CanScroll || !listView.ShouldShowScrollBar)
				return;

			var scrollBarRect = CalculateScrollBarRect(UnityRect, listView);

			var offset = (float) listView.ScrollBarCornerRadius;
			var smallest = (int) (Mathf.Min(offset, Mathf.Min(scrollBarRect.width * 0.5f, scrollBarRect.height * 0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			ScrollBarStyle.border = new RectOffset(smallest, smallest, smallest, smallest);
			
			EditorGUI.LabelField(scrollBarRect, string.Empty, ScrollBarStyle);

			if (UnityEngine.Event.current.type == EventType.MouseDown && scrollBarRect.Contains(UnityEngine.Event.current.mousePosition))
			{
				_draggingScrollBar = true;
				_previousMousePosition = UnityEngine.Event.current.mousePosition;
			}
			if (UnityEngine.Event.current.rawType == EventType.MouseUp)
			{
				_draggingScrollBar = false;
			}

			if (_draggingScrollBar && UnityEngine.Event.current.isMouse)
			{
				var currentMousePosition = UnityEngine.Event.current.mousePosition;
				var delta = _previousMousePosition - currentMousePosition;
				
				var scrollDelta = SizingHelper.GetImportantValue(listView.Orientation, -delta.x, -delta.y);
				
				var sizeRatio = listView.TotalContentSize / SizingHelper.GetImportantSize(listView.Orientation, listView.RectRequest);
				listView.ScrollOffset += scrollDelta * sizeRatio;
				_previousMousePosition = currentMousePosition;
			}
		}

		protected override void SetupWithNewStyle()
		{
			base.SetupWithNewStyle();
			
			ScrollBarStyle = new GUIStyle
			{
				focused = {background = ScrollBarBackgroundTexture},
				active = {background = ScrollBarBackgroundTexture},
				hover = {background = ScrollBarBackgroundTexture},
				normal = {background = ScrollBarBackgroundTexture}
			};
		}

		public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

			if (e.PropertyName == ListView.ScrollBarCornerRadiusProperty.PropertyName ||
			    e.PropertyName == ListView.ScrollBarCornerMaskProperty.PropertyName ||
			    e.PropertyName == ListView.ScrollBarOutlineColorProperty.PropertyName ||
			    e.PropertyName == ListView.ScrollBarBackgroundColorProperty.PropertyName ||
			    e.PropertyName == ListView.ScrollBarOutlineThicknessProperty.PropertyName ||
			    e.PropertyName == ListView.ScrollBarOutlineMaskProperty.PropertyName)
			{
				CreateScrollBarBackgroundTexture();
				ResetStyle();
			}
		}

		public override bool PushMaskStack(UIRect maskRect)
		{
			base.PushMaskStack(maskRect);
			
			var listView = Control as ListView;
			
			// ReSharper disable once PossibleNullReferenceException
			if (listView.ShouldShowScrollBar && listView.CanScroll)
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
			// ReSharper disable once PossibleNullReferenceException
			ScrollBarBackgroundTexture = Texture2DExtensions.CreateRoundedTexture(64, 64, listView.ScrollBarBackgroundColor, listView.ScrollBarOutlineColor, listView.ScrollBarCornerRadius, listView.ScrollBarOutlineThickness, listView.ScrollBarCornerMask, listView.ScrollBarOutlineMask);
		}

		private static void HandleScroll(Rect unityRect, ListView listView)
		{
			if (!unityRect.Contains(UnityEngine.Event.current.mousePosition) || UnityEngine.Event.current.type != EventType.ScrollWheel) 
				return;
			
			var scrollDelta = SizingHelper.GetImportantValue(listView.Orientation, UnityEngine.Event.current.delta.x, UnityEngine.Event.current.delta.y);
			
			// Adjust for scroll too slow.
			scrollDelta *= 4.0f;
			
			listView.ScrollOffset += scrollDelta;
		}

		private static Rect CalculateScrollBarRect(Rect unityRect, IListView listView)
		{
			if (!listView.CanScroll)
				return Rect.zero;
			
			var scrollBarRect = unityRect;
			float sizeRatio;
			int portionAvailableToMove;
			float scrollRatio;
			switch (listView.Orientation)
			{
				case OrientationOptions.Horizontal:
					scrollBarRect.y += scrollBarRect.height - listView.ScrollBarSize;
					scrollBarRect.height = listView.ScrollBarSize;
					sizeRatio = listView.RectRequest.Width / listView.TotalContentSize;
					scrollBarRect.width = (int)(scrollBarRect.width * sizeRatio);
					portionAvailableToMove = (int)(unityRect.width - scrollBarRect.width);
					scrollRatio = listView.ScrollOffset / ListViewHelper.MaxScrollFor((int)unityRect.width, listView.TotalContentSize);
					scrollBarRect.x += (int)(portionAvailableToMove * scrollRatio);
					if (scrollBarRect.width < 10.0f)
						scrollBarRect.width = 10.0f;
					break;
				case OrientationOptions.Vertical:
					scrollBarRect.x += scrollBarRect.width - listView.ScrollBarSize;
					scrollBarRect.width = listView.ScrollBarSize;
					sizeRatio = listView.RectRequest.Height / listView.TotalContentSize;
					scrollBarRect.height = (int)(scrollBarRect.height * sizeRatio);
					portionAvailableToMove = (int)(unityRect.height - scrollBarRect.height);
					scrollRatio = listView.ScrollOffset / ListViewHelper.MaxScrollFor((int)unityRect.height, listView.TotalContentSize);
					scrollBarRect.y += (int)(portionAvailableToMove * scrollRatio);
					if (scrollBarRect.height < 10.0f)
						scrollBarRect.height = 10.0f;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			return scrollBarRect;
		}
	}
}