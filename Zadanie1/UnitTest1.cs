using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Structures.Figures;
using Structures.MathObjects;

namespace Zadanie1;

[TestClass]
public class UnitTest1
{
    private static Ray R1;
    private static Ray R2;
    private static Ray R3;
    private static Sphere S;

    [ClassInitialize]
    public static void Init(TestContext testContext)
    {
        // 2. Zdefiniować sferę S o środku w punkcie (0,0,0) i promieniu 10.
        S = new Sphere(new Vector3(0, 0, 0), 10);

        // 3. Zdefiniować promień R1 o początku w punkcie (0,0,-20) i skierowany środek kuli.
        R1 = new Ray(new Vector3(0, 0, -20),
            new Vector3(new Vector3(0, 0, -20), new Vector3(0, 0, 0)).GetNormalized());

        // 4. Zdefiniować promień R2 o początku w tym samym punkcie, co R1, skierowany równolegle do osi Y.
        R2 = new Ray(new Vector3(0, 0, -20),
            new Vector3(new Vector3(0, 1, 0)));

        // 6. Proszę zdefiniować dowolny promień R3, tak aby przecinał on sferę S w dokładnie jednym punkcie.
        R3 = new Ray(new Vector3(10, 10, 0),
            new Vector3(new Vector3(10, 10, 0), new Vector3(10, 0, 0)).GetNormalized());
    }

    [TestMethod]
    public void TestSphereIntersection()
    {
        // 5. Proszę sprawdzić, czy istnieje przecięcie sfery S z promieniami R1 oraz R2. 
        //    Wynik w postaci współrzędnych punktu przecięcia należy wyświetlić.
        var x1 = S.Intersections(R1);
        Assert.AreEqual(2, x1.Count);
        Assert.AreEqual(new Vector3(0, 0, -10), x1[0]);
        Assert.AreEqual(new Vector3(0, 0, 10), x1[1]);

        Console.WriteLine($"Przeciecie Promienia R1 ze sferą S: p1: {x1[0]} p2:{x1[0]}");

        var x2 = S.Intersections(R2);
        Assert.AreEqual(0, x2.Count);

        Console.WriteLine("Przeciecie Promienia R2 ze sferą S: brak");

        // 6. Podać współrzędne punktu przecięcia
        var x3 = S.Intersections(R3);
        Assert.AreEqual(1, x3.Count);
        Assert.AreEqual(new Vector3(10, 0, 0), x3[0]);

        Console.WriteLine($"Przeciecie Promienia R2 ze sferą S: p: {x3[0]}");
    }

    public static string WriteToConsole<T>(List<T> list)
    {
        var toret = new StringBuilder("List: ");
        foreach (var x1 in list) toret.Append(x1).Append(" ");

        return toret.ToString();
    }

    [TestMethod]
    [ExpectedException(typeof(Plane.InfiniteIntersectionsException))]
    public void TestSphere()
    {
        var sphere = new Sphere(new Vector3(0, 0, 10), 1);
        var ray = new Ray(new Vector3(0, 0, 10), new Vector3(0, 0, 1));
        var x = sphere.Intersections(ray);
        Console.WriteLine($"{WriteToConsole(x)}");
        var plane = new Plane(new Vector3(0, 0, 1), 0);
        var ray2 = new Ray(new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        var invalidIntersection = plane.Intersection(ray2);
    }

    [TestMethod]
    public void TestPlaneIntersection()
    {
        // 7. Proszę zdefiniować płaszczyznę P przechodzącą przez punkt (0,0,0), której wektor normalny tworzy kąt 45 stopni z osiami Y i Z.
        var P = new Plane(new Vector3(0, 0.5, 0.5), new Vector3(0, 0, 0));

        // 8. Proszę znaleźć punkt przecięcia płaszczyzny P z promieniem R2.
        var intersection = P.Intersection(R2);
        Assert.AreEqual(new Vector3(0, 20, -20), intersection);
        Console.WriteLine($"Punkt przecięcia Płaszczyzny P z Promieniem R2: p: {intersection}");
    }

    [TestMethod]
    public void TestSphereIntersectionRayStartInsideSphere()
    {
        var R4 = new Ray(new Vector3(0, 0, 9),
            new Vector3(0, 0, 1));

        var intersection = S.Intersections(R4);

        Assert.AreEqual(1, intersection.Count);
        Assert.AreEqual(new Vector3(0, 0, 10), intersection[0]);
    }

    [TestMethod]
    public void TestPlaneIntersection2()
    {
        var plane = new Plane(new Vector3(6, 5, 1), new Vector3(0, 0, 30));
        var ray = new Ray(new Vector3(0, 0, 0),
            new Vector3(new Vector3(0, 0, 0), new Vector3(2, 4, 8)).GetNormalized());
        var intersection = plane.Intersection(ray);
        Assert.AreEqual(new Vector3(1.5, 3, 6), intersection);
    }
}