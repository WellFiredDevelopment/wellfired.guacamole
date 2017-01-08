using System;
using System.Threading.Tasks;
using WellFired.Guacamole.Event;

namespace WellFired.Guacamole.Automation.UnityEditor
{
	public class Automation : IAutomation
	{
		private IApplication _application;

		public IApplication LaunchWith(Func<IApplication> launch)
		{
			_application = launch();
			return _application;
		}

		public Task Click(string viewId)
		{
			_application.RaiseEventFor(viewId, new ClickEvent { Button = 0 });
			Device.ExecuteOnMainThread(_application.Update);
			return TaskEx.Delay(100); // Artificial delay to simulate typing
		}

		public Task Type(string viewId, char key)
		{
			_application.RaiseEventFor(viewId, new TypeEvent { Key = key });
			Device.ExecuteOnMainThread(_application.Update);
			return TaskEx.Delay(100); // Artificial delay to simulate typing
		}
	}
}