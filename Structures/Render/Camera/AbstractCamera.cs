using Structures.MathObjects;

namespace Structures.Render.Camera;

public abstract class AbstractCamera : ICamera
{
    public AbstractCamera() : this(Vector3.Zero(), new Vector3(0, 0, 1), new Vector3(0, 1, 0))
    {
    }

    public AbstractCamera(Vector3 position, Vector3 target, Vector3 up)
    {
        Position = position;
        Target = target;
        Up = up;
    }

    public Vector3 Position { get; set; }
    public Vector3 Target { get; set; }
    public Vector3 Up { get; set; }
    public abstract Picture RenderScene(Scene scene);
}