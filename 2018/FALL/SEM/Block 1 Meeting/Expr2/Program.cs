using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expr2
{
    class Program
    {
        static void Main(string[] args)
        {
            // input our Number
            int Number = int.Parse(Console.ReadLine());
            // разбить трехзначное число  на цыфры
            int a = Number % 10;
            int b = Number % 100;
            int c = Number % 1000;
            // выводить ответ
            Console.WriteLine(100 * a + 10 * b + c);

        }
    }
}
