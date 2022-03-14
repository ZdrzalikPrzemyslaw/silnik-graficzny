namespace Structures.Render;

public class LightIntensity : IEquatable<LightIntensity>
{
    public LightIntensity(double r, double g, double b)
    {
        r = r > 1 ? 1 : r;
        r = r < 0 ? 0 : r;
        R = r;

        g = g > 1 ? 1 : g;
        g = g < 0 ? 0 : g;
        G = g;

        b = b > 1 ? 1 : b;
        b = b < 0 ? 0 : b;
        B = b;
    }

    public LightIntensity()
    {
        R = B = G = 0.0;
    }

    public double R { get; set; }
    public double G { get; set; }
    public double B { get; set; }

    public bool Equals(LightIntensity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B);
    }

    public static LightIntensity DefaultObject()
    {
        return new LightIntensity(1, 0.78, 0.64);
    }

    public static LightIntensity DefaultBackground()
    {
        return new LightIntensity(0, 0, 0);
    }

    public override string ToString()
    {
        return $"RGB({R}, {G}, {B})";
    }

    public LightIntensity GetSum(LightIntensity other)
    {
        return this + other;
    }

    public static LightIntensity operator +(LightIntensity a, LightIntensity b)
    {
        return new LightIntensity(a.R + b.R, a.G + b.G, a.B + b.B);
    }

    public static LightIntensity operator -(LightIntensity a, LightIntensity b)
    {
        return new LightIntensity(a.R - b.R, a.G - b.G, a.B - b.B);
    }

    public static LightIntensity operator /(LightIntensity a, double k)
    {
        if (k == 0) throw new DivideByZeroException();
        return a * (1 / k);
    }

    public static LightIntensity operator *(LightIntensity a, double k)
    {
        return new LightIntensity(a.R * k, a.G * k, a.B * k);
    }

    public static LightIntensity operator *(double k, LightIntensity a)
    {
        return new LightIntensity(a.R * k, a.G * k, a.B * k);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as LightIntensity);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(R, G, B);
    }


    public class LightIntensityBuilder
    {
        private double B;
        private double G;
        private double R;

        public LightIntensity Build()
        {
            return new LightIntensity(R, G, B);
        }

        public LightIntensityBuilder SetR(double R)
        {
            this.R = R;
            return this;
        }

        public LightIntensityBuilder SetG(double G)
        {
            this.G = G;
            return this;
        }

        public LightIntensityBuilder SetB(double B)
        {
            this.B = B;
            return this;
        }
    }
}