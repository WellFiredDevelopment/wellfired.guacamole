using System;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    public static class ViewSizingExtensions
    {
        public static void DoSizingAndLayout(IView view, UIRect availableRegion)
        {
            CalculateRectRequest(view);
            AttemptToFullfillRequests(view, availableRegion);

            UpdateContextIfNeeded(view as IBindableObject);
            if(view.Content != null)
                UpdateContextIfNeeded(view.Content as IBindableObject);

            DoLayout(view);
        }

        private static void CalculateRectRequest(IView view)
        {
            if (view == null)
                return;

            var canLayout = view as ICanLayout;
            if (canLayout != null)
            {
                foreach(var child in canLayout.Children)
                    CalculateRectRequest(child as IView);
            }

            var content = view.Content;
            if(view.Equals(content))
                throw new Exception("Circular view dependency");

            CalculateRectRequest(content);

            if (view.ValidRectRequest)
                return;

            view.RectRequest = CalculateValidRectRequest(view);
            view.ContentRectRequest = view.RectRequest;
            
            //Thibault : In which situation View can be again processed by CalculateRectRequest so that we
            // have to set a flag to true ? Layout inside Layout ?
            view.ValidRectRequest = true;
        }

        private static UIRect CalculateValidRectRequest(IView view)
        {
            var canLayout = view as ICanLayout;
            if (canLayout != null)
                return canLayout.Layout.CalculateValidRectRequest(canLayout.Children, view.MinSize);

            var defaultSize = UISize.Of(view.MinSize.Width, view.MinSize.Height);
            
            var content = view.Content;
            if (content != null)
                defaultSize = content.RectRequest.Size;

            defaultSize.Width += view.Padding.Width;
            defaultSize.Height += view.Padding.Height;

            // If the native renderer returns null, we simply use our own layoutting system.
            var nativeSize = view?.NativeRenderer?.NativeSize ?? defaultSize;

            // Constrain
            nativeSize = Constrain(nativeSize, view.MinSize);

            return UIRect.With(0, 0, nativeSize.Width, nativeSize.Height);
        }

        public static void AttemptToFullfillRequests(IView view, UIRect availableSpace)
        {
            var rectRequest = view.RectRequest;
            var contentRectRequest = view.ContentRectRequest;

            rectRequest.X = availableSpace.X;
            rectRequest.Y = availableSpace.Y;

            switch (view.HorizontalLayout)
            {
                case LayoutOptions.Fill:
                    rectRequest.Width = availableSpace.Width;
                    contentRectRequest.Width = availableSpace.Width;
                    break;
                case LayoutOptions.Center:
                    rectRequest.Width = availableSpace.Width;
                    contentRectRequest.X = availableSpace.Width / 2 - contentRectRequest.Width / 2;
                    break;
                case LayoutOptions.Expand:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (view.VerticalLayout)
            {
                case LayoutOptions.Fill:
                    rectRequest.Height = availableSpace.Height;
                    contentRectRequest.Height = availableSpace.Height;
                    break;
                case LayoutOptions.Center:
                    rectRequest.Height = availableSpace.Height;
                    contentRectRequest.Y = availableSpace.Height / 2 - contentRectRequest.Height / 2;
                    break;
                case LayoutOptions.Expand:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            view.RectRequest = rectRequest;
            view.ContentRectRequest = contentRectRequest;

            if (view.Content != null)
                AttemptToFullfillRequests(view.Content, UIRect.With(view.ContentRectRequest.Width, view.ContentRectRequest.Height) - view.Padding);

            var layout = view as ICanLayout;
            layout?.Layout.AttemptToFullfillRequests(layout.Children, UIRect.With(view.ContentRectRequest.Width, view.ContentRectRequest.Height) - view.Padding, view.Padding, view.HorizontalLayout, view.VerticalLayout);
        }

        public static void UpdateContextIfNeeded(IBindableObject bindable)
        {
            if(bindable == null)
                return;

             // If something has set the binding context manually, we shouldn't override it. Otherwise, update it here.
             if (bindable.BindingContext == null)
                 bindable.BindingContext = bindable.BindingContext;

            var canLayout = bindable as ICanLayout;
            if (canLayout == null)
                return;

            foreach(var child in canLayout.Children)
                UpdateContextIfNeeded(child as IBindableObject);
        }

        private static void DoLayout(IView view)
        {
            if (view == null)
                return;

            var layout = view as ICanLayout;
            layout?.Layout.Layout(layout.Children, view.RectRequest, view.Padding);

            if(view.Content != null)
                DoLayout(view.Content);

            if (layout == null)
                return;

            foreach(var child in layout.Children)
                DoLayout(child as IView);
        }

        private static UISize Constrain(UISize requestedSize, UISize minSize)
        {
            if (minSize == UISize.Min)
                return requestedSize;

            //Thibault : It looks like we are setting requestedSize to minSize. Is it intended ?
            if (requestedSize.Width < minSize.Width)
                requestedSize.Width = minSize.Width;
            if (requestedSize.Height < minSize.Height)
                requestedSize.Height = minSize.Height;
            if (requestedSize.Width > minSize.Width)
                requestedSize.Width = minSize.Width;
            if (requestedSize.Height > minSize.Height)
                requestedSize.Height = minSize.Height;

            return requestedSize;
        }
    }
}