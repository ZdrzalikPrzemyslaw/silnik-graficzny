﻿using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public class AmbientLightSource : LightSource
{
    public override LightIntensity GetIntensity(Vector3 position)
    {
        return this.Colour;
    }

    public AmbientLightSource(LightIntensity lightIntensity) : base(lightIntensity)
    {
    }

    public override bool IsInShadow(PointOfIntersection pointOfIntersection, Scene scene)
    {
        return false;
    }
}