using System;
using System.ComponentModel;
using System.Diagnostics;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    public class ListView : ItemsView
    {
        public ListView()
        {
            VerticalLayout = LayoutOptions.Fill;
            HorizontalLayout = LayoutOptions.Fill;
            Layout = new AdjacentLayout {Orientation = OrientationOptions.Vertical};
        }

        protected override ILayoutable CreateDefault(object item)
        {
            string text = null;
            if (item != null)
                text = item.ToString ();

            return new LabelCell {
                Text = text
            };
        }

        [NotNull]
        protected override ILayoutable CreateWith(DataTemplate template, object item)
        {
            if (template == null)
                return CreateDefault(item);

            var instance = Activator.CreateInstance(template.Type);
            var bindableObject = instance as IBindableObject;
            var layoutable = instance as ILayoutable;
            Debug.Assert(bindableObject != null, "Data Template type does not implement IBindableObject.");
            Debug.Assert(layoutable != null, "Data Template type does not implement ICell.");
            bindableObject.BindingContext = item as INotifyPropertyChanged;
            return layoutable;
        }
    }
}