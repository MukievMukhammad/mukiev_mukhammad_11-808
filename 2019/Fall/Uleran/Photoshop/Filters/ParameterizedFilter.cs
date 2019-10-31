using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public abstract class ParameterizedFilter<TParameters> : IFilter
    where TParameters : IParameters, new()
    {
        public ParameterInfo[] GetParameters()
        {
            return new TParameters().GetDiscription();
        }

        public Photo Process(Photo original, double[] values)
        {
            var parameters = new TParameters();
            parameters.SetValues(values);
            return Process(original, parameters);
        }

        public abstract Photo Process(Photo original, TParameters parameters);
    }
}
