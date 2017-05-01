using System;
using System.Diagnostics;

namespace WellFired.Guacamole.Annotations
{
	[Conditional("JETBRAINS_ANNOTATIONS")]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class MeansImplicitUseAttribute : Attribute
	{
		[PublicAPI]
		public MeansImplicitUseAttribute()
		{
		}
	}
}