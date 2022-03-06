// See https://aka.ms/new-console-template for more information


using System.Text;
using Structures;

namespace Zad1;

internal class Zad1
{
    public static string WriteToConsole<T>(List<T> list)
    {
        var toret = new StringBuilder("List: ");
        foreach (var x1 in list) toret.Append(x1).Append(" ");

        return toret.ToString();
    }

    public static void Main(string[] args)
    {
        // Zdefiniować sferę S o środku w punkcie (0,0,0) i promieniu 10
        var S = new Sphere(new Vector3(0, 0, 0), 10);
        // Zdefiniować promień R1 o początku w punkcie (0,0,-20) i skierowany w srodek kuli (SPRAWDZIC CZY TO DOBRZE)
        var R1 = new Ray(new Vector3(0, 0, -20),
            new Vector3(new Vector3(0, 0, -20), new Vector3(0, 0, 0)).GetNormalized());
        // Zdefiniować promień R2 o początku w tym samym punkcie, co R1, skierowany równolegle do osi Y (SPRAWDZIC CZY TO DOBRZE)
        var R2 = new Ray(new Vector3(0, 0, -20),
            new Vector3(new Vector3(0, 1, 0)));
        // Proszę zdefiniować dowolny promień R3, tak aby przecinał on sferę S w 
        // dokładnie jednym punkcie. Podać współrzędne punktu przecięcia
        var R3 = new Ray(new Vector3(10, 10, 0),
            new Vector3(new Vector3(10, 10, 0), new Vector3(10, 0, 0)).GetNormalized());

        // Przecina
        var x1 = S.Intersection(R1);
        // Nie przecina
        var x2 = S.Intersection(R2);
        // Styczny
        var x3 = S.Intersection(R3);

        Console.WriteLine($"{WriteToConsole(x1)}\n{WriteToConsole(x2)}\n{WriteToConsole(x3)}");


        // Proszę zdefiniować płaszczyznę P przechodzącą przez punkt (0,0,0), której 
        // wektor normalny tworzy kąt 45 stopni z osiami Y i Z
        // TODO: czy to na pewno tworzy kat 45??
        var P = new Plane(new Vector3(0, 0.5, 0.5), new Vector3(0,0,0));
        
        // Proszę znaleźć punkt przecięcia płaszczyzny P z promieniem R2
        
        // TODO:
        var x4 = P.Intersection(R2);
    }
}