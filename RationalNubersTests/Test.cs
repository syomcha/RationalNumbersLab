using System;
using NUnit.Framework;
using RationalNumbersLab;

namespace RationalNubersTest
{
    [TestFixture]
    internal class Test
    {
        [TestCase("1/2", "1/4", "3/4")]
        [TestCase("-3/-7", "-5/9", "-8/63")]
        [TestCase("22/9", "1/10", "229/90")]
        [TestCase("1/22222", "22222/3", "493817287/66666")]
        [TestCase("6/9", "9/6", "13/6")]
        [TestCase("0/4", "3/992", "3/992")]
        [TestCase("19000013/20994343", "19000087/20994959", "797798837442308/440775370516937")]
        public void Addition(string strNum1, string strNum2, string strExpected)
        {
            var num1 = Rational.Parse(strNum1);
            var num2 = Rational.Parse(strNum2);

            var actual = Rational.Add(num1, num2);

            Assert.AreEqual(strExpected, actual.ToString());
        }

        [TestCase("0/10", "4/7", "-4/7")]
        [TestCase("13/7", "23/13", "8/91")]
        [TestCase("3/4", "1/2", "1/4")]
        [TestCase("3/7", "0/6", "3/7")]
        [TestCase("593183192498/136876498436",
            "974314672431/743983179463",
            "153978768410750592725329/50916906250088813409934")]
        public void Substract(string strNum1, string strNum2, string strExpected)
        {
            var num1 = Rational.Parse(strNum1);
            var num2 = Rational.Parse(strNum2);

            var actual = Rational.Subtract(num1, num2);

            Assert.AreEqual(strExpected, actual.ToString());
        }

        [TestCase("0/1", "3/2", "0")]
        [TestCase("1/4", "0/4", "0")]
        [TestCase("1/2", "3/42", "1/28")]
        [TestCase("27/4", "17/5", "459/20")]
        [TestCase("-4/3", "5/8", "-5/6")]
        [TestCase("4/7", "-1/3", "-4/21")]
        public void Multiply(string strNum1, string strNum2, string strExpected)
        {
            var num1 = Rational.Parse(strNum1);
            var num2 = Rational.Parse(strNum2);

            var actual = Rational.Multiply(num1, num2);

            Assert.AreEqual(strExpected, actual.ToString());
        }

        [TestCase("2/3", "2/3", "1")]
        [TestCase("2/3", "2/3", "1")]
        [TestCase("3/8", "4/27", "81/32")]
        [TestCase("4/27", "3/8", "32/81")]
        public void Division(string strNum1, string strNum2, string strExpected)
        {
            var num1 = Rational.Parse(strNum1);
            var num2 = Rational.Parse(strNum2);

            var actual = Rational.Divide(num1, num2);

            Assert.AreEqual(strExpected, actual.ToString());
        }

        [TestCase("1/3", "0.(3)")]
        [TestCase("5/11", "0.(45)")]
        [TestCase("15/13", "1.(153846)")]
        [TestCase("471/900", "0.52(3)")]
        [TestCase("1/4", "0.25")]
        public void RationalToDecimal(string strNumber, string expected)
        {
            var rational = Rational.Parse(strNumber);

            var actual = rational.ConvertToDecimal();

            Assert.AreEqual(expected, actual);
        }

        [TestCase("0.(3)", "1/3")]
        [TestCase("0.(45)", "5/11")]
        [TestCase("1.(153846)", "15/13")]
        [TestCase("0.52(3)", "157/300")]
        [TestCase("0.25", "1/4")]
        public void DecimalToRational(string strNumber, string expected)
        {
            var actual = Rational.ConvertToRational(strNumber);

            Assert.AreEqual(expected, actual.ToString());
        }
    }
}