int[] testValues = [2, 3, 5, 6, 7, 8, 19, 25, 100, 101, 194, 727, 971, 1987, 23687, 27143, 34919, 34921];

foreach (var m in testValues)
{
    Console.WriteLine($"Число {m} {(IsPrime(m)
        ? "простое"
        : "составное")}");
    
    var factors = PrimeFactors(m);
    
    Console.WriteLine($"Простые множители: {string.Join(", ", factors)}\n");
}

bool IsPrime(int m)
{
    switch (m)
    {
        case < 2:
            return false;
        case 2:
            return true;
    }

    if (m % 2 == 0)
    {
        return false;
    }
    
    for (var i = 3; i * i <= m; i += 2)
    {
        if (m % i == 0)
        {
            return false;
        }
    }
    
    return true;
}

List<int> PrimeFactors(int m)
{
    var factors = new List<int>();
    var n = m;
    
    for (var i = 2; i * i <= n; i++)
    {
        while (n % i == 0)
        {
            factors.Add(i);
            
            n /= i;
        }
    }
    
    if (n > 1)
    {
        factors.Add(n);
    }
    
    return factors;
}
