using System.Collections;
using System.Collections.Generic;
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
    
    public static class ItemSource<T>
    {
        public static IEnumerable<T> From(params T [] collection)
        {
            return collection.ToList();
        }
    }
}