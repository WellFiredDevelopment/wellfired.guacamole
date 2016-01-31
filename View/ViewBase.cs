using System;
using System.Collections.Generic;

namespace WellFired.Guacamole
{
	public class ViewBase
	{
		private INativeRenderer nativeRenderer;
		private bool invalidRectRequest = true;
		private UIRect validRectRequest = new UIRect();

		public IList<ViewBase> Children { get; set; }
		public LayoutOptions HorizontalLayout { get; set; }
		public LayoutOptions VerticalLayout { get; set; }
		public Padding Padding { get; set; }
		public UIColor BackgroundColor { get; set; }

		public UIRect RectRequest
		{
			get 
			{
				return validRectRequest;
			}
		}

		protected INativeRenderer NativeRenderer
		{
			get 
			{
				if (nativeRenderer == default(INativeRenderer)) 
				{
					try
					{
						nativeRenderer = NativeRendererHelper.CreateNativeRendererFor(this.GetType());
						nativeRenderer.Control = this;
					}
					catch(Exception) 
					{
						throw new NativeRendererCannotBeFound(forControl : this.GetType().ToString());
					}
				}

				return nativeRenderer;
			}
		}

		public ViewBase()
		{
			Children = new List<ViewBase>();
			validRectRequest = UIRect.Min;
		}

		public virtual void Layout()
		{
			foreach(var child in Children)
				child.Layout();	
		}

		public virtual void Render()
		{
			NativeRenderer.Render(renderRect : RectRequest);

			foreach(var child in Children)
				child.Render();
		}

		public void InvalidateRectRequest()
		{
			invalidRectRequest = true;
			foreach(var child in Children) 
				child.InvalidateRectRequest();
		}

		public void CalculateRectRequest()
		{
			// When calculating size, we want to recurse the whole structure, calculating the size of the Child
			// components first of all.
			foreach (var child in Children)
				child.CalculateRectRequest();

			if(invalidRectRequest) 
			{
				validRectRequest = CalculateValidRectRequest();
				invalidRectRequest = false;
			}
		}

		protected virtual UIRect CalculateValidRectRequest()
		{
			return UIRect.Min;
		}

		public void LayoutTo(int x, int y)
		{
			validRectRequest.X = x;
			validRectRequest.Y = y;
		}
	}
}