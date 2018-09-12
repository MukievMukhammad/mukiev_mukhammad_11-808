using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expr6
{
    class Program
    {
        static void Main(string[] args)
        {
            //введем шесть переменных, так как даны три точки
            // первые четыре из них - это координаты концов отрезка
            // (a,b) & (c,d)
            int a, b, c, d;

            a = int.Parse(Console.ReadLine());
            b = int.Parse(Console.ReadLine());
            c = int.Parse(Console.ReadLine());
            d = int.Parse(Console.ReadLine());

            // введем координаты заданной точки
            int e, f;
            e = int.Parse(Console.ReadLine());
            f = int.Parse(Console.ReadLine());

            // вычислим по формуле квадрат длины
            double distans = (e - a) * (e - a) + (f - b) * (f - b) - ((c - a) * (c - a) + (d - b) * (d - b)) / 4;
            // найдем саму длину
            distans = Math.Sqrt(distans);

            Console.WriteLine(distans);
        }
    }
}
