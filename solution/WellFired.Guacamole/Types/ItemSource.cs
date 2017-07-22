using System.Collections;
using System.Linq;
using WellFired.Guacamole.Views.BindingContexts;

namespace WellFired.Guacamole.Types
{
    public static class ItemSource
    {
        public static IList From(params string [] collection)
        {
            return collection.Select(o => new LabelCellBindingContext(o)).ToList();
        }
    }
}