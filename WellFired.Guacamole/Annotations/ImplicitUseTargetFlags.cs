using System;

namespace WellFired.Guacamole.Annotations
{
    [Flags]
    public enum ImplicitUseTargetFlags
    {
        Default = 1,
        Itself = Default,
        Members = 2,
        WithMembers = Members | Itself,
    }
}
