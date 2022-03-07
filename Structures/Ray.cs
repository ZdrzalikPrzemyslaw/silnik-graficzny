namespace Structures;

public class Ray : IEquatable<Ray>
{
    public Ray(Vector3 origin, Vector3 direction)
    {
        Origin = origin;
        Direction = direction.GetNormalized();
    }

    public Ray Inverse()
    {
        return new Ray(Origin, Direction.Inverse());
    }

    public Ray() : this(Vector3.Zero(), Vector3.Zero())
    {
    }

    public Vector3 Origin { get; }
    public Vector3 Direction { get; }

    public bool Equals(Ray? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Origin.Equals(other.Origin) && Direction.Equals(other.Direction);
    }

    public Vector3 PointAtDistanceFromOrigin(double distance)
    {
        return Origin + Direction * distance;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Ray);
    }
}