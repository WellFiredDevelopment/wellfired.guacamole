using System;
using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.Renderer;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
    public class ViewBase : BindableObject
    {
        [PublicAPI] public static readonly BindableProperty BackgroundColorProperty = BindableProperty
            .Create<ViewBase, UIColor>(
                defaultValue: UIColor.White,
                bindingMode: BindingMode.TwoWay,
                getter: view => view.BackgroundColor
            );

        [PublicAPI] public static readonly BindableProperty HoverBackgroundColorProperty = BindableProperty
            .Create<ViewBase, UIColor>(
                default(UIColor),
                bindingMode: BindingMode.TwoWay,
                getter: view => view.HoverBackgroundColor
            );

        [PublicAPI] public static readonly BindableProperty ActiveBackgroundColorProperty = BindableProperty
            .Create<ViewBase, UIColor>(
                default(UIColor),
                bindingMode: BindingMode.TwoWay,
                getter: view => view.ActiveBackgroundColor
            );

        [PublicAPI] public static readonly BindableProperty OutlineColorProperty = BindableProperty
            .Create<ViewBase, UIColor>(
                default(UIColor),
                bindingMode: BindingMode.TwoWay,
                getter: view => view.OutlineColor
            );

        [PublicAPI] public static readonly BindableProperty CornerRadiusProperty = BindableProperty
            .Create<TextEntry, double>(
                defaultValue: 0.0,
                bindingMode: BindingMode.TwoWay,
                getter: entry => entry.CornerRadius
            );

        [PublicAPI] public static readonly BindableProperty CornerMaskProperty = BindableProperty
            .Create<TextEntry, CornerMask>(
                defaultValue: CornerMask.All,
                bindingMode: BindingMode.TwoWay,
                getter: entry => entry.CornerMask
            );

		[PublicAPI]
		public static readonly BindableProperty MinSizeProperty = BindableProperty
			.Create<TextEntry, UISize>(
				defaultValue: UISize.Min,
				bindingMode: BindingMode.TwoWay,
				getter: entry => entry.MinSize
			);

		[PublicAPI]
		public static readonly BindableProperty MaxSizeProperty = BindableProperty
			.Create<TextEntry, UISize>(
				defaultValue: UISize.Max,
				bindingMode: BindingMode.TwoWay,
				getter: entry => entry.MaxSize
			);

		private UIRect _finalRenderRect;
        private bool _invalidRectRequest = true;
        private INativeRenderer _nativeRenderer;
        private UIRect _validRectRequest;
        private Style _style;

        public IList<ViewBase> Children { get; }

		[PublicAPI]
		public LayoutOptions HorizontalLayout { get; set; }

		[PublicAPI]
        public LayoutOptions VerticalLayout { get; set; }

		[PublicAPI]
		public UIPadding Padding { get; set; }

        public Style Style
        {
            set
            {
                if (_style == value)
                    return;

                _style = value;
                UpdateFromStyle(_style);
            }
        }

        [PublicAPI]
        public UIColor BackgroundColor
        {
            get { return (UIColor)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        [PublicAPI]
        public UIColor HoverBackgroundColor
        {
            get { return (UIColor)GetValue(HoverBackgroundColorProperty); }
            set { SetValue(HoverBackgroundColorProperty, value); }
        }

        [PublicAPI]
        public UIColor ActiveBackgroundColor
        {
            get { return (UIColor)GetValue(ActiveBackgroundColorProperty); }
            set { SetValue(ActiveBackgroundColorProperty, value); }
        }

        [PublicAPI]
        public UIColor OutlineColor
        {
            get { return (UIColor)GetValue(OutlineColorProperty); }
            set { SetValue(OutlineColorProperty, value); }
        }

        [PublicAPI]
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        [PublicAPI]
        public CornerMask CornerMask
        {
            get { return (CornerMask)GetValue(CornerMaskProperty); }
            set { SetValue(CornerMaskProperty, value); }
		}

		[PublicAPI]
		public UISize MinSize
		{
			get { return (UISize)GetValue(MinSizeProperty); }
			set { SetValue(MinSizeProperty, value); }
		}

		[PublicAPI]
		public UISize MaxSize
		{
			get { return (UISize)GetValue(MaxSizeProperty); }
			set { SetValue(MaxSizeProperty, value); }
		}

		public UIRect RectRequest => _validRectRequest;

        protected INativeRenderer NativeRenderer
        {
            get
            {
                if(_nativeRenderer != default(INativeRenderer))
                    return _nativeRenderer;

                try
                {
                    var newNativeRenderer = NativeRendererHelper.CreateNativeRendererFor(GetType());

                    if(_nativeRenderer != null && newNativeRenderer != _nativeRenderer)
                        PropertyChanged -= _nativeRenderer.OnPropertyChanged;

                    _nativeRenderer = newNativeRenderer;

                    if(_nativeRenderer != null)
                    {
                        PropertyChanged += _nativeRenderer.OnPropertyChanged;
                        _nativeRenderer.Control = this;
                    }
                }
                catch(Exception)
                {
                    throw new NativeRendererCannotBeFound(GetType().ToString());
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

            NativeRenderer.Render(renderRect: _finalRenderRect);

            foreach(var child in Children)
                child.Render(parentRect: _finalRenderRect);
        }

        public virtual void AttemptToFullfillRequests(UIRect availableSpace)
        {
            if(HorizontalLayout == LayoutOptions.Fill)
                _validRectRequest.Width = availableSpace.Width;
            if(VerticalLayout == LayoutOptions.Fill)
                _validRectRequest.Height = availableSpace.Height;

            foreach(var child in Children)
                child.AttemptToFullfillRequests(availableSpace - Padding);
        }

        [PublicAPI]
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
            foreach(var child in Children)
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
	        var nativeSize = NativeRenderer.NativeSize;
			// If the native renderer returns null, we simply use our own layoutting system.
	        return nativeSize != null ? new UIRect(0, 0, nativeSize.Value.Width, nativeSize.Value.Height) : new UIRect(0, 0, 100, 10);
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

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

	        if(e.PropertyName != BindingContextProperty.PropertyName)
				return;

	        foreach(var child in Children)
		        child.BindingContext = BindingContext;
        }

        private void UpdateFromStyle(Style style)
        {
            foreach (var setter in style.Setters)
                SetValue(setter.Property, setter.Value);
        }
    }
}