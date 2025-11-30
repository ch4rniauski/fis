const int start = 2;
const int end = 100_000;

Console.WriteLine($"Числа Кармайкла в диапазоне от {start} до {end}:");

for (var i = start; i <= end; i++)
{
    if (KorseltCriterion(i))
    {
        Console.WriteLine(i);
    }
}

Console.WriteLine("\nФункция Кармайкла для 561: " + CarmichaelFunction(561));

List<int> PrimeFactors(int n)
{
    var factors = new List<int>();
    
    for (var i = 2; i <= n / i; i++)
    {
        if (n % i != 0)
        {
            continue;
        }
        
        factors.Add(i);
        
        while (n % i == 0)
        {
            n /= i;
        }
    }
    
    if (n > 1)
    {
        factors.Add(n);
    }
    
    return factors;
}

bool KorseltCriterion(int n)
{
    if (n < 2 || IsPrime(n))
    {
        return false;
    }
    
    if (!IsSquareFree(n))
    {
        return false;
    }

    var primes = PrimeFactors(n);
    
    return primes.Count >= 3
           && primes.All(p => (n - 1) % (p - 1) == 0); // Carmichael numbers have at least 3 prime factors
}

int CarmichaelFunction(int n)
{
    var primes = PrimeFactors(n);
    var lambdaValues = new List<int>();

    foreach (var p in primes)
    {
        var k = 0;
        var temp = n;
        
        while (temp % p == 0)
        {
            temp /= p;
            k++;
        }

        int lambdaP;
        
        if (p == 2 && k >= 3)
        {
            lambdaP = (int)Math.Pow(2, k - 2);
        }
        else
        {
            lambdaP = (int)Math.Pow(p, k - 1) * (p - 1);
        }

        lambdaValues.Add(lambdaP);
    }
    
    return Lcm(lambdaValues);
}

bool IsPrime(int n)
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

int Lcm(List<int> numbers)
{
    return numbers.Aggregate((a, b) => a / Gcd(a, b) * b);
}

int Gcd(int a, int b)
{
    while (b != 0)
    {
        var temp = b;
        b = a % b;
        a = temp;
    }
    
    return a;
}

bool IsSquareFree(int n)
{
    for (var i = 2; i * i <= n; i++)
    {
        if (n % (i * i) == 0)
        {
            return false;
        }
    }
    
    return true;
}
