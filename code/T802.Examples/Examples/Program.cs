using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            //OddOrEven();
            //CheckIfAVowel();
            //CheckIfALeapYear();
            //FloydsTriangle();
            //AddMatrices();
            //NarcissisticNumber();
            //TrabbPardoKnuth();
            //QuickSort();
            //Zeckendorf();
        }

        static void HelloWorld()
        {
            Console.WriteLine("Hello, world");
        }

        static void OddOrEven()
        {
            Console.WriteLine("Enter a number: ");
            string parameter = Console.ReadLine();
            int numberToBeEvaluated = int.Parse(parameter);
            if (numberToBeEvaluated % 2 == 0)
            {
                Console.WriteLine("{0} is even", numberToBeEvaluated);
            }
            else
            {
                Console.WriteLine("{0} is odd", numberToBeEvaluated);
            }
        }

        static void CheckIfAVowel()
        {
            Console.WriteLine("Enter a character");
            string parameter = Console.ReadLine();
            char ch = parameter[0];
            if (ch == 'a' || ch == 'A'
                || ch == 'e' || ch == 'E'
                || ch == 'i' || ch == 'I'
                || ch == 'o' || ch == 'O'
                || ch == 'u' || ch == 'U')
                Console.WriteLine(ch + " is a vowel");
            else
                Console.WriteLine(ch + " is not a vowel");
        }

        static void CheckIfALeapYear()
        {
            Console.WriteLine("Enter a year to check if it is a leap year");
            string parameter = Console.ReadLine();
            int year = int.Parse(parameter);
            if (year % 400 == 0)
                Console.WriteLine(year.ToString() + " is a leap year");
            else if (year % 100 == 0)
                Console.WriteLine(year.ToString() + " is not a leap year");
            else if (year % 4 == 0)
                Console.WriteLine(year.ToString() + " is a leap year");
            else
                Console.WriteLine(year.ToString() + " is not a leap year");
        }

        static void FloydsTriangle()
        {
            int n, num = 1, c, d;
            Console.WriteLine("Enter the number of rows of floyd's triangle you want");
            string rows = Console.ReadLine();
            n = int.Parse(rows);
            Console.WriteLine("Floyd's triangle :-");
            for (c = 1; c <= n; c++)
            {
                for (d = 1; d <= c; d++)
                {
                    Console.Write(num + " ");
                    num++;
                }
                Console.WriteLine(Environment.NewLine);
            }
        }

        static void AddMatrices()
        {
            int m, n, c, d;

            Console.WriteLine("Enter the number of rows and columns of matrix");
            m = int.Parse(Console.ReadLine());
            n = int.Parse(Console.ReadLine());

            int[,] first = new int[m, n];
            int[,] second = new int[m, n];
            int[,] sum = new int[m, n];

            Console.WriteLine("Enter the elements of first matrix");

            for (c = 0; c < m; c++)
            {
                for (d = 0; d < n; d++)
                {
                    first[c, d] = int.Parse(Console.ReadLine());
                }
            }

            Console.WriteLine("Enter the elements of second matrix");

            for (c = 0; c < m; c++)
            {
                for (d = 0; d < n; d++)
                {
                    second[c, d] = int.Parse(Console.ReadLine());
                }
            }

            for (c = 0; c < m; c++)
            {
                for (d = 0; d < n; d++)
                {
                    sum[c, d] = first[c, d] + second[c, d];
                }
            }

            Console.WriteLine("Sum of entered matrices:-");

            for (c = 0; c < m; c++)
            {
                for (d = 0; d < n; d++)
                {
                    Console.WriteLine(sum[c, d] + "\t");
                }
                Console.WriteLine();
            }
        }

        static void NarcissisticNumber()
        {
            int n, sum = 0, temp, remainder, digits = 0;

            Console.WriteLine("Input a number to check if it is an narcissistic number");
            string parameter = Console.ReadLine();
            n = int.Parse(parameter);

            temp = n;

            while (temp != 0)
            {
                digits++;
                temp = temp / 10;
            }

            temp = n;

            while (temp != 0)
            {
                remainder = temp % 10;
                sum = sum + Power(remainder, digits);
                temp = temp / 10;
            }

            if (n == sum)
            {
                Console.WriteLine(n + " is an narcissistic number.");
            }
            else
            {
                Console.WriteLine(n + " is not an narcissistic number.");
            }
        }

        static void TrabbPardoKnuth()
        {
            double check = 400, result;
            double[] inputs = new double[11];
            int i;

            Console.WriteLine("\nPlease enter 11 numbers :");

            for (i = 0; i < 11; i++)
            {
                Console.WriteLine("{0}:\n", i);
                string number = Console.ReadLine();
                inputs[i] = double.Parse(number);
            }

            Console.WriteLine("\n\n\nEvaluating f(x) = |x|^0.5 + 5x^3 for the given inputs :");

            for (i = 10; i >= 0; i--)
            {
                result = Math.Sqrt(Fabs(inputs[i])) + 5 * Math.Pow(inputs[i], 3);

                Console.WriteLine("\nf({0}) = ", i);

                if (result > check)
                {
                    Console.WriteLine("Overflow!");
                }
                else
                {
                    Console.WriteLine("{0}", result);
                }
            }
        }

        static void QuickSort()
        {
            // Create an unsorted array of string elements
            string[] unsorted = { "z", "e", "x", "c", "m", "q", "a" };

            // Print the unsorted array
            for (int i = 0; i < unsorted.Length; i++)
            {
                Console.Write(unsorted[i] + " ");
            }

            Console.WriteLine();

            // Sort the array
            QuickSort(unsorted, 0, unsorted.Length - 1);

            // Print the sorted array
            for (int i = 0; i < unsorted.Length; i++)
            {
                Console.Write(unsorted[i] + " ");
            }

            Console.WriteLine();

            Console.ReadLine();
        }

        public static void QuickSort(IComparable[] elements, int left, int right)
        {
            int i = left, j = right;
            IComparable pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    IComparable tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                QuickSort(elements, left, j);
            }

            if (i < right)
            {
                QuickSort(elements, i, right);
            }
        }

        static double Fabs(double n)
        {
            return Math.Sqrt(Math.Abs(n)) + 5 * n * n * n;
        }

        static int Power(int n, int r)
        {
            int c, p = 1;

            for (c = 1; c <= r; c++)
                p = p * n;

            return p;
        }

        static void Zeckendorf()
        {
            for (uint i = 1; i <= 20; i++)
            {
                string zeckendorfRepresentation = Zeckendorf(i);
                Console.WriteLine(string.Format("{0} : {1}", i, zeckendorfRepresentation));
            }
            Console.ReadKey();
        }

        private static string Zeckendorf(uint num)
        {
            IList<uint> fibonacciNumbers = new List<uint>();
            uint fibPosition = 2;

            uint currentFibonaciNum = Fibonacci(fibPosition);

            do
            {
                fibonacciNumbers.Add(currentFibonaciNum);
                currentFibonaciNum = Fibonacci(++fibPosition);
            } while (currentFibonaciNum <= num);

            uint temp = num;
            StringBuilder output = new StringBuilder();

            foreach (uint item in fibonacciNumbers.Reverse())
            {
                if (item <= temp)
                {
                    output.Append("1");
                    temp -= item;
                }
                else
                {
                    output.Append("0");
                }
            }

            return output.ToString();
        }

        static uint Fibonacci(uint n)
        {
            if (n < 2)
            {
                return n;
            }
            else
            {
                return Fibonacci(n - 1) + Fibonacci(n - 2);
            }
        }
    }
}
