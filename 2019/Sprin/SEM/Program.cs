using System;
using System.Diagnostics;
using IntroSortOnArray;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Windows.Forms;

namespace Semestrovka
{
    class Program
    {
        static Random random = new Random();

        static int[] GenerateRandomArray(int length)
        {
            var array = new int[length];
            for (int i = 1; i < array.Length; i++)
                array[i] = random.Next();
            return array;
        }

        private static Chart MakeChart(Series introSort, Series line)
        {
            var chart = new Chart();
            chart.ChartAreas.Add(new ChartArea());

            introSort.ChartType = SeriesChartType.FastLine;
            introSort.Color = Color.Green;
            introSort.BorderWidth = 3;

            line.ChartType = SeriesChartType.FastLine;
            line.Color = Color.Red;
            line.BorderWidth = 3;

            chart.Series.Add(introSort);
            // chart.Series.Add(line);
            chart.Dock = DockStyle.Fill;
            return chart;
        }

        static void MeasureTime(int[] array, Action<int[]> sort, Series series)
        {
            var watch = new Stopwatch();
            watch.Start();
            sort(array);
            watch.Stop();
            series.Points.Add(new DataPoint(array.Length, (float)watch.ElapsedMilliseconds / 1000));
        }

        public static void Main()
        {
            var arraySort = new Series();
            var line = new Series();

            for (int i = 100; i <= 1000000; i *= 2)
            {
                GC.Collect();
                var array = GenerateRandomArray(i);
                MeasureTime(array, IntroSort.Sort, arraySort);
                line.Points.Add(new DataPoint(i, Math.Log(i)));
            }

            var chart = MakeChart(arraySort, line);
            var form = new Form();
            form.ClientSize = new Size(800, 600);
            form.Controls.Add(chart);
            Application.Run(form);
        }
    }
}
