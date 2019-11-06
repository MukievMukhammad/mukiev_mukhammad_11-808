using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyPhotoshop
{
	class MainClass
	{
        [STAThread]
		public static void Main (string[] args)
		{
			var window=new MainWindow();
			window.AddFilter (new AbstractFilter<LightenningParameters>(
				"Осветление/затемнение",
				(pixle, parameters) => pixle * parameters.Coefficient
				));
			window.AddFilter(new AbstractFilter<EmptyParameters>(
				"Оттенки серого",
				(original, parameners) =>
				{
					var lightness = (original.R + original.G + original.B) / 3;
					return new Pixle(lightness, lightness, lightness);
				}));
//			window.AddFilter(new TransformFilter(
//				"Отражение по горизонтали",
//				size=>size,
//				(point, size)=>new Point(size.Width - point.X - 1, point.Y)));
//			window.AddFilter(new TransformFilter(
//				"Поветнуть по ч.с",
//				size => new Size(size.Height, size.Width), 
//				(point, size) => new Point(point.Y, point.X)));

			window.AddFilter(new TransformFilter<RotationParameters>(
				"Свободное вращение",
				new RotateTransformer()));
			Application.Run (window);
		}
	}
}
