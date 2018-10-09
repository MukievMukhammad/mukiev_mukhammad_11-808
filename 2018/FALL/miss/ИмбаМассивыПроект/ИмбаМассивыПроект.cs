using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            /*
             Марсианские наконец смогли вычислить точную продолжительность жизни одного человека.
             К счастью она у всех людей совпала.
             В связи с тем, что эта продолжительность равна 1000 лет, люди перестали размножаться размножаться.
             На основе приведенных данных составить гистограмму, которая показывает смертность людей по года
             Соответственно, исходя из этой Гистограммы вы узнаете, когда исчезнетколония на Марсе и в каком году будет
             пик смертности
           */


            Console.WriteLine("Статистика смертности по годам");
            // Узнаем начало и конец вымирания марсианской коллонии
            var minYear = int.MaxValue;
            var maxYear = int.MinValue;
            foreach (var name1 in names)
            {
                minYear = Math.Min(minYear, name1.BirthDate.Year);
                maxYear = Math.Max(maxYear, name1.BirthDate.Year);
            }
            // Выпишем все года смерти по Иксу
            var years = new string[maxYear - minYear + 1];
            for (var y = 0; y < years.Length; y++)
                years[y] = (y + minYear+1001).ToString();
            // Посчитаем кол-во лоюдей, которые умрут в каждом году
            var dethCount = new double[maxYear - minYear + 1];
            foreach (var name1 in names)
                dethCount[name1.BirthDate.Year - minYear]++;

            return new HistogramData("Смертность по годам", years, dethCount);
        }
    }
}