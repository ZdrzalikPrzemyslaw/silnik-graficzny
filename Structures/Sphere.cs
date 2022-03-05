namespace Structures;

public class Sphere
{
    public Sphere()
    {
        Center = Vector3.Zero();
        Radius = 0;
    }

    public Sphere(Vector3 center, double radius)
    {
        Center = center;
        Radius = radius;
    }

    public double Distance(Ray ray)
    {
        return Vector3.Cross(ray.Direction, this.Center - ray.Origin).Magnitude();
    }
    
    // TODO: czy przeciecie to też stycznosc? jak tak to zle (<=)
    public bool Intersects(Ray ray)
    {
        return Distance(ray) < Radius;
    }

    // http://kylehalladay.com/blog/tutorial/math/2013/12/24/Ray-Sphere-Intersection.html
    public List<Vector3> Intersection(Ray ray)
    {
        var L = new Vector3(ray.Origin, Center);
        var tc = L.Dot(ray.Direction);

        var d = Math.Sqrt(L.MagnitudeSquared() - tc * tc);
        if (d > Radius)
        {
            return new List<Vector3>();
        }

        var t1c = Math.Sqrt(Radius * Radius - d * d);
        var t1 = tc - t1c;
        var t2 = tc + t1c;
        var retList = new List<Vector3>();
        if (t1c == 0)
        {
            retList.Add(ray.PointAtDistanceFromOrigin(t1));
            return retList;
        }

        if (t1 > 0)
        {
            retList.Add(ray.PointAtDistanceFromOrigin(t1));
        }

        if (t2 > 0)
        {
            retList.Add(ray.PointAtDistanceFromOrigin(t2));
        }

        return retList;
    }

    public Vector3 Center { get; set; }
    public double Radius { get; set; }
}