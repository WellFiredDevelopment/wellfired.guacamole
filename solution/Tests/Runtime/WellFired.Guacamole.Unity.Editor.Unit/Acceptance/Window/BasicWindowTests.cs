using System.Threading.Tasks;
using NUnit.Framework;
using WellFired.Guacamole.Automation;
using WellFired.Guacamole.Unity.Editor.Unit.Applications.Empty;

namespace WellFired.Guacamole.Unity.Editor.Unit.Acceptance.Window
{
	[TestFixture]
	public class BasicWindowTests
	{
		private IApplication _application;
		private IAutomation _automation;

		[SetUp]
		public void Setup()
		{
			_automation = new Automation.UnityEditor.Automation();
			_application = _automation.LaunchWith(EmptyApplication.Launch);
		}

		[Test]
		public void SimpleApplicationOpens()
		{
			Assert.That(_application.IsRunning);
			_application.Teardown();
		}

		[Test]
		public async Task TypingSomething()
		{
			await _automation.Click("AcceptanceLabel");
			await _automation.Type("AcceptanceLabel", 'G');
			await _automation.Type("AcceptanceLabel", 'u');
			await _automation.Type("AcceptanceLabel", 'a');
			await _automation.Type("AcceptanceLabel", 'c');
			await _automation.Type("AcceptanceLabel", 'a');
			await _automation.Type("AcceptanceLabel", 'm');
			await _automation.Type("AcceptanceLabel", 'o');
			await _automation.Type("AcceptanceLabel", 'l');
			await _automation.Type("AcceptanceLabel", 'e');
			
			await _automation.Type("AcceptanceLabel", " Is");
			await _automation.Type("AcceptanceLabel", " The Best!");
			
			_application.Teardown();

			Assert.That(_application.IsRunning);
		}
	}
}