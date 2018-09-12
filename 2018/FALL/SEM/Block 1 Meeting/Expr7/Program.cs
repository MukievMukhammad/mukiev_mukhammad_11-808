using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expr7
{
    class Program
    {
        static void Main(string[] args)
        {
            // k is a coficent
            double k = double.Parse(Console.ReadLine());
            Console.WriteLine("line's parallel is {1," + k + "}");
            Console.WriteLine("line's perpendicular is {1," + 1 / k + "}");
        }
    }
}
