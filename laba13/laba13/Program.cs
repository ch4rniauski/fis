using System.Numerics;

Console.Write("Введите число m: ");
var m = int.Parse(Console.ReadLine()!);

var phi = EulerPhi(m);
Console.WriteLine($"phi({m}) = {phi}");

for (var k = 1; k < m; k++)
{
    if (Gcd(k, m) == 1)
    {
        var modPow = BigInteger.ModPow(k, phi, m);
        
        Console.WriteLine($"{k}^{phi} = {modPow} (mod {m})");
    }
}

static int EulerPhi(int m)
{
    var result = m;
    var temp = m;

    for (var p = 2; p * p <= temp; p++)
    {
        if (temp % p == 0)
        {
            while (temp % p == 0)
            {
                temp /= p;
            }
            
            result -= result / p;
        }
    }

    if (temp > 1)
    {
        result -= result / temp;
    }

    return result;
}

static int Gcd(int a, int b)
{
    while (b != 0)
    {
        var t = b;
        b = a % b;
        a = t;
    }
    
    return a;
}
