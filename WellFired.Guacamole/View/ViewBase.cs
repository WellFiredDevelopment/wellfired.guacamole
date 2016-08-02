using System.Collections.Generic;
using System;
using WellFired.Guacamole.Databinding;

namespace WellFired.Guacamole
{
	public class ViewBase : BindableObject
	{
		private INativeRenderer _nativeRenderer;
		private bool _invalidRectRequest = true;
		private UIRect _validRectRequest;
		private UIRect _finalRenderRect;

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
			get { return _validRectRequest; }
		}

		protected INativeRenderer NativeRenderer
		{
			get 
			{
				if (_nativeRenderer != default(INativeRenderer))
					return _nativeRenderer;

				try
				{
					var newNativeRenderer = NativeRendererHelper.CreateNativeRendererFor(GetType());

					if (_nativeRenderer != null && newNativeRenderer != _nativeRenderer)
						PropertyChanged -= _nativeRenderer.OnPropertyChanged;

					_nativeRenderer = newNativeRenderer;

					if (_nativeRenderer != null)
						PropertyChanged += _nativeRenderer.OnPropertyChanged;

					_nativeRenderer.Control = this;
				}
				catch(Exception) 
				{
					throw new NativeRendererCannotBeFound(forControl : GetType().ToString());
				}

				return _nativeRenderer;
			}
		}

		public ViewBase()
		{
			Children = new List<ViewBase>();
			_validRectRequest = UIRect.Min;
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
			_finalRenderRect.X = parentRect.X + RectRequest.X;
			_finalRenderRect.Y = parentRect.Y + RectRequest.Y;
			_finalRenderRect.Width = RectRequest.Width;
			_finalRenderRect.Height = RectRequest.Height;

			NativeRenderer.Render(renderRect : _finalRenderRect);

			foreach(var child in Children)
				child.Render(parentRect: _finalRenderRect);
		}

		public virtual void AttemptToFullfillRequests(UIRect availableSpace)
		{
			if (HorizontalLayout == LayoutOptions.Fill)
				_validRectRequest.Width = availableSpace.Width;
			if (VerticalLayout == LayoutOptions.Fill)
				_validRectRequest.Height = availableSpace.Height;

			foreach(var child in Children)
				child.AttemptToFullfillRequests(availableSpace: (availableSpace - Padding));
		}

		public void InvalidateRectRequest()
		{
			_invalidRectRequest = true;
			foreach(var child in Children) 
				child.InvalidateRectRequest();
		}

		public void CalculateRectRequest()
		{
			// When calculating size, we want to recurse the whole structure, calculating the size of the Child
			// components first of all.
			foreach (var child in Children)
				child.CalculateRectRequest();

			if(_invalidRectRequest) 
			{
				_validRectRequest = CalculateValidRectRequest();
				_validRectRequest += Padding;
				_invalidRectRequest = false;
			}
		}

		protected virtual UIRect CalculateValidRectRequest()
		{
			return UIRect.Min;
		}

		internal void LayoutTo(int x, int y)
		{
			_validRectRequest.X = x;
			_validRectRequest.Y = y;
		}

		internal void ReAdjustTo(int width, int height)
		{
			_validRectRequest.Width = width;
			_validRectRequest.Height = height;
		}

	    protected override void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

			if(e.PropertyName == BindingContextProperty.PropertyName) {
				foreach(var child in Children)
					child.BindingContext = BindingContext;
			}
		}
	}
}