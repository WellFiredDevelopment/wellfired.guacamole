using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	[PublicAPI]
	public static class UnityColorExtensions 
	{
        // ReSharper disable once InconsistentNaming	
        [PublicAPI]
        public static Types.UIColor ToUIColor(this UnityEngine.Color source)
		{
			return Types.UIColor.FromRGB((int)(source.r * 255.0f), (int)(source.g * 255.0f), (int)(source.b * 255.0f));
		}
	}
}