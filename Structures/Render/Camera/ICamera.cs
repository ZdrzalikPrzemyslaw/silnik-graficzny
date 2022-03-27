namespace Structures.Render.Camera;

public interface ICamera
{
    public Picture RenderScene(Scene scene, int sizeX = 500, int sizeY = 500);
}