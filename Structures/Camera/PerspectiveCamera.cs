namespace Structures.Camera;

public class PerspectiveCamera : AbstractCamera
{
    public Vector3 NearPlane { get; set; }
    public Vector3 FarPlane { get; set; }
    public double Fov { get; set; }
    public override void RenderScene(Scene scene)
    {
        throw new NotImplementedException();
    }
}