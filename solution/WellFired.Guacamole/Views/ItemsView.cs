using System.Collections;
using System.Collections.Generic;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public abstract partial class ItemsView : LayoutView, IItemsView
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
                OnAdd(layoutable);
                Children.Insert(index, layoutable);
                index++;
            }
        }

        private void RemoveCollection(IEnumerable items)
        {
            foreach (var item in items)
            {
                var layoutable = _container[item];
                OnRemove(layoutable);
                Children.Remove(layoutable);
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
            foreach(var layoutable in Children)
                OnRemove(layoutable);
            _container.Clear();
            Children.Clear();
        }
    }
}