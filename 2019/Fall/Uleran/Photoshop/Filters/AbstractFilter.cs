using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public abstract class AbstractFilter<TParameters> : ParameterizedFilter<TParameters>
    where TParameters : IParameters, new()
    {
        public override Photo Process(Photo original, TParameters parameters)
        {
            var result = new Photo(original.width, original.height);

            for (int x = 0; x < result.width; x++)
                for (int y = 0; y < result.height; y++)
                {
                    result[x, y] = ProcessPixle(original[x, y], parameters);
                }
            return result;
        }

        public abstract Pixle ProcessPixle(Pixle original, TParameters parameter);
    }
}
