using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expr8
{
    class Program
    {
        static void Main(string[] args)
        {
            // введем коэффиценты данной прямой 
            double k = double.Parse(Console.ReadLine());
            double b = double.Parse(Console.ReadLine());

            // введем координаты точки
            double X1 = double.Parse(Console.ReadLine());
            double Y1 = double.Parse(Console.ReadLine());

            // найдем коэффицент перпендикулярной прямой
            double B = Y1 + X1 / k;

            // найдем по формуле координаты пересечения двух прямых, а именно:
            // данной прямой и его перпендикуляра проходящего через введенную 
            //точку
            double x = ( (B-b)* k ) / (k*k + 1);
            double y = k * x + b;
            // answer
            Console.WriteLine("{" + x + "," + y + "}");
        }
    }
}
