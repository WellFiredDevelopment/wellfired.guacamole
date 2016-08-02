﻿using System;
using System.Diagnostics;

namespace WellFired.Guacamole.Annotations
{
    [Conditional("JETBRAINS_ANNOTATIONS")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = false, Inherited = true)]
    public sealed class CanBeNullAttribute : Attribute
    {
    }
}