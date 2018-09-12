using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expr5
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());

            // находим количество чисел кратные 4
            // для числа а
            int da = a / 4;
            // для числа b
            int db = b / 4;

            int da100 = a / 100;
            int db100 = b / 100;

            int da400 = a / 400;
            int db400 = b / 400;

            int r = da - da100 + da400;
            int t = db - db100 + db400;

            Console.WriteLine(Math.Abs(t-r));
        }
    }
}
