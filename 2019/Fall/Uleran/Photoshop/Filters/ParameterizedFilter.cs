using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public abstract class ParameterizedFilter : IFilter
    {
        IParameters parameters;
        public ParameterizedFilter(IParameters parameters)
        {
            this.parameters = parameters;
        }

        public ParameterInfo[] GetParameters()
        {
            return parameters.GetDiscription();
        }

        public Photo Process(Photo original, double[] values)
        {
            parameters.SetValues(values);
            return Process(original, parameters);
        }

        public abstract Photo Process(Photo original, IParameters parameters);
    }
}
