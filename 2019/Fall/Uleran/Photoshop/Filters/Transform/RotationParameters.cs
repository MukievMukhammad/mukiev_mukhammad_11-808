using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public class RotationParameters : IParameters
    {
        public double Angle { get; set; }
        public ParameterInfo[] GetDiscription()
        {
            return new[]
            {
                new ParameterInfo { Name="Угол", MaxValue=360, MinValue=0, Increment=1, DefaultValue=1 }

            };
        }

        public void SetValues(double[] values)
        {
            Angle = values[0];
        }
    }
}
