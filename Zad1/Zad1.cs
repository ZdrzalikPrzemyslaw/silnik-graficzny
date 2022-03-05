// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using System.Text;
using Structures;

namespace Zad1;

class Zad1
{

    static public String WriteToConsole<T>(List<T> list)
    {
        var toret = new StringBuilder("List: ");
        foreach (var x1 in list)
        {
            toret.Append(x1).Append(" ");
        }

        return toret.ToString();
    }
    static public void Main(String[] args)
    {
        // Zdefiniować sferę S o środku w punkcie (0,0,0) i promieniu 10
        Sphere S = new Sphere(new Vector3(0, 0, 0), 10);
        // Zdefiniować promień R1 o początku w punkcie (0,0,-20) i skierowany w srodek kuli (SPRAWDZIC CZY TO DOBRZE)
        Ray R1 = new Ray(new Vector3(0, 0, -20),
            new Vector3(new Vector3(0, 0, -20), new Vector3(0, 0, 0)).GetNormalized());
        // Zdefiniować promień R2 o początku w tym samym punkcie, co R1, skierowany równolegle do osi Y (SPRAWDZIC CZY TO DOBRZE)
        Ray R2 = new Ray(new Vector3(0, 0, -20),
            new Vector3(new Vector3(0, 1, 0)));
        Ray R3 = new Ray(new Vector3(0, 0, -9),
            new Vector3(new Vector3(0, 0, -9), new Vector3(0, 0, 11)).GetNormalized());
        Ray R4 = new Ray(new Vector3(0, 0, 9),
            new Vector3(new Vector3(0, 0, 9), new Vector3(0, 0, 11)).GetNormalized());
        Ray R5 = new Ray(new Vector3(10, 10,0),
            new Vector3(new Vector3(10, 10, 0), new Vector3(10, 0, 0)).GetNormalized());
        // Proszę sprawdzić, czy istnieje przecięcie sfery S z promieniami R1 oraz R2. 
        // Wynik w postaci współrzędnych punktu przecięcia należy wyświetlić
        // TODO: policzyc wspolrzedne punktu przeciecia
        Console.WriteLine(S.Intersects(R1));
        Console.WriteLine(S.Intersects(R2));
        // Przecina
        var x1 = S.Intersection(R1);
        // Nie przecina
        var x2 = S.Intersection(R2);
        // Zaczyna sie w srodku, blizej poczatku 
        var x3 = S.Intersection(R3);
        // Zaczyna sie w srodku, dalej poczatku 
        var x4 = S.Intersection(R4);
        // Styczny
        var x5 = S.Intersection(R5);
        Console.WriteLine($"{WriteToConsole(x1)}\n{WriteToConsole(x2)}\n{WriteToConsole(x3)}\n{WriteToConsole(x4)}\n{WriteToConsole(x5)}");
    }
}