using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expr10
{
    class Program
    {
        static void Main(string[] args)
        {
            // Задача: Найти сумму всех положительных чисел меньше 1000 кратных 3 или 5.
            // Автор: Мукиев Мухаммад
            //Сумма всех чисел до 1000 кратных 3 
            int S1 = ((3 + 999) / 2) * 333;
            //Сумма всех чисел до 1000 кратных 5 
            int S2 = ((5 + 995) / 2) * (995 / 5);
            //Сумма всех чисел до 1000 кратных 15 
            int S3 = ((15 + 990) / 2) * (990 / 15);

            //Сумма всех чисел до 1000 кратных 3 или 5
            int Sum = S1 + S2 - S3;
            Console.WriteLine(Sum);
        }
    }
}
