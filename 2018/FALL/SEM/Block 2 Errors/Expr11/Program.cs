using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expr11
{
    class Program
    {
        static void Main(string[] args)
        {
	    // Автор Мукиев Мухаммад
	    // Дано время в часах и минутах. Найти угол от часовой к минутной стрелке на обычных часах.
            // вводим время
            int hours = int.Parse(Console.ReadLine());
            int minuts = int.Parse(Console.ReadLine());
            
            //найдем угол часовой стрелки
            int hangle = (hours * 60 + minuts) / 2;
            if (hangle > 360)
                hangle -= 360;
            //найдем угол минутной стрелки
            int mangle = minuts * 6;
            // выведим в качестве ответа модуль разности углов 
            Console.WriteLine(Math.Abs(hangle - mangle));
        }
    }
}
