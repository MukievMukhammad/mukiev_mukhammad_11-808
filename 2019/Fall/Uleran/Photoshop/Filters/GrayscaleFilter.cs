using System;

namespace MyPhotoshop
{
    public class GrayScaleFilter : AbstractFilter
    {
        public GrayScaleFilter(): base(new EmptyParameters()) { }

        public override string ToString()
        {
            return "Оттенки серого";
        }

        public override Pixle ProcessPixle(Pixle original, IParameters parameter)
        {
            var lightness = (original.R + original.G + original.B) / 3;
            return new Pixle(lightness, lightness, lightness);
        }
    }
}

