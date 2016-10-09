using System;
using System.Threading.Tasks;

namespace WellFired.Guacamole.Automation
{
	public interface IAutomation
	{
		IApplication LaunchWith(Func<IApplication> launch);
		Task Click(string viewId);
		Task Type(string viewId, char key);
	}
}