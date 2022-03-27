namespace Structures.Render.Light;

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

    public LightIntensity(LightIntensity lightIntensity)
    {
        R = lightIntensity.R;
        G = lightIntensity.G;
        B = lightIntensity.B;
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

    public static LightIntensity DefaultWhite()
    {
        return new LightIntensity(1, 1, 1);
    }

    public static LightIntensity DefaultBlack()
    {
        return new LightIntensity(0, 0, 0);
    }

    public static LightIntensity DefaultBackground()
    {
        return new LightIntensity(0.1, 0.1, 0.1);
    }

    public override string ToString()
    {
        return $"RGB({R}, {G}, {B})";
    }

    public LightIntensity GetSum(LightIntensity other)
    {
        return this + other;
    }

    public LightIntensity Add(double r = 0.0, double g = 0.0, double b = 0.0)
    {
        return this + new LightIntensity(r, g, b);
    }

    public LightIntensity Divide(double k)
    {
        return this / k;
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
        if (k == 0) return a * 0;
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

    public static LightIntensity operator *(LightIntensity lhs, LightIntensity rhs)
    {
        return new LightIntensity(lhs.R * rhs.R, lhs.G * rhs.G, lhs.B * rhs.B);
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

        public LightIntensityBuilder()
        {
        }

        public LightIntensityBuilder(double r, double g, double b)
        {
            SetR(r);
            SetG(g);
            SetB(b);
        }

        public LightIntensityBuilder(LightIntensity lightIntensity)
        {
            SetR(lightIntensity.R);
            SetG(lightIntensity.G);
            SetB(lightIntensity.B);
        }

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

        public static LightIntensityBuilder operator +(LightIntensityBuilder lightIntensityBuilder,
            LightIntensity lightIntensity)
        {
            return lightIntensityBuilder.AddLightIntensity(lightIntensity);
        }

        public static LightIntensityBuilder operator +(LightIntensityBuilder lhs,
            LightIntensityBuilder rhs)
        {
            lhs.R += rhs.R;
            lhs.G += rhs.G;
            lhs.B += rhs.B;
            return lhs;
        }

        public static LightIntensityBuilder operator *(LightIntensityBuilder lightIntensityBuilder,
            double k)
        {
            return new LightIntensityBuilder().SetB(lightIntensityBuilder.B * k).SetG(lightIntensityBuilder.G * k)
                .SetR(lightIntensityBuilder.R * k);
        }

        public static LightIntensityBuilder operator *(double k, LightIntensityBuilder lightIntensityBuilder)
        {
            return lightIntensityBuilder * k;
        }

        public static LightIntensityBuilder operator *(LightIntensityBuilder lightIntensityBuilder,
            LightIntensity lightIntensity)
        {
            return new LightIntensityBuilder().SetB(lightIntensityBuilder.B * lightIntensity.B)
                .SetG(lightIntensityBuilder.G * lightIntensity.G)
                .SetR(lightIntensityBuilder.R * lightIntensity.R);
        }

        public static LightIntensityBuilder operator *(LightIntensityBuilder lhs,
            LightIntensityBuilder rhs)
        {
            return new LightIntensityBuilder().SetB(lhs.B * rhs.B)
                .SetG(lhs.G * rhs.G)
                .SetR(lhs.R * rhs.R);
        }

        public LightIntensityBuilder AddLightIntensity(LightIntensity lightIntensity)
        {
            R += lightIntensity.R;
            G += lightIntensity.G;
            B += lightIntensity.B;
            return this;
        }
    }
}