using System;
using System.Text.RegularExpressions;
using System.Linq;
using DemoLibrary;

namespace ConsoleUI
{
    delegate double Avg(int num1, int num2, int num3);

    class MainClass
    {
        private static Func<double, double, double> Add = (num1, num2) => num1 + num2;
        private static Func<double, double, double> Sub = (num1, num2) => num1 - num2;
        private static Func<double, double, double> Mul = (num1, num2) => num1 * num2;
        private static Func<double, double, double> Div = (num1, num2) =>
        {
            if (num2 == 0)
            {
                Console.WriteLine("Нельзя (а точнее невозможно) делить на ноль.");
                Environment.Exit(1);
            }
            return num1 / num2;
        };

        public static void Main(string[] args)
        {
            Avg task1 = delegate (int num1, int num2, int num3) { return (num1 + num2 + num3) / 3.0; };

            // Test.
            Console.WriteLine(task1(2, 4, 5));
            Task2();
        }

        private static void Task2()
        {
            while (true)
            {
                Console.Write("Пишите пример: \t");
                string usrInput = Console.ReadLine();
                if (usrInput == "exit") break;
                Match match = Regex.Match(usrInput, @"(?x) # флаг
^\s*                 # начало
([-+]?\d+([.,]\d+)?) # первое
\s*
([-+*/])             # знак
\s*
([-+]?\d+([.,]\d+)?) # второе
\s*$                 # конец
");
                if (!match.Success) continue;
                double num1 = Convert.ToDouble(match.Groups[1].Value.Replace('.', ','));
                char oper = Convert.ToChar(match.Groups[3].Value);
                double num2 = Convert.ToDouble(match.Groups[4].Value.Replace('.', ','));
                switch (oper)
                {
                    case '+':
                        Console.WriteLine("Решение: \t" + Add(num1, num2));
                        break;
                    case '-':
                        Console.WriteLine("Решение: \t" + Sub(num1, num2));
                        break;
                    case '*':
                        Console.WriteLine("Решение: \t" + Mul(num1, num2));
                        break;
                    case '/':
                        Console.WriteLine("Решение: \t" + Div(num1, num2));
                        break;
                }
            }
        }
    }
}
