namespace Structures.Render.Camera;

public interface ICamera
{
    public Picture RenderScene(Scene scene, int sizeX = 32, int sizeY = 32);
}