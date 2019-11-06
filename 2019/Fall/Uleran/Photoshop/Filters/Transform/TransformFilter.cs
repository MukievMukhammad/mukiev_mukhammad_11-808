using System;
using System.Drawing;

namespace MyPhotoshop
{
    public class TransformFilter<TParameters> : ParameterizedFilter<TParameters> 
        where TParameters : IParameters, new()
    {
        private ITransformer<TParameters> transformer;
        private string name;

        public TransformFilter(
            string name,
            ITransformer<TParameters> transformer
        )
        {
            this.name = name;
            this.transformer = transformer;
        }
        
        public override string ToString()
        {
            return name;
        }

        public override Photo Process(Photo original, TParameters parameters)
        {
            var oldSize = new Size(original.width, original.height);
            transformer.Prepare(oldSize, parameters);
            var result = new Photo(transformer.ResultSize.Width, transformer.ResultSize.Height);

            for (var x = 0; x < result.width; x++)
            for (var y = 0; y < result.height; y++)
            {
                var point = new Point(x, y);
                var oldPoint = transformer.MapPoint(point);
                if(oldPoint.HasValue)
                    result[x, y] = original[oldPoint.Value.X, oldPoint.Value.Y];
            }

            return result;
        }
    }
}