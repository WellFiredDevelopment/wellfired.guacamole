namespace WellFired.Guacamole
{
	public interface IApplication
	{
		IWindow MainWindow { get; }
		void Teardown();
		bool IsRunning { get; }
		void Update();
	}
}