namespace Structures;

public class Plane
{
    public double Distance { get; set; } // to 0, 0, 0

    public Vector3 Normal { get; set; }
    
    private Vector3 Center { get; set; }

    public double Z { get; set; }

    public Plane(Vector3 normal, double distance)
    {
        Distance = distance;
        Normal = normal;
        Center = new Ray(Vector3.Zero(), Normal).PointAtDistanceFromOrigin(Distance);
    }

    public Plane(Vector3 normal, Vector3 point)
    {
        // jesli wektory sa prostopadle to 0, skierowane w przeciwnych kierunkach - ujemne, w ten samym - dodanie
        //  wiec chyba tak ma byc?
        // zastanawiam sie jak by dzialala prostopadlosc tylko
        if (normal.Dot(point) < 0)
            Distance = -point.Magnitude();
        else
            Distance = point.Magnitude();

        Normal = normal;
        Center = new Ray(Vector3.Zero(), Normal).PointAtDistanceFromOrigin(Distance);
    }

    public bool Intersects(Ray ray)
    {
        double dot = Normal.Dot(ray.Direction);
        if (Math.Abs(dot) > 0.0001f)
        {
            Vector3 p = new Ray(Vector3.Zero(), Normal).PointAtDistanceFromOrigin(Distance);
            double t = (p - ray.Origin).Dot(Normal) / dot;
            if (t >= 0) return true;
        }
        return false;
    }

    public Vector3 Intersection(Ray ray)
    {
        var d = Center.Dot(-Normal);
        var t = -(d + ray.Origin.Z * Normal.Z + ray.Origin.Y * Normal.Y + ray.Origin.X * Normal.X) 
                / (ray.Direction.Z * Normal.Z + ray.Direction.Y * Normal.Y + ray.Direction.X * Normal.X);
        return ray.Origin + t * ray.Direction;
    }
}