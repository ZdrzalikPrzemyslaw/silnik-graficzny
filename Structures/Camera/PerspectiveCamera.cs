namespace Structures.Camera;

public class PerspectiveCamera : AbstractCamera
{
    public Plane NearPlane { get; set; }
    public Plane FarPlane { get; set; }
    public double Fov { get; set; }

    public override void RenderScene(Scene scene)
    {
        throw new NotImplementedException();
    }

    public Vector3[] FindNearPlaneCorners()
    {
        System.Numerics.Quaternion
    }

    public PerspectiveCamera() : this(Vector3.Zero(), new Vector3(0, 0, 1), new Vector3(0, 1, 0))
    {
    }

    public PerspectiveCamera(Vector3 position, Vector3 target, Vector3 up, Plane nearPlane, Plane farPlane, double fov)
        : base(position, target, up)
    {
        NearPlane = nearPlane;
        FarPlane = farPlane;
        Fov = fov;
    }

    public PerspectiveCamera(Vector3 position, Vector3 target, Vector3 up)
        : this(position, target, up,
            new Plane(target, new Ray(position, target).PointAtDistanceFromOrigin(1)),
            new Plane(target, new Ray(position, target).PointAtDistanceFromOrigin(1000)),
            90)
    {
    }
}