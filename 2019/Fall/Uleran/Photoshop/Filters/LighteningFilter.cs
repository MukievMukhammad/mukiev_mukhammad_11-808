using System;

namespace MyPhotoshop
{
	public class LighteningFilter : AbstractFilter
	{
        public LighteningFilter() : base(new LightenningParameters()) { }

		public override string ToString ()
		{
			return "Осветление/затемнение";
		}

        public override Pixle ProcessPixle(Pixle original, IParameters parameter)
        {
            return original * (parameter as LightenningParameters).Coefficient;
        }
	}
}

