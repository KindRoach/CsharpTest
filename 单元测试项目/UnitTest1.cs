using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using 单元测试;

namespace 单元测试项目
{
    [TestClass]
    public class TestProgram
    {
        //[TestMethod]
        //public void TestTheAnswerToTheUltimateQuestionOfLifeTheUniverseAndEverythingthe()
        //{
        //    DeepThought f1 = new DeepThought();
        //    int expected = 42;
        //    int actual = f1.TheAnswerToTheUltimateQuestionOfLifeTheUniverseAndEverything();
        //    Assert.AreEqual(expected, actual);
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStringSampleNull()
        {
            StringSample sample = new StringSample(null);
        }

        [TestMethod]
        public void GetStringDemoAB()
        {
            string expected = "b not found in a";
            StringSample sample = new StringSample(String.Empty);
            string actual = sample.GetStringDemo("a", "b");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStringDemoABCDBC()
        {
            string expected = "removed bc from abcd: ad";
            StringSample sample = new StringSample(String.Empty);
            string actual = sample.GetStringDemo("abcd", "bc");
            Assert.AreEqual(expected, actual);
        }
    }
}
