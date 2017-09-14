using System;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;

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

            var hasChildren = view as IHasChildren;
            if (hasChildren != null)
            {
                foreach(var child in hasChildren.Children)
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
            view.ValidRectRequest = true;
        }

        private static UIRect CalculateValidRectRequest(IView view)
        {
            var canLayout = view as ICanLayout;
            if (canLayout != null)
            {
                var rectRequest = canLayout.Layout.CalculateValidRectRequest(canLayout.Children, view.MinSize);
                var totalSize = UISize.Of(rectRequest.Width, rectRequest.Height);
                var adjustedForPadding = ViewPaddingCalculation.AdjustForPadding(view.HorizontalLayout, view.VerticalLayout, view.Padding, totalSize);
                return UIRect.With(rectRequest.X, rectRequest.Y, adjustedForPadding.Width, adjustedForPadding.Height);
            }

            var listView = view as IListView;
            if (listView != null)
                return ListViewHelper.CalculateValidRectRequest(listView);

            var defaultSize = UISize.Of(view.MinSize.Width, view.MinSize.Height);
            
            var content = view.Content;
            if (content != null)
                defaultSize = content.RectRequest.Size;

            // If the native renderer returns null, we simply use our own layoutting system.
            var nativeSize = ViewPaddingCalculation.AdjustForPadding(view.HorizontalLayout, view.VerticalLayout, view.Padding, view.NativeRenderer?.NativeSize ?? defaultSize);

            // Constrain
            nativeSize = ConstrainUnder(nativeSize, view.MinSize);
            nativeSize = ConstrainOver(nativeSize, view.MaxSize);

            return UIRect.With(0, 0, nativeSize.Width, nativeSize.Height);
        }

        public static void AttemptToFullfillRequests(IView view, UIRect availableSpace)
        {
            var isCell = view is ICell;
            
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
                    if (rectRequest.Width > availableSpace.Width)
                        rectRequest.Width = availableSpace.Width;
                    if (contentRectRequest.Width > availableSpace.Width)
                        contentRectRequest.Width = availableSpace.Width;
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
                    if (rectRequest.Height > availableSpace.Height)
                        rectRequest.Height = availableSpace.Height;
                    if (contentRectRequest.Height > availableSpace.Height)
                        contentRectRequest.Height = availableSpace.Height;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (!isCell)
            {
                view.RectRequest = rectRequest;
                view.ContentRectRequest = contentRectRequest;
            }

            if (view.Content != null)
                AttemptToFullfillRequests(view.Content, UIRect.With(view.ContentRectRequest.Width, view.ContentRectRequest.Height) - view.Padding);

            var layout = view as ICanLayout;
            var listView = view as IListView;

            layout?.Layout.AttemptToFullfillRequests(layout.Children, UIRect.With(view.ContentRectRequest.Width, view.ContentRectRequest.Height) - view.Padding, view.Padding, view.HorizontalLayout, view.VerticalLayout);

            if (listView == null) 
                return;

            foreach (var child in listView.Children)
            {
                ListViewHelper.ConstrainToCell(listView, child);
                AttemptToFullfillRequests(child as IView, UIRect.With(view.ContentRectRequest.Width, view.ContentRectRequest.Height) - view.Padding);
            }
        }

        public static void UpdateContextIfNeeded(IBindableObject bindable)
        {
            if(bindable == null)
                return;

             // If something has set the binding context manually, we shouldn't override it. Otherwise, update it here.
             if (bindable.BindingContext == null)
                 bindable.BindingContext = bindable.BindingContext;

            var children = bindable as IHasChildren;
            if (children == null)
                return;

            foreach(var child in children.Children)
                UpdateContextIfNeeded(child as IBindableObject);
        }

        private static void DoLayout(IView view)
        {
            if (view == null)
                return;

            var layout = view as ICanLayout;
            layout?.Layout.Layout(layout.Children, view.RectRequest, view.Padding);
            
            var listView = view as IListView;
            if (listView != null)
                ListViewHelper.Layout(listView, view.RectRequest, view.Padding);

            if(view.Content != null)
                DoLayout(view.Content);

            var hasChildren = view as IHasChildren;
            if (hasChildren == null)
                return;

            foreach(var child in hasChildren.Children)
                DoLayout(child as IView);
        }

        private static UISize ConstrainUnder(UISize requestedSize, UISize size)
        {
            if (size == UISize.Min)
                return requestedSize;

            if (requestedSize.Width < size.Width)
                requestedSize.Width = size.Width;
            if (requestedSize.Height < size.Height)
                requestedSize.Height = size.Height;

            return requestedSize;
        }

        private static UISize ConstrainOver(UISize requestedSize, UISize size)
        {
            if (size == UISize.Min)
                return requestedSize;

            if (requestedSize.Width > size.Width)
                requestedSize.Width = size.Width;
            if (requestedSize.Height > size.Height)
                requestedSize.Height = size.Height;

            return requestedSize;
        }
    }

    public class ViewPaddingCalculation
    {
        public static UISize AdjustForPadding(LayoutOptions horizontalLayout, LayoutOptions verticalLayout, UIPadding padding, UISize size)
        {
            var flexibleWidth = horizontalLayout == LayoutOptions.Expand;
            var flexibleHeight = verticalLayout == LayoutOptions.Expand;

            return UISize.Of(
                flexibleWidth ? size.Width + padding.Right + padding.Left : size.Width,
                flexibleHeight ? size.Height + padding.Bottom + padding.Top : size.Height);
        }
    }
}