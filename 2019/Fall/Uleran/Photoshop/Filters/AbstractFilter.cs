using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public abstract class AbstractFilter : ParameterizedFilter
    {
        public AbstractFilter(IParameters parameters) : base(parameters) { }

        public override Photo Process(Photo original, IParameters parameters)
        {
            var result = new Photo(original.width, original.height);

            for (int x = 0; x < result.width; x++)
                for (int y = 0; y < result.height; y++)
                {
                    result[x, y] = ProcessPixle(original[x, y], parameters);
                }
            return result;
        }

        public abstract Pixle ProcessPixle(Pixle original, IParameters parameter);
    }
}
