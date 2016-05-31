using System.Collections.Generic;
using System;
using WellFired.Guacamole.Databinding;

namespace WellFired.Guacamole
{
	public class ViewBase : BindableObject
	{
		private INativeRenderer nativeRenderer;
		private bool invalidRectRequest = true;
		private UIRect validRectRequest;
		private UIRect finalRenderRect;

		public IList<ViewBase> Children { get; set; }
		public LayoutOptions HorizontalLayout { get; set; }
		public LayoutOptions VerticalLayout { get; set; }
		public UIPadding Padding { get; set; }

		public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create<ViewBase, UIColor>(
			defaultValue: UIColor.White,
			bindingMode: BindingMode.TwoWay,
			getter: view => view.BackgroundColor
		);

		public static readonly BindableProperty HoverBackgroundColorProperty = BindableProperty.Create<ViewBase, UIColor>(
			defaultValue: default(UIColor),
			bindingMode: BindingMode.TwoWay,
			getter: view => view.HoverBackgroundColor
		);

		public static readonly BindableProperty ActiveBackgroundColorProperty = BindableProperty.Create<ViewBase, UIColor>(
			defaultValue: default(UIColor),
			bindingMode: BindingMode.TwoWay,
			getter: view => view.ActiveBackgroundColor
		);

		public static readonly BindableProperty OutlineColorProperty = BindableProperty.Create<ViewBase, UIColor>(
			defaultValue: default(UIColor),
			bindingMode: BindingMode.TwoWay,
			getter: view => view.OutlineColor
		);

		public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create<TextEntry, double>(
			defaultValue: 0.0,
			bindingMode: BindingMode.TwoWay,
			getter: entry => entry.CornerRadius
		);

		public static readonly BindableProperty CornerMaskProperty = BindableProperty.Create<TextEntry, CornerMask>(
			defaultValue: CornerMask.All,
			bindingMode: BindingMode.TwoWay,
			getter: entry => entry.CornerMask
		);

		public UIColor BackgroundColor
		{
			get { return (UIColor)GetValue(BackgroundColorProperty); }
			set { SetValue(BackgroundColorProperty, value); }
		}

		public UIColor HoverBackgroundColor
		{
			get { return (UIColor)GetValue(HoverBackgroundColorProperty); }
			set { SetValue(HoverBackgroundColorProperty, value); }
		}

		public UIColor ActiveBackgroundColor
		{
			get { return (UIColor)GetValue(ActiveBackgroundColorProperty); }
			set { SetValue(ActiveBackgroundColorProperty, value); }
		}

		public UIColor OutlineColor
		{
			get { return (UIColor)GetValue(OutlineColorProperty); }
			set { SetValue(OutlineColorProperty, value); }
		}

		public double CornerRadius
		{
			get { return (double)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}

		public CornerMask CornerMask
		{
			get { return (CornerMask)GetValue(CornerMaskProperty); }
			set { SetValue(CornerMaskProperty, value); }
		}

		public UIRect RectRequest
		{
			get { return validRectRequest; }
		}

		protected INativeRenderer NativeRenderer
		{
			get 
			{
				if (nativeRenderer != default(INativeRenderer))
					return nativeRenderer;

				try
				{
					var newNativeRenderer = NativeRendererHelper.CreateNativeRendererFor(GetType());

					if (nativeRenderer != null && newNativeRenderer != nativeRenderer)
						PropertyChanged -= nativeRenderer.OnPropertyChanged;

					nativeRenderer = newNativeRenderer;

					if (nativeRenderer != null)
						PropertyChanged += nativeRenderer.OnPropertyChanged;

					nativeRenderer.Control = this;
				}
				catch(Exception) 
				{
					throw new NativeRendererCannotBeFound(forControl : GetType().ToString());
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
			{
				// If something has set the binding context manually, we shouldn't override it. Otherwise, update it here.
				if(child.BindingContext == null)
					child.BindingContext = BindingContext;
				
				child.Layout();
			}
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

		public override void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

			if(e.PropertyName == BindingContextProperty.PropertyName) {
				foreach(var child in Children)
					child.BindingContext = BindingContext;
			}
		}
	}
}