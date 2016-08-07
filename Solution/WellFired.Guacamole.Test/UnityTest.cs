using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

namespace WellFired.Guacamole.Test
{
    [TestClass]
    public class UnityTest
    {
        [TestMethod]
        public void VectorTest()
        {
            var vector1 = Vector3.one;
            var vector2 = Vector3.one;
            var vector3 = vector1 + vector2;
            Assert.AreEqual(vector3, new Vector3(2.0f, 2.0f, 2.0f));
        }

        [TestMethod]
        public void VectorTest2()
        {
            var vector1 = Vector3.one;
            var vector2 = Vector3.one;
            var vector3 = vector1 + vector2;
            Assert.AreNotEqual(vector3, new Vector3(3.0f, 2.0f, 2.0f));
        }
    }
}
