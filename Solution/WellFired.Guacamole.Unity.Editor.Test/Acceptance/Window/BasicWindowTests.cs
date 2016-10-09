using NUnit.Framework;
using WellFired.Guacamole.Unity.Editor.Test.Applications.Empty;

namespace WellFired.Guacamole.Unity.Editor.Test.Acceptance.Window
{
	[TestFixture]
	public class BasicWindowTests
	{
		private Application _application;

		[SetUp]
		public void Setup()
		{
			_application = EmptyApplication.Launch();
		}

		[TearDown]
		public void Teardown()
		{
			_application.Teardown();
		}

		[Test]
		public void SimpleApplicationOpens()
		{
			Assert.That(_application.IsRunning);
		}
	}
}