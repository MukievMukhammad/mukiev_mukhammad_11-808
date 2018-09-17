using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expr13
{
    class Program
    {
        static void Main(string[] args)
        {
            // Автор Мукиев Мухаммад
            //задание: Козла пустили в квадратный огород и привязали к колышку. Колышек воткнули точно в центре огорода.
            //Козёл ест всё, до чего дотянется, не перелезая через забор огорода и не разрывая веревку.
            //Какая площадь огорода будет объедена? Даны длина веревки и размеры огорода.

            //введем значение длины радиуса и стороны квадрата  
            int r = int.Parse(Console.ReadLine());
            int length = int.Parse(Console.ReadLine());
            //рассмотрим 3 случаи, когда окужность внутри квадрата, когда окружность снаржде квадрата
            // и когда окружность между
            if (r<=length/2)
                Console.WriteLine(Math.PI * r * r);
            if (r >= (Math.Sqrt(2) / 2 * length))
                Console.WriteLine(length * length);
            else
            {
                double s=((Math.PI*r*r)/360)*2 * Math.Acos(length / (2 * r));
                double s1 = Math.Sqrt((r * r) - (length * length) / 2)*(length/2);
                Console.WriteLine(s - s1);
            }


        }
    }
}
