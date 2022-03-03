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
        // TODO: w unity ten dystans jest dodatni lub ujemny w zaleznosci od tego jak skierowany jest wektor normalny,
        //      u nas jest narazie zawsze dodatni idk czy tak to ma zostac?
        distance = point.Magnitude();
        normal = inNormal;
    }
}