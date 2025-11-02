using System.Numerics;

Console.Write("Введите верхнюю границу поиска чисел Кармайкла: ");
var limit = int.Parse(Console.ReadLine()!);

for (var n = 2; n <= limit; n++)
{
    if (IsCarmichael(n))
    {
        Console.WriteLine($"Число Кармайкла найдено: {n}");
    }
}

static bool IsCarmichael(int n)
{
    if (IsPrime(n)) // число Кармайкла должно быть составным
    {
        return false;
    }

    for (var a = 2; a < n; a++)
    {
        if (Gcd(a, n) == 1)
        {
            var modPow = BigInteger.ModPow(a, n - 1, n);
            
            if (modPow != 1)
            {
                return false;
            }
        }
    }
    
    return true;
}

static bool IsPrime(int n)
{
    if (n < 2)
    {
        return false;
    }
    
    for (var i = 2; i * i <= n; i++)
    {
        if (n % i == 0)
        {
            return false;
        }
    }
    
    return true;
}

static int Gcd(int a, int b)
{
    while (b != 0)
    {
        var temp = a % b;
        a = b;
        b = temp;
    }
    
    return a;
}