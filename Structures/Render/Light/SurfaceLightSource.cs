using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public class SurfaceLightSource : AbstractLightSourceArray
{
    private readonly PointLightSource[,] _pointLightSources;

    public SurfaceLightSource(PointLightSource pointLightSource, Plane plane, Vector3 up, double width = 4,
        double height = 4, int rows = 3, int columns = 3)
    {
        // TODO: poprawic exepct
        if (rows <= 0) throw new ArgumentException();
        if (columns <= 0) throw new ArgumentException();
        if (width <= 0) throw new ArgumentException();
        if (height <= 0) throw new ArgumentException();

        _pointLightSources = new PointLightSource[rows, columns];
        Plane = plane;
        Up = up;
        Width = width;
        Height = height;
        var xOffset = width / (columns <= 1 ? 1 : columns - 1);
        var yOffset = height / (rows <= 1 ? 1 : rows - 1);
        var rayX = new Ray(pointLightSource.Location, up.Cross(plane.Normal));
        for (var i = 0; i < rows; i++)
        for (var j = 0; j < columns; j++)
        {
            var rayY = new Ray(rayX.PointAtDistanceFromOrigin(xOffset * i), -up);
            var point = rayY.PointAtDistanceFromOrigin(yOffset * j);
            _pointLightSources[i, j] = new PointLightSource(pointLightSource, point);
        }
    }

    public double Width { get; }
    public double Height { get; }

    public Plane Plane { get; }

    public Vector3 Up { get; }

    public override AbstractLightSource[] GetLightSources()
    {
        return _pointLightSources.Cast<AbstractLightSource>().ToArray();
    }
}