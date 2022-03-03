// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using Structures;

namespace Zad1;

class Zad1
{
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
        // Proszę sprawdzić, czy istnieje przecięcie sfery S z promieniami R1 oraz R2. 
        // Wynik w postaci współrzędnych punktu przecięcia należy wyświetlić
        // TODO: policzyc wspolrzedne punktu przeciecia
        Console.WriteLine(S.Intersects(R1));
        Console.WriteLine(S.Intersects(R2));
        
    }
}