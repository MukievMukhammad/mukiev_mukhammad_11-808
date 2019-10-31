using System;

namespace MyPhotoshop
{
	public class LighteningFilter : AbstractFilter<LightenningParameters>
	{
		public override string ToString ()
		{
			return "Осветление/затемнение";
		}

        public override Pixle ProcessPixle(Pixle original, LightenningParameters parameter)
        {
            return original * parameter.Coefficient;
        }
	}
}

