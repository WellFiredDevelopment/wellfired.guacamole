using WellFired.Guacamole.Event;

namespace WellFired.Guacamole
{
	public interface IApplication
	{
		void Teardown();
		bool IsRunning { get; }
		void RaiseEventFor(string controlId, IEvent raisedEvent);
		void Update();
	}
}