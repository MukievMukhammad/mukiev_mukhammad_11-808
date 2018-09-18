using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankPercent
{
    class Program
    {
        // Автор Мукиев Мухаммад
        // Пользователь должен ввести исходные данные с консоли — три числа через пробел:
        //исходную сумму, процентную ставку (в процентах) и срок вклада в месяцах.
        //Программа должна вывести накопившуюся сумму на момент окончания вклада.

        public static double Calculate(string userInput)
        {
            //Разделим строку на построки
            string[] s = userInput.Split(' ');
            // p - это сумма вклада
            double p = double.Parse(s[0]);
            // r - коэффицент = 1 + процент/100
            double r = double.Parse(s[1]) / 1200 +1 ;
            // k - количество месяцев
            double k = double.Parse(s[2]);
            // sum - это сумма счета на момент закрытия
            double sum = p * Math.Pow(r, k);
            return sum;
        }
        static void Main(string[] args)
        {
            /*// sum is quantity of money
            double sum = double.Parse(Console.ReadLine());
            // percent is annual percent
            double percent = double.Parse(Console.ReadLine());
            // month is quantity of month of account
            double month = double.Parse(Console.ReadLine());*/

            
            Console.WriteLine(Calculate(Console.ReadLine()));

        }
    }
}
