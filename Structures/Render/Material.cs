namespace Structures.Render;

public class Material
{
    private readonly double[] KAmbient = new double[3];
    private readonly double[] KDiffuse = new double[3];
    private readonly double[] KSpecular = new double[3];
    private double Alpha;
    // private Texture TextureA;
    private bool HasTexture;

    public Material()
    {
        for (var i = 0; i < 3; i++)
        {
            KAmbient[i] = 0.3f;
            KDiffuse[i] = 0.5f;
            KSpecular[i] = 0.8f;
        }

        Alpha = 100;
        HasTexture = false;
        

    }

    public Material(double[] kAmbient, double[] kDiffuse, double[] kSpecular, double alpha, 
        // Texture texture,
        bool hasTexture)
    {
        KAmbient = kAmbient;
        KDiffuse = kDiffuse;
        KSpecular = kSpecular;
        Alpha = alpha;
        // TextureA = texture;
        HasTexture = hasTexture;
    }
}