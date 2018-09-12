using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expr3
{
    class Program
    {
        static void Main(string[] args)
        {
            // input a time (hours)
            int time = int.Parse(Console.ReadLine());
            // find what time is it (in a.m./p.m.)
            int a = time % 12;
            
            if (a > 6)
            {
                Console.WriteLine(360 - (a * 30));
            }
            else
            {
                Console.WriteLine(a * 30);
            }
            
        }
    }
}
