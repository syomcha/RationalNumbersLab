using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

// ReSharper disable NotResolvedInText
namespace RationalNumbersLab
{
    public class Rational
    {
        public BigInteger P;
        public BigInteger Q;

        public Rational(BigInteger p, BigInteger q)
        {
            if (q == 0)
                throw new ArgumentNullException("Знаменатель не может быть равен 0.");

            if (p < 0 && q < 0)
            {
                P = BigInteger.Abs(p);
                Q = BigInteger.Abs(q);
                return;
            }

            if (p > 0 && q < 0)
            {
                P = -p;
                Q = BigInteger.Abs(q);
                return;
            }

            P = p;
            Q = q;
        }

        public static Rational Parse(string strRational)
        {
            try
            {
                var numbers = strRational.Split("/");
                return new Rational(BigInteger.Parse(numbers[0]), BigInteger.Parse(numbers[1]));
            }
            catch
            {
                throw new ArgumentException("Невозможно создать дробь из этой строки");
            }
        }

        public static Rational Add(Rational a, Rational b)
        {
            var newP = a.P * b.Q + a.Q * b.P;
            var newQ = a.Q * b.Q;

            var newRational = new Rational(newP, newQ);
            newRational.ReduceToLowestTerms();
            newRational.CheckSign();

            return newRational;
        }

        public static Rational Subtract(Rational a, Rational b)
        {
            var newP = a.P * b.Q - a.Q * b.P;
            var newQ = a.Q * b.Q;

            var newRational = new Rational(newP, newQ);
            newRational.ReduceToLowestTerms();
            newRational.CheckSign();

            return newRational;
        }

        public static Rational Multiply(Rational a, Rational b)
        {
            var newP = a.P * b.P;
            var newQ = a.Q * b.Q;

            var newRational = new Rational(newP, newQ);
            newRational.ReduceToLowestTerms();
            newRational.CheckSign();

            return newRational;
        }

        public static Rational Divide(Rational a, Rational b)
        {
            if (b.P == 0)
                throw new ArgumentException("Знаменатель не может быть равен 0.");

            var newP = a.P * b.Q;
            var newQ = a.Q * b.P;

            var newRational = new Rational(newP, newQ);
            newRational.ReduceToLowestTerms();
            newRational.CheckSign();

            return newRational;
        }

        public static Rational ConvertToRational(string strNumber)
        {
            var numbers = strNumber.Split(".");
            var y = BigInteger.Parse(numbers[0]);

            BigInteger k, m, a, b;
            if (!numbers[1].Contains("("))
            {
                a = BigInteger.Parse(numbers[1]);
                b = 0;

                k = 1;
                m = BigInteger.Pow(10, numbers[1].Length);
            }
            else
            {
                var left = numbers[1].IndexOf('(');
                var right = numbers[1].IndexOf(')');
                var str = numbers[1][..left] + numbers[1].Substring(left + 1, right - left - 1);

                a = BigInteger.Parse(str);

                b = left == 0 ? 0 : BigInteger.Parse(numbers[1][..left]);

                m = BigInteger.Pow(10, left);
                k = BigInteger.Pow(10, right - left - 1) - 1;
            }

            var q = k * m;
            var p = a - b;
            if (y > 0) p += y * q;

            var rational = new Rational(p, q);
            rational.ReduceToLowestTerms();

            return rational;
        }

        public string ConvertToDecimal()
        {
            var sb = new StringBuilder();
            sb.Append(P / Q);

            BigInteger newP;
            if (P / Q != 0)
                newP = P % Q;
            else
                newP = P;

            sb.Append('.');

            var modIndex = new Dictionary<BigInteger, int>();
            while (true)
            {
                var mod = newP % Q;
                if (mod == 0)
                    break;

                if (modIndex.ContainsKey(mod))
                {
                    sb.Insert(modIndex[mod], '(');
                    sb.Append(')');
                    break;
                }

                modIndex.Add(mod, sb.Length);

                newP *= 10;
                sb.Append(newP / Q);

                newP %= Q;
            }

            return sb.ToString();
        }

        public void ReduceToLowestTerms()
        {
            var gcd = BigInteger.GreatestCommonDivisor(P, Q);
            if (gcd != 1)
            {
                P /= gcd;
                Q /= gcd;
            }
        }

        public override bool Equals(object otherNum)
        {
            return otherNum != null && P == ((Rational) otherNum).P && Q == ((Rational) otherNum).Q;
        }

        private void CheckSign()
        {
            if (P < 0 && Q < 0)
            {
                P = BigInteger.Abs(P);
                Q = BigInteger.Abs(Q);
                return;
            }

            if (P <= 0 || Q >= 0) 
                return;

            P = -P;
            Q = BigInteger.Abs(Q);
        }

        public override string ToString()
        {
            if (P == Q)
                return "1";
            if (P == 0)
                return "0";
            return $"{P}/{Q}";
        }
    }
}