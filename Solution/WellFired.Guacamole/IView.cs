using WellFired.Guacamole.Types;

namespace WellFired.Guacamole
{
	public interface IView
	{
	    UIPadding Padding { get; }
	    UIRect RectRequest { get; }
		string Id { get; set; }
	}
}