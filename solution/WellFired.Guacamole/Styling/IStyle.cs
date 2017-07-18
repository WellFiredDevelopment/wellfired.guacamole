using System.Collections.Generic;

namespace WellFired.Guacamole.Styling
{
    public interface IStyle
    {
        IList<ISetter> Setters { get; }
        IList<ITrigger> Triggers { get; }
    }
}