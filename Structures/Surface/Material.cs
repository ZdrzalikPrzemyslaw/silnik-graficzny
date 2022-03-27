﻿using Structures.Surface.Reflection;

namespace Structures.Surface;

public class Material
{
    public double KAmbient { get; set; }
    public double KDiffuse { get; set; }
    public double KSpecular { get; set; }

    public IReflection? Reflection { get; set; }
    public double ShinessConstant { get; set; }

    public Texture? Texture { get; set; }
    public bool HasTexture { get; set;  }

    public Material(Texture? texture = null, IReflection? reflection = null) : this(0.3f, 0.5f, 0.8f, 100, texture,
        reflection)
    {
    }

    public Material(double kAmbient, double kDiffuse, double kSpecular, double shinessConstant, Texture? texture = null,
        IReflection? reflection = null)
    {
        KAmbient = kAmbient;
        KDiffuse = kDiffuse;
        KSpecular = kSpecular;
        ShinessConstant = shinessConstant;
        Texture = texture;
        HasTexture = texture is not null;
        Reflection = reflection;
    }
}