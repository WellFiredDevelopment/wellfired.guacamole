using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    public abstract partial class ItemsView : View, IItemsView
    {
        private readonly Dictionary<object, ILayoutable> _container = new Dictionary<object, ILayoutable>();

        protected ItemsView()
        {
            Children = new List<ILayoutable>();
        }

        private void BuildCollection()
        {
            foreach (var item in ItemSource)
            {
                var cell = CreateWith(ItemTemplate, item);
                _container[item] = cell;
                Children.Add(cell);
                OnAdd(cell);
            }

            InvalidateRectRequest();
        }

        protected abstract ILayoutable CreateWith(DataTemplate template, object item);
        protected abstract ILayoutable CreateDefault(object item);

        protected abstract void OnAdd(ILayoutable item);
        protected abstract void OnRemove(ILayoutable item);

        private void AddCollection(IEnumerable items, int index)
        {
            foreach (var item in items)
            {
                var layoutable = CreateWith(ItemTemplate, item);
                Children.Insert(index, layoutable);
                OnAdd(layoutable);
                index++;
            }
        }

        private void RemoveCollection(IEnumerable items)
        {
            foreach (var item in items)
            {
                var layoutable = _container[item];
                Children.Remove(layoutable);
                OnRemove(layoutable);
                _container.Remove(item);
            }
        }

        private void ReplaceCollection(IList oldItems, IList newItems, int index)
        {
            for (var n = 0; n < oldItems.Count; n++)
            {
                var item = oldItems[n];
                var layoutable = _container[item];
                _container.Remove(item);
                Children.Remove(layoutable);
                Children.Insert(index, CreateWith(ItemTemplate, newItems[n]));
                index++;
            }
        }

        private void ResetCollection()
        {
            _container.Clear();
            Children.Clear();
            foreach(var layoutable in Children)
                OnRemove(layoutable);
        }

        public override void Render(UIRect parentRect)
        {
            base.Render(parentRect);

            var finalContentRect = FinalRenderRect;
            finalContentRect.X += ContentRectRequest.X;
            finalContentRect.Y += ContentRectRequest.Y;
            finalContentRect.Width = ContentRectRequest.Width;
            finalContentRect.Height = ContentRectRequest.Height;

            foreach (var child in Children)
                (child as View)?.Render(finalContentRect);
        }

        public override void InvalidateRectRequest()
        {
            base.InvalidateRectRequest();

            foreach (var child in Children)
                (child as View)?.InvalidateRectRequest();
        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (e.PropertyName == ChildrenProperty.PropertyName || 
                e.PropertyName == BindingContextProperty.PropertyName)
                SetupChildBindingContext();
        }

        private void SetupChildBindingContext()
        {
            foreach (var child in Children)
            {
                var view = child as View;
                if (view != null)
                    view.BindingContext = BindingContext;
            }
        }

        public override void SetStyleDictionary(IStyleDictionary styleDictionary)
        {
            base.SetStyleDictionary(styleDictionary);

            foreach (var child in Children)
            {
                var view = child as View;
                view?.SetStyleDictionary(styleDictionary);
            }
        }
    }
}