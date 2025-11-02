using System.Numerics;

for (var p = 100; p <= 200; p++)
{
    if (IsPrime(p))
    {
        Console.WriteLine($"p={p}, Wilson: {CheckWilson(p)}");
    }
}

static bool CheckWilson(int p)
{
    BigInteger fact = 1;
    
    for (var i = 2; i < p; i++)
    {
        fact = (fact * i) % p;
    }
    
    return fact == p - 1;
}

static bool IsPrime(int n)
{
    if (n < 2)
    {
        return false;
    }
    
    if (n % 2 == 0)
    {
        return n == 2;
    }
    
    for (var i = 3; i * i <= n; i += 2)
    {
        if (n % i == 0)
        {
            return false;
        }
    }
    
    return true;
}
