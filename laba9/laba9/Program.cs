using System.Numerics;

Console.Write("Введите число p: ");
var m = int.Parse(Console.ReadLine()!);

CheckFermat(m);

void CheckFermat(int p)
{
    Console.WriteLine($"\nПроверка малой теоремы Ферма для p = {p}:");
    
    for (var a = 1; a < p; a++)
    {
        var result = BigInteger.ModPow(a, p - 1, p);
        Console.WriteLine($"\ta = {a}: a^{p - 1} = {result} (mod {p})");

        if (result != 1)
        {
            Console.WriteLine("Ошибка! Малая теорема Ферма не выполнена\n");
            
            return;
        }
    }
    Console.WriteLine("Все числа удовлетворяют малой теореме Ферма (значения равны 1)\n");
}
