namespace Structures;

public abstract class AbstractCamera : ICamera
{
    public abstract void RenderScene();

    public Vector3 Position { get; set; }
    public Vector3 Target { get; set; }

    public AbstractCamera()
    {
        Position = new Vector3(0, 0, 0);
        Target = new Vector3(0, 0, 1);
    }

    public AbstractCamera(Vector3 position, Vector3 target)
    {
        Position = position;
        Target = target;
    }
}