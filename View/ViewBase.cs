using System;
using System.Collections.Generic;
using System.Linq;

namespace WellFired.Guacamole
{
	public class ViewBase
	{
		private INativeRenderer nativeRenderer;
		private bool invalidRectRequest = true;
		private UIRect validRectRequest = new UIRect();
		private UIRect finalRenderRect = new UIRect();

		public IList<ViewBase> Children { get; set; }
		public LayoutOptions HorizontalLayout { get; set; }
		public LayoutOptions VerticalLayout { get; set; }
		public UIPadding Padding { get; set; }
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

		public virtual void Render(UIRect parentRect)
		{
			finalRenderRect.X = parentRect.X + RectRequest.X;
			finalRenderRect.Y = parentRect.Y + RectRequest.Y;
			finalRenderRect.Width = RectRequest.Width;
			finalRenderRect.Height = RectRequest.Height;

			NativeRenderer.Render(renderRect : finalRenderRect);

			foreach(var child in Children)
				child.Render(parentRect: finalRenderRect);
		}

		public virtual void AttemptToFullfillRequests(UIRect availableSpace)
		{
			if (HorizontalLayout == LayoutOptions.Fill)
				validRectRequest.Width = availableSpace.Width;
			if (VerticalLayout == LayoutOptions.Fill)
				validRectRequest.Height = availableSpace.Height;

			foreach(var child in Children)
				child.AttemptToFullfillRequests(availableSpace: (availableSpace - Padding));
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
				validRectRequest += Padding;
				invalidRectRequest = false;
			}
		}

		protected virtual UIRect CalculateValidRectRequest()
		{
			return UIRect.Min;
		}

		internal void LayoutTo(int x, int y)
		{
			validRectRequest.X = x;
			validRectRequest.Y = y;
		}

		internal void ReAdjustTo(int width, int height)
		{
			validRectRequest.Width = width;
			validRectRequest.Height = height;
		}
	}
}