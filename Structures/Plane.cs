namespace Structures;

public class Plane
{
    private double distance; // to 0, 0, 0
    private Vector3 normal;

    public Plane(Vector3 inNormal, double inDistance)
    {
        distance = inDistance;
        normal = inNormal;
    }

    public Plane(Vector3 inNormal, Vector3 point)
    {
        distance = point.GetLength();
        normal = inNormal;
    }
}