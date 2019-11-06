using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public class AbstractFilter<TParameters> : ParameterizedFilter<TParameters>
    where TParameters : IParameters, new()
    {
        private string name;
        private Func<Pixle, TParameters, Pixle> processor;

        public AbstractFilter(string name, Func<Pixle, TParameters, Pixle> processor)
        {
            this.name = name;
            this.processor = processor;
        }
        
        public override Photo Process(Photo original, TParameters parameters)
        {
            var result = new Photo(original.width, original.height);

            for (int x = 0; x < result.width; x++)
                for (int y = 0; y < result.height; y++)
                {
                    result[x, y] = processor(original[x, y], parameters);
                }
            return result;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
