namespace Structures.Render.Camera;

public interface ICamera
{
    public Picture RenderScene(Scene scene, int sizeX = 256, int sizeY = 256);
}