using System;
using NUnit.Framework;
using CaseStudyWebApp.Web.Account;
using System.Web;

using CaseStudyWebApp.Web.Calculator;

namespace CaseStudyWebApp.Test
{
    [TestFixture]
    public class MathOperationsTestFixture
    {
        [Test]
        public void AddTest()
        {
            double expected = 15;
            double actual = MathOperations.Add(5, 10);

            Assert.IsTrue(actual == expected, string.Format("Expected [{0}] but got [{1}]", expected, actual));
        }

        [Test]
        public void SubtractTest()
        {
            double expected = 5;
            double actual = MathOperations.Subtract(10, 5);

            Assert.IsTrue(actual == expected, string.Format("Expected [{0}] but got [{1}]", expected, actual));
        }

        [Test]
        public void MultiplicationTest()
        {
            double expected = 50;
            double actual = MathOperations.Multiply(5, 10);

            Assert.IsTrue(actual == expected, string.Format("Expected [{0}] but got [{1}]", expected, actual));
        }

        [Test]
        public void DivisionTest()
        {
            double expected = 2;
            double actual = MathOperations.Divide(10, 5);

            Assert.IsTrue(actual == expected, string.Format("Expected [{0}] but got [{1}]", expected, actual));
        }
    }
}
