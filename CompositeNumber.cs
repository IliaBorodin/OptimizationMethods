using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLab
{
    public class CompositeNumber
    {
        private long numerator;
        private long denominator;
        public const int REGULAR = 0, DECIMAL=1 ;

        public static int typeOfFraction = 0;// Тип дроби(0-обыкновенная, 1- десятичная)

        public CompositeNumber(long numerator, long denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be zero", nameof(denominator));
            }
            if (denominator < 0)
            {
                numerator *= -1;
                denominator *= -1;
            }
            this.numerator = numerator;
            this.denominator = denominator;
            reduce();
        }
        public void reduce()
        {
            long com = HelperClass.gcd(Math.Abs(numerator), Math.Abs(denominator));
            numerator /= com;
            denominator /= com;
        }


        public static CompositeNumber Parse(string str)
        {
            if (str == "")
            {
                throw new Exception();
            }
            if (str.Contains("/"))
            {
                
                int numerator = 0, i = 0, coef = 1;
                if (str[0] == '-')
                {
                    coef = -1;
                    ++i;
                }
                for (; i < str.Length && str[i] != '/'; ++i)
                {
                    numerator *= 10;
                    numerator += Int32.Parse(str[i] + "");
                }
                ++i;
                int denominator = 0;
                for (; i < str.Length; ++i)
                {
                    denominator *= 10;
                    denominator += Int32.Parse(str[i] + "");
                }
                if (denominator == 0)
                {
                    return new CompositeNumber(numerator, 1) * new CompositeNumber(coef, 1);
                }
                return new CompositeNumber(numerator, denominator) * new CompositeNumber(coef, 1);
            }
             if (str.Contains(","))
            {
                int num1 = 0, i = 0, coef = 1;
                if (str[0] == '-')
                {
                    coef = -1;
                    ++i;
                }
                for (; i < str.Length && str[i] != ','; ++i)
                {
                    num1 *= 10;
                    num1 += Int32.Parse(str[i] + "");
                }
                ++i;
                int num2 = 0; int count = 0;
                for (; i < str.Length; ++i)
                {
                    num2 *= 10;
                    num2 += Int32.Parse(str[i] + "");
                    ++count;
                }
                if (num2 == 0)
                {
                    return new CompositeNumber(num1, 1) * new CompositeNumber(coef, 1);
                }
                return new CompositeNumber(num1, 1) + new CompositeNumber(num2, count*10);
            }

            int num = 0, index = 0, k = 1;
            if (str[0] == '-')
            {
                k = -1;
                ++index;
            }
            for (; index < str.Length && str[index] != '/'; ++index)
            {
                num *= 10;
                num += Int32.Parse(str[index] + "");
            }
          
            return new CompositeNumber(num, 1) * new CompositeNumber(k, 1);
        } // Парсинг строковых чисел


        public static CompositeNumber Null = new CompositeNumber(0, 1);
        //Переопределение
        public static CompositeNumber operator +(CompositeNumber a)
        {
            a.reduce(); return a;
        }
        public static CompositeNumber operator -(CompositeNumber a)
        {
            CompositeNumber b = new CompositeNumber(-a.numerator, a.denominator);
            b.reduce();
            return b;
        }

        public static CompositeNumber operator +(CompositeNumber a, CompositeNumber b)
        {
            CompositeNumber c = new CompositeNumber(a.numerator * b.denominator + b.numerator * a.denominator, a.denominator * b.denominator);
            c.reduce();
            return c;
        }
        public static CompositeNumber operator -(CompositeNumber a, CompositeNumber b)
        {
            CompositeNumber c = a + (-b);
            c.reduce();
            return c;
        }
        public static CompositeNumber operator *(CompositeNumber a, CompositeNumber b)
        {
            CompositeNumber c = new CompositeNumber(a.numerator * b.numerator, a.denominator * b.denominator);
            c.reduce();
            return c;
        }
        public static CompositeNumber operator /(CompositeNumber a, CompositeNumber b)
        {
            if (b.numerator == 0)
            {
                throw new DivideByZeroException();
            }
            CompositeNumber c = new CompositeNumber(a.numerator * b.denominator, a.denominator * b.numerator);
            c.reduce();
            return c;
        }
        public static bool operator <(CompositeNumber a, CompositeNumber b)
            => ((double)a.numerator / (double)a.denominator - (double)b.numerator / (double)b.denominator) < 0;
        public static bool operator >(CompositeNumber a, CompositeNumber b)
            => ((double)b.numerator / (double)b.denominator - (double)a.numerator / (double)a.denominator) < 0;
        public static bool operator ==(CompositeNumber a, CompositeNumber b)
            => ((double)b.numerator / (double)b.denominator - (double)a.numerator / (double)a.denominator) == 0;
        public static bool operator !=(CompositeNumber a, CompositeNumber b)
            => ((double)b.numerator / (double)b.denominator - (double)a.numerator / (double)a.denominator) != 0;

        //Переопределение toString()
        public override string ToString()
        {
            if (typeOfFraction==DECIMAL)
            {
                double rez = (double)this.numerator /
                    (double)this.denominator;
                return rez.ToString();
            }

            if (denominator == 1)
            {
                return numerator.ToString();
            }
            if (numerator == 0)
            {
                return numerator.ToString();
            }

            long com = HelperClass.gcd(Math.Abs(numerator), Math.Abs(denominator));
            this.numerator /= com;
            this.denominator /= com;
            return numerator + "/" + denominator;
        }

        public Double GetDouble()
        {
            double num = (double)this.numerator / this.denominator;
            return num;
           
        }
        public float GetFloat()
        {
            float num = (float)this.numerator / this.denominator;
            return num;

        }

        public CompositeNumber getFractionalPart()
        {
            if(this == CompositeNumber.Null)
            {
                return CompositeNumber.Null;
            }else if(this > CompositeNumber.Null)
            {
                long numerator = this.numerator - (this.numerator / this.denominator) * this.denominator;
                long denomerator = this.denominator;
              CompositeNumber number = new CompositeNumber(numerator, denomerator);
                return number;
            }else 
            {
                long inverseNumerator = (this.numerator * -1);
                long numerator = this.denominator - (inverseNumerator - (inverseNumerator / this.denominator) * this.denominator);
                long denomerator = this.denominator;
                CompositeNumber number = new CompositeNumber(numerator, denomerator);
                return number;
            }
        }

        public long getNumerator()
        {
            return this.numerator;
        }

        public long getDenominator()
        {
            return this.denominator;
        }

    }
}
