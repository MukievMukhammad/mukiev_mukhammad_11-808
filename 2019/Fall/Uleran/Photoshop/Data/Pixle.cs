using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public struct Pixle
    {
        public Pixle(double r, double g, double b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        private double r;
        public double R
        {
            get { return r; }
            set
            {
                r = Check(value);
            }
        }
        private double g;
        public double G
        {
            get { return g; }
            set
            {
                g = Check(value);
            }
        }
        private double b;
        public double B
        {
            get { return b; }
            set
            {
                b = Check(value);
            }
        }

        public double Check(double value)
        {
            if (value < 0 || value > 255) throw new ArgumentException();
            return value;
        }

        public static Pixle operator *(Pixle p, double c)
        {
            return new Pixle(
                        p.R * c,
                        p.G * c,
                        p.B * c);
        }

        public static Pixle operator *(double c, Pixle p)
        {
            return p * c;
        }
    }
}
