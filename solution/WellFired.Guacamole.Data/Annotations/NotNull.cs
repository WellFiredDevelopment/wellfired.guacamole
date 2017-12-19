using System;
using System.Diagnostics;

namespace WellFired.Guacamole.Data.Annotations
{
	[Conditional("JETBRAINS_ANNOTATIONS")]
	[AttributeUsage(
		 AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter |
		 AttributeTargets.Delegate)]
	[UsedImplicitly]
	public sealed class NotNullAttribute : Attribute
	{
	}
}