using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exper1
{
    class Program
    {
        static void Main(string[] args)
        {
            int a, b, c;
            // intput our varyables
            a = int.Parse(Console.ReadLine());
            b = int.Parse(Console.ReadLine());

            c = a;
            a = b;
            b = c;
            Console.WriteLine(a);
            Console.WriteLine(b);

        }
    }
}
