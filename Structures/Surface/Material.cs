namespace Structures.Surface;

public class Material
{
    public double KAmbient { get; set; }
    public double KDiffuse { get; set; }
    public double KSpecular { get; set; }

    public double ShinessConstant { get; set; }

    private Texture? Texture { get; }
    private bool HasTexture { get; }

    public Material(Texture? texture = null)
    {
        KAmbient = 0.3f;
        KDiffuse = 0.5f;
        KSpecular = 0.8f;

        ShinessConstant = 100;
        Texture = texture;
        HasTexture = texture is not null;
    }

    public Material(double kAmbient, double kDiffuse, double kSpecular, double shinessConstant, Texture? texture = null)
    {
        KAmbient = kAmbient;
        KDiffuse = kDiffuse;
        KSpecular = kSpecular;
        ShinessConstant = shinessConstant;
        Texture = texture;
        HasTexture = texture is not null;
    }
}