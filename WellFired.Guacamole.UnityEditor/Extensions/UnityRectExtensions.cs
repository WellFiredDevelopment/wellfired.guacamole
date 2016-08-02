using WellFired.Guacamole;

namespace WellFired.Guacamole.Unity.Editor
{
	public static class UnityRectExtensions 
	{
	    // ReSharper disable once InconsistentNaming
		public static UIRect ToUIRect(this UnityEngine.Rect source)
		{
			return new UIRect((int)source.x, (int)source.y, (int)source.width, (int)source.height);
		}
	}
}