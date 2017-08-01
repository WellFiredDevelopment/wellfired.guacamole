using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.Styles
{
    internal static class TaskCell
    {
        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.Clear},
                new Setter {Property = Views.View.OutlineColorProperty, Value = UIColor.Clear},
            }
        };
    }
}