using System;

namespace AngryBirds
{
	public static class AngryBirdsTask
	{
        //Это простой симулятор системы прицеливания.
        //В файле AngryBirdsTask реализуйте функцию расчета угла прицеливания, 
        //в зависимости от начальной скорости снаряда и дальности до цели. 
        // Автор Мукиев Мухаммад

        //  Ниже — это XML документация, её использует ваша среда разработки, 
        // чтобы показывать подсказки по использованию методов. 
        // Но писать её естественно не обязательно.
        /// <param name="v">Начальная скорость</param>
        /// <param name="distance">Расстояние до цели</param>
        /// <returns>Угол прицеливания в радианах от 0 до Pi/2</returns>
        public static double FindSightAngle(double v, double distance)
		{
            double angle = Math.Asin((distance * 9.8) / (v * v))/2;
            if ((distance / v) < ((v * Math.Sin(angle)) / 9.8))
            {
                return 0;
            }
            else
            {
                return angle;
            }
		}
	}
}