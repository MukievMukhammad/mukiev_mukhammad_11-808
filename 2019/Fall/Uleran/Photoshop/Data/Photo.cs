using System;

namespace MyPhotoshop
{
    public class Photo
    {
        public readonly int width;
        public readonly int height;
        readonly Pixle[,] data;

        public Photo(int width, int height)
        {
            this.width = width;
            this.height = height;
            data = new Pixle[width, height];
        }

        public Pixle this[int x, int y]
        {
            get { return data[x, y]; }
            set { data[x, y] = value; }
        }
	}
}

