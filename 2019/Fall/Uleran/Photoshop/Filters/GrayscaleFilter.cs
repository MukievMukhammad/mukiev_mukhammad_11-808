using System;

namespace MyPhotoshop
{
    public class GrayScaleFilter : AbstractFilter<EmptyParameters>
    {
        public override string ToString()
        {
            return "Оттенки серого";
        }

        public override Pixle ProcessPixle(Pixle original, EmptyParameters parameter)
        {
            var lightness = (original.R + original.G + original.B) / 3;
            return new Pixle(lightness, lightness, lightness);
        }
    }
}

