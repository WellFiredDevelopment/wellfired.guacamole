using System;
using System.Diagnostics;

namespace WellFired.Guacamole.Data.Annotations
{
	[Conditional("JETBRAINS_ANNOTATIONS")]
	[AttributeUsage(AttributeTargets.All)]
	public sealed class UsedImplicitlyAttribute : Attribute
	{
	}
}