using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Structures;

namespace Zadanie1;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestSphereIntersection()
    {
        Sphere S = new Sphere(new Vector3(0, 0, 0), 10);
        Ray R1 = new Ray(new Vector3(0, 0, -20),
            new Vector3(new Vector3(0, 0, -20), new Vector3(0, 0, 0)).GetNormalized());
        Ray R2 = new Ray(new Vector3(0, 0, -20),
            new Vector3(new Vector3(0, 1, 0)));
        Ray R3 = new Ray(new Vector3(10, 10, 0),
            new Vector3(new Vector3(10, 10, 0), new Vector3(10, 0, 0)).GetNormalized());
        
        List<Vector3> x1 = S.Intersection(R1);
        Assert.AreEqual(2, x1.Count);
        Assert.AreEqual(new Vector3(0, 0, -10), x1[0]);
        Assert.AreEqual(new Vector3(0, 0, 10), x1[1]);
        
        List<Vector3> x2 = S.Intersection(R2);
        Assert.AreEqual(0, x2.Count);
        
        List<Vector3> x3 = S.Intersection(R3);
        Assert.AreEqual(1, x3.Count);
        Assert.AreEqual(new Vector3(10, 0, 0), x3[0]);
    }

    [TestMethod]
    public void TestPlaneIntersection()
    {
        Plane P = new Plane(new Vector3(0, 0.5, 0.5), new Vector3(0, 0, 0));
        Ray R2 = new Ray(new Vector3(0, 0, -20),
            new Vector3(new Vector3(0, 1, 0)));
        new Ray(Vector3.Zero(), P.Normal).PointAtDistanceFromOrigin(P.Distance);
        
        Assert.AreEqual(new Vector3(0, 20, -20), P.Intersection(R2));
    }
}