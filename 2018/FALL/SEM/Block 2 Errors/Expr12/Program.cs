using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expr12
{
    class Program
    {
        static void Main(string[] args)
        {
            // Автор Мукиев Мухаммад
            //Самолёт должен набрать высоту h метров в течение первых t секунд полёта и удерживать её в течение всего полёта. 
            //Разрешён набор высоты со скоростью не более чем v метров в секунду. До полного набора высоты запрещено снижаться.
            //Известно, что уши заложены в те и только те моменты времени, когда самолёт поднимается со скоростью более x метров в секунду.
            //Посчитайте минимальное и максимальное возможное время, в течение которого у пассажиров будут заложены уши. 
            //Считайте, что самолёт способен изменять скорость мгновенно.

            int high = int.Parse(Console.ReadLine());
            int time = int.Parse(Console.ReadLine());
            int v = int.Parse(Console.ReadLine());
            int x = int.Parse(Console.ReadLine());

            if (high - v * time < 0)
            {
                int mintime = (high - x * time) / (v - x);
                Console.WriteLine(mintime);
            }
            if (high - v * time == 0)
            {
                Console.WriteLine(0);
                Console.WriteLine(time);
            }

            if (high / time > x)
            {
                Console.WriteLine(time);
            }
            else
            {
                int maxtime = (high - time) / x;
                Console.WriteLine(maxtime);
            }
        }
    }
}
