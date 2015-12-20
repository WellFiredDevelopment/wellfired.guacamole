using WellFired.Guacamole;

namespace WellFired.Guacamole
{
	public interface IApplication 
	{
		IWindow MainWindow 
		{
			get;
			set;
		}

		void Launch();
	}
}