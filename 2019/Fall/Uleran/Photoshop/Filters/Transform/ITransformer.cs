using System.Drawing;

namespace MyPhotoshop
{
    public interface ITransformer<TPamareters>
    where TPamareters : IParameters, new()
    {
        void Prepare(Size size, TPamareters parameters);
        Size ResultSize { get; }
        Point? MapPoint(Point newPoint);
    }
}