namespace Structures;

public class LightIntensity : IEquatable<LightIntensity>
{
    public double R { get; set; }
    public double G { get; set; }
    public double B { get; set; }

    public LightIntensity(double r, double g, double b)
    {
        R = R > 1 ? 1 : R;
        R = R < 0 ? 0 : R;
        R = r;

        G = G > 1 ? 1 : G;
        G = G < 0 ? 0 : G;
        G = g;

        B = B > 1 ? 1 : B;
        B = B < 0 ? 0 : B;
        B = b;
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

    public bool Equals(LightIntensity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B);
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
        private double R;
        private double G;
        private double B;

        public LightIntensity Build()
        {
            return new LightIntensity(R, G, B);
        }

        public LightIntensityBuilder setR(double R)
        {
            this.R = R;
            return this;
        }

        public LightIntensityBuilder setG(double G)
        {
            this.G = G;
            return this;
        }

        public LightIntensityBuilder setB(double B)
        {
            this.B = B;
            return this;
        }
    }
}