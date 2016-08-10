using System;
using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.Renderer;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
    public partial class ViewBase : BindableObject
	{
		private UIRect _finalRenderRect;
        private bool _invalidRectRequest = true;
        private INativeRenderer _nativeRenderer;
        private UIRect _validRectRequest;
        private Style _style;

        public IList<ViewBase> Children { get; }

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

                    if (_nativeRenderer != null && newNativeRenderer != _nativeRenderer)
                    {
                        PropertyChanged -= OnPropertyChanged;
                        PropertyChanged -= _nativeRenderer.OnPropertyChanged;
                    }

                    _nativeRenderer = newNativeRenderer;

                    if(_nativeRenderer != null)
                    {
                        PropertyChanged += _nativeRenderer.OnPropertyChanged;
                        PropertyChanged += OnPropertyChanged;
                        _nativeRenderer.Control = this;
						_nativeRenderer.Create();
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

	        if (!_invalidRectRequest)
				return;

	        _validRectRequest = CalculateValidRectRequest();
	        _validRectRequest += Padding;
	        _invalidRectRequest = false;
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

            ProcessTriggers(e.PropertyName);

            if (e.PropertyName != BindingContextProperty.PropertyName)
				return;

	        foreach(var child in Children)
		        child.BindingContext = BindingContext;
        }

        private void UpdateNewStyle(Style style)
        {
            foreach (var setter in style.Setters)
                SetValue(setter.Property, setter.Value);
        }

        private void ProcessTriggers(string propertyName)
        {
            foreach (var trigger in Style.Triggers)
			{
				if (trigger.Property.PropertyName != propertyName)
                    continue;

				var value = GetValue(trigger.Property);
                if (!value.Equals(trigger.Value))
                    continue;

                foreach(var setter in trigger.Setters)
                    SetValue(setter.Property, setter.Value);
            }
        }
    }
}