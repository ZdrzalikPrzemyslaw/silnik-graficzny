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
        // jesli wektory sa prostopadle to 0, skierowane w przeciwnych kierunkach - ujemne, w ten samym - dodanie
        //  wiec chyba tak ma byc?
        // zastanawiam sie jak by dzialala prostopadlosc tylko
        if (inNormal.Dot(point) < 0)
            distance = -point.Magnitude();
        else
            distance = point.Magnitude();

        normal = inNormal;
    }

    // TODO:
    public Vector3 Intersection(Ray ray)
    {
        return Vector3.Zero();
    }
}