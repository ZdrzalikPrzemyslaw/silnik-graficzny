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
        Animation animation = new Animation(fps);
        
        // dodanie scen

        string path = "./Animation/";

        Animation.PictureList animated = animation.Animate();
        
        animated.Save();
        
        RunCmd.RunCommand("python"
            ,
            $"animate.py -d {path} -f {fps}"
            );

    }
}