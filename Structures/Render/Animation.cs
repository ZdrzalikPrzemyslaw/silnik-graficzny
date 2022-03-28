using Structures.Render.Camera;

namespace Structures.Render;

public class Animation
{
    private List<SceneAndCamera> _scenesAndCameras;
    public int FPS { get; set; } = 30;

    public Animation(int fps, List<SceneAndCamera>? scenesAndCameras = null)
    {
        this._scenesAndCameras = scenesAndCameras?? new List<SceneAndCamera>();
        this.FPS = fps;
    }
    
    public void AddSceneAndCamera(SceneAndCamera sceneAndCamera)
    {
        _scenesAndCameras.Add(sceneAndCamera);
    }

    public void RemoveSceneAndCamera(SceneAndCamera sceneAndCamera)
    {
        _scenesAndCameras.Remove(sceneAndCamera);
    }

    public PictureList Animate()
    {
        List<Picture> pictures = new List<Picture>();
        foreach (var (scene, camera) in _scenesAndCameras)
        {
            pictures.Add(camera.RenderScene(scene));
        }
        return new PictureList(pictures);
    }


    public record PictureList(List<Picture> Pictures)
    {
        public void Save(string path = "./Animation/", string pictureName = "Picture")
        {
            try
            {
                Directory.Delete(path, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                // ignored
            }
            
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                // ignored
            }
            

            for (var i = 0; i < Pictures.Count; i++)
            {
                Pictures[i].PrintToPath(path, pictureName + (i + 1).ToString("D8") + ".png");
            }
        }
    }
}