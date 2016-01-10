using System.Collections.Generic;

namespace WellFired.Guacamole
{
	public class ViewBase
	{
		private INativeRenderer nativeRenderer;

		public IList<ViewBase> Children
		{
			get;
			set;
		}

		public LayoutOptions HorizontalLayout 
		{
			get;
			set;
		}

		public LayoutOptions VerticalLayout 
		{
			get;
			set;
		}

		public UIColor BackgroundColor 
		{
			get;
			set;
		}

		private INativeRenderer NativeRenderer
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
		}

		public virtual void Layout(UIRect rect)
		{
			
		}

		public virtual void Render()
		{
			
		}
	}
}