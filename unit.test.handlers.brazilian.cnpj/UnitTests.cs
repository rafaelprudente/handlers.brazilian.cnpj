using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace handlers.brazilian
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestIsValidWithoutFormatting()
        {
            string cnpjToTest = "74184017000182";

            Assert.IsTrue(Cnpj.IsValid(cnpjToTest));
        }

        [TestMethod]
        public void TestIsValidWithFormatting()
        {
            string cnpjToTest = "74.184.017/0001-82";

            Assert.IsTrue(Cnpj.IsValid(cnpjToTest));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Invalid argument length [10].")]
        public void TestCheckSmallerNumber()
        {
            string cnpjToTest = "74.184.017/0001-8";

            Cnpj.Check(cnpjToTest);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Invalid argument length [11].")]
        public void TestCheckBiggerNumber()
        {
            string cnpjToTest = "174.184.017/0001-82";

            Cnpj.Check(cnpjToTest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid CNPJ number.")]
        public void TestCheckWrongNumber()
        {
            string cnpjToTest = "74.184.117/0001-82";

            Cnpj.Check(cnpjToTest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Value cannot be null.")]
        public void TestFormatWithValueNull()
        {
            string cnpjToTest = null;

            Cnpj.Format(cnpjToTest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Value cannot be empty.")]
        public void TestFormatWithValueEmpty()
        {
            string cnpjToTest = "";

            Cnpj.Format(cnpjToTest);
        }

        [TestMethod]
        public void TestFormat1()
        {
            string cnpjToTest = "1";

            Assert.AreEqual("00.000.000/0000-01", Cnpj.Format(cnpjToTest));
        }

        [TestMethod]
        public void TestFormat2()
        {
            string cnpjToTest = "74184017000182";

            Assert.AreEqual("74.184.017/0001-82", Cnpj.Format(cnpjToTest));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid CNPJ number.")]
        public void TestFormat3()
        {
            string cnpjToTest = "74184117000182";

            Cnpj.Format(cnpjToTest, true);
        }
    }
}
