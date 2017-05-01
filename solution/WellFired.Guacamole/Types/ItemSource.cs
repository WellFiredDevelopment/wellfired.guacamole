using System.Collections;
using System.Linq;

namespace WellFired.Guacamole.Types
{
    public static class ItemSource
    {
        public static IEnumerable From(params string [] collection)
        {
            return collection.ToList();
        }
    }
}