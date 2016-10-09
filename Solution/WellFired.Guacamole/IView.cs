using WellFired.Guacamole.Types;

namespace WellFired.Guacamole
{
	public interface IView
	{
		UIRect RectRequest { get; }
		string Id { get; set; }
	}
}