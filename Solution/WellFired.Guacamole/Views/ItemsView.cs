using System;
using System.Collections.Generic;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public abstract partial class ItemsView : View, ICanLayout
    {
        public IList<ILayoutable> Children { get; set; } = new List<ILayoutable>();

        public void DoLayout()
        {

        }

        private void BuildCollection()
        {
            var template = ItemTemplate.Type;
            foreach (var item in ItemSource)
            {
                // TODO : Create the object and Set The Binding Context.
                var view = Activator.CreateInstance(template) as View;
                //view.BindingContext = item;
                Children.Add(view);
            }
        }
    }
}