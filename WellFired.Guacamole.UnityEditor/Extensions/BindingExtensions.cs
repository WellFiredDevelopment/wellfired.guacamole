using UnityEngine;

namespace WellFired.Guacamole.Databinding.Unity.Editor
{
    public static class BindingExtensions
    {
        public static T FindInParent<T>(this GameObject obj) where T : Component
        {
            var t = obj.transform;

            for (var i = 0; i < 25; i++)
            {
                if (t == null)
					return default(T);

                var found = t.GetComponent<T>();

                if (found != null)
                    return found;

                if (t.parent == null)
					return default(T);

                t = t.parent;
            }

			return default(T);
        }
    }
}