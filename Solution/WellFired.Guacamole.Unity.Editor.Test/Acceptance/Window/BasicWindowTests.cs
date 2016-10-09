using System.Threading.Tasks;
using NUnit.Framework;
using WellFired.Guacamole.Automation;
using WellFired.Guacamole.Unity.Editor.Test.Applications.Empty;

namespace WellFired.Guacamole.Unity.Editor.Test.Acceptance.Window
{
	[TestFixture]
	public class BasicWindowTests
	{
		private IApplication _application;
		private IAutomation _automation;

		[SetUp]
		public void Setup()
		{
			_automation = new AutomationLauncher().Automation;
			_application = _automation.LaunchWith(EmptyApplication.Launch);
		}

		[TearDown]
		public void Teardown()
		{
			//_application.Teardown();
		}

		[Test]
		public void SimpleApplicationOpens()
		{
			Assert.That(_application.IsRunning);
		}

		[Test]
		public async Task SimpleButtonPressed()
		{
			await _automation.Click("AcceptanceLabel");
			await _automation.Type("AcceptanceLabel", 'D');
			await _automation.Type("AcceptanceLabel", 'a');
			await _automation.Type("AcceptanceLabel", 'n');
			await _automation.Type("AcceptanceLabel", 'n');
			await _automation.Type("AcceptanceLabel", 'y');
			await _automation.Type("AcceptanceLabel", 'I');
			await _automation.Type("AcceptanceLabel", 's');
			await _automation.Type("AcceptanceLabel", 'A');
			await _automation.Type("AcceptanceLabel", 'M');
			await _automation.Type("AcceptanceLabel", 'u');
			await _automation.Type("AcceptanceLabel", 'p');
			await _automation.Type("AcceptanceLabel", 'p');
			await _automation.Type("AcceptanceLabel", 'e');
			await _automation.Type("AcceptanceLabel", 't');
			await _automation.Type("AcceptanceLabel", '!');

			Assert.That(_application.IsRunning);
		}
	}
}