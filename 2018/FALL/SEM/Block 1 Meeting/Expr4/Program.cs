using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expr4
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            int X = int.Parse(Console.ReadLine());
            int Y = int.Parse(Console.ReadLine());

            Console.WriteLine( (N/X) + (N/Y) - (N/(X*Y)) );
        }
    }
}
