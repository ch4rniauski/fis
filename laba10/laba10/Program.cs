using System.Numerics;

Console.Write("Введите число: ");
var m = int.Parse(Console.ReadLine()!);

Console.WriteLine($"phi({m}) = {EulerFuc(m)}");

int EulerFuc(int n)
{
    var count = 0;
    
    for (var k = 1; k <= n; k++)
    {
        if (BigInteger.GreatestCommonDivisor(n, k) == 1)
        {
            count++;
        }
    }
    
    return count;
}
