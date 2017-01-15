using System.Collections.Generic;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public abstract partial class ItemsView : Layout
    {
        protected ItemsView()
        {
            Children = new List<ILayoutable>();
        }

        private void BuildCollection()
        {
//            var template = ItemTemplate.Type;
            foreach (var item in ItemSource)
                Children.Add(CreateDefault(item) as ILayoutable);

            InvalidateRectRequest();
        }

        protected abstract ICell CreateDefault(object item);
    }
}