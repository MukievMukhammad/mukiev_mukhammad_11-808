
using System;
using System.Diagnostics;
using System.Drawing;

namespace RefactorMe
{
    // ## Прочитайте! ##
    //
    // Ваша задача привести код в этом файле в порядок. 
    // Для начала запустите эту программу. Для этого в VS в проект подключите сборку System.Drawing.

    // Переименуйте всё, что называется неправильно. Это можно делать комбинацией клавиш Ctrl+R, Ctrl+R (дважды нажать Ctrl+R).
    // Повторяющиеся части кода вынесите во вспомогательные методы. Это можно сделать выделив несколько строк кода и нажав Ctrl+R, Ctrl+M
    // Избавьтесь от всех зашитых в коде числовых констант — положите их в переменные с понятными именами.
    // 
    // После наведения порядка проверьте, что ваш код стал лучше. 
    // На сколько проще после ваших переделок стало изменить размер фигуры? Повернуть её на некоторый угол? 
    // Научиться рисовать невозможный треугольник, вместо квадрата?

    class Risovatel
    {
        static Bitmap image = new Bitmap(800, 600);
        static float x, y;
        static Graphics graphics;

        public static void Initialize()
        {
            image = new Bitmap(800, 600);
            graphics = Graphics.FromImage(image);
        }

        public static void Set_pos(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void Go(double l, double angle)
        {
            //Делает шаг длиной L в направлении angle и рисует пройденную траекторию
            var x1 = (float)(x + l * Math.Cos(angle));
            var y1 = (float)(y + l * Math.Sin(angle));
            graphics.DrawLine(Pens.Black, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void ShowResult()
        {
            image.Save("result.bmp");
            Process.Start("result.bmp");
        }
    }

    public class StrangeThing
    {
        public static void Main()
        {
            Risovatel.Initialize();
           
            //Рисуем четыре одинаковые части невозможного квадрата.
            // Часть первая:
            Risovatel.Set_pos(10, 0);// 0, Math.PI / 4, Math.PI, Math.PI / 2
            PartOfSquare(0, Math.PI / 4, Math.PI, Math.PI / 2);

            // Часть вторая:
            Risovatel.Set_pos(120, 10);
            PartOfSquare(Math.PI / 2, 3 * Math.PI / 4, 3 * Math.PI / 2, Math.PI);
            
            // Часть третья:
            Risovatel.Set_pos(110, 120);
            PartOfSquare(Math.PI, Math.PI + Math.PI / 4, 2 * Math.PI, Math.PI + Math.PI / 2);
            
            // Часть четвертая:
            Risovatel.Set_pos(0, 110);
            PartOfSquare(-Math.PI / 2, -Math.PI / 2 + Math.PI / 4, -Math.PI / 2 + Math.PI, -Math.PI / 2 + Math.PI / 2);
           
            Risovatel.ShowResult();
        }

        private static void PartOfSquare(double angle1, double angle2, double angle3, double angle4)
        {
            Risovatel.Go(100, angle1);
            Risovatel.Go(10 * Math.Sqrt(2), angle2);
            Risovatel.Go(100, angle3);
            Risovatel.Go(100 - (double)10, angle4);
        }
    }
}