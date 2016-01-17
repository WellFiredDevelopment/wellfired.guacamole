using System;
using System.Collections.Generic;

namespace WellFired.Guacamole
{
	public class ViewBase
	{
		private INativeRenderer nativeRenderer;
		private bool invalidRectRequest = false;
		private UIRect validRectRequest = new UIRect();

		public IList<ViewBase> Children { get; set; }
		public LayoutOptions HorizontalLayout { get; set; }
		public LayoutOptions VerticalLayout { get; set; }
		public Padding Padding { get; set; }
		public UIColor BackgroundColor { get; set; }
		public UIRect FinalRenderedRect { get; set; }

		public UIRect RectRequest
		{
			get 
			{
				if(!invalidRectRequest)
					return validRectRequest;

				CalculateRect();
				return validRectRequest;
			}
		}

		protected INativeRenderer NativeRenderer
		{
			get 
			{
				if (nativeRenderer == default(INativeRenderer)) 
				{
					nativeRenderer = NativeRendererHelper.CreateNativeRendererFor(this.GetType());
					nativeRenderer.Control = this;
				}

				return nativeRenderer;
			}
		}

		public ViewBase()
		{
			Children = new List<ViewBase>();
			validRectRequest = UIRect.Min;
		}

		public virtual void Layout(UIRect parentRect)
		{
			foreach(var child in Children)
				child.Layout(parentRect : parentRect);	
		}

		public virtual void Render()
		{
			foreach(var child in Children)
				child.Render();
		}

		public void InvalidateRectRequest()
		{
			invalidRectRequest = true;
			foreach(var child in Children) 
				child.InvalidateRectRequest();
		}

		protected virtual UIRect CalculateRect()
		{
			invalidRectRequest = false;
			return UIRect.Min;
		}
	}
}