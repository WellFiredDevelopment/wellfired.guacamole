using NUnit.Framework;
using WellFired.Guacamole.Unity.Editor.Extensions;

namespace WellFired.Guacamole.Unity.Editor.Test.Unit
{
	[TestFixture]
	public class ReflectionTests
	{
		[Test]
		public void TestThatUnityDidntRemoveActiveEditor()
		{
			var activeEditor = EditorGUIExtensions.ActiveEditorFieldInfo;
			Assert.That(activeEditor, Is.Not.Null);
		}
		
		[Test]
		public void TestThatUnityDidntRemoveLastControlID()
		{
			var lastControlId = EditorGUIUtilityExtensions.LastControlIDFieldInfo;
			Assert.That(lastControlId, Is.Not.Null);
		}
	}
}