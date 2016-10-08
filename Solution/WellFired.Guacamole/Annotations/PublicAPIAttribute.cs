using System;
using System.Diagnostics;

namespace WellFired.Guacamole.Annotations
{
    [MeansImplicitUse]
    [Conditional("JETBRAINS_ANNOTATIONS")]
    // ReSharper disable once InconsistentNaming
    public sealed class PublicAPIAttribute : Attribute
    {}
}
