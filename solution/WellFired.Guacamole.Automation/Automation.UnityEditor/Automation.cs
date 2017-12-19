using System;
using System.Threading.Tasks;
using WellFired.Guacamole.Automation.Extensions;
using WellFired.Guacamole.Event;
using WellFired.Guacamole.Platform;

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

		public async Task Click(string viewId)
		{
			_application.RaiseEventFor(viewId, new ClickEvent { Button = 0 });
			MainThreadRunner.ExecuteOnMainThread(_application.Update);
			await TaskEx.Delay(100); // Artificial delay to simulate typing
		}

		public async Task Type(string viewId, char key)
		{
			_application.RaiseEventFor(viewId, new TypeEvent { Key = key });
			MainThreadRunner.ExecuteOnMainThread(_application.Update);
			await TaskEx.Delay(100); // Artificial delay to simulate typing
		}

		public async Task Type(string viewId, string message)
		{
			foreach (var character in message)
				await Type(viewId, character);
		}
	}
}