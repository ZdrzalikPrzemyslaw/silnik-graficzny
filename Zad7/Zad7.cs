using System.Diagnostics;
using Structures;
using Structures.Figures;
using Structures.MathObjects;
using Structures.Render;
using Structures.Render.Camera;
using Structures.Render.Light;
using Structures.Surface;
using Structures.Surface.Reflection;

namespace Zad7;

internal class Zad7
{
    public static void Main(string[] args)
    {

        int fps = 24;
        ScenesForAnimation scenesForAnimation = new ScenesForAnimation();
        List<SceneAndCamera> sceneAndCameras = scenesForAnimation.CreateScenesForAnimations(fps);
        
        
        Animation animation = new Animation(fps, sceneAndCameras);
        
        // dodanie scen

        string path = "./Animation/";

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Animation.PictureList animated = animation.Animate();
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
        animated.Save();
        
        RunCmd.RunCommand("python"
            ,
            $"animate.py -d {path} -f {fps}"
            );
    }
}