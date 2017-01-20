using System.Collections;
using System.Collections.Generic;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public abstract partial class ItemsView : Layout
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

        private void AddCollection(IEnumerable items, int index)
        {
            foreach (var item in items)
            {
                var layoutable = CreateWith(ItemTemplate, item);
                Children.Insert(index, layoutable);
                index++;
            }
        }

        private void RemoveCollection(IEnumerable items)
        {
            foreach (var item in items)
            {
                var cell = _container[item];
                Children.Remove(cell);
                _container.Remove(item);
            }
        }

        private void ReplaceCollection(IList oldItems, IList newItems, int index)
        {
            for (var n = 0; n < oldItems.Count; n++)
            {
                var item = oldItems[n];
                var cell = _container[item];
                _container.Remove(item);
                Children.Remove(cell);
                Children.Insert(index, CreateWith(ItemTemplate, newItems[n]));
                index++;
            }
        }

        private void ResetCollection()
        {
            _container.Clear();
            Children.Clear();
        }
    }
}