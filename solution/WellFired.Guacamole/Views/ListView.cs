using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    public partial class ListView : ItemsView
    {
        private int TotalContentSize { get; set; }
        
        public ListView()
        {
            VerticalLayout = LayoutOptions.Fill;
            HorizontalLayout = LayoutOptions.Fill;
            Layout = AdjacentLayout.Of(OrientationOptions.Vertical);
        }

        protected override ILayoutable CreateDefault(object item)
        {
            string text = null;
            if (item != null)
                text = item.ToString();

            return new LabelCell {
                Text = text
            };
        }

        protected override void OnRemove(ILayoutable item)
        {
            RebuildViewBounds();
        }

        protected override void OnAdd(ILayoutable item)
        {
            RebuildViewBounds();
        }

        private void RebuildViewBounds()
        {
            TotalContentSize = Children.Count * EntrySize;
        }

        [NotNull]
        protected override ILayoutable CreateWith(DataTemplate template, object item)
        {
            if (template == null)
                return CreateDefault(item);

            var instance = Activator.CreateInstance(template.Type);
            var bindableObject = instance as IBindableObject;
            var layoutable = instance as ILayoutable;
            var cell = instance as ICell;
            Debug.Assert(bindableObject != null, "Data Template type does not implement IBindableObject.");
            Debug.Assert(layoutable != null, "Data Template type does not implement ICell.");
            Debug.Assert(cell != null, "cell != null");
            bindableObject.BindingContext = item as INotifyPropertyChanged;
            cell.Container = this;
            
            return layoutable;
        }
    }
}