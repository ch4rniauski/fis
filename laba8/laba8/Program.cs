using System.Numerics;

Console.Write("Введите число p: ");
var m = int.Parse(Console.ReadLine()!);

CheckFermat(m);

Console.WriteLine("Проверка обратного утверждения малой теоремы Ферма для чисел до 5000:");
CheckConverseFermat(5000);

void CheckFermat(int p)
{
    Console.WriteLine($"\nПроверка малой теоремы Ферма для p = {p}:");
    
    for (var a = 1; a < p; a++)
    {
        var result = BigInteger.ModPow(a, p - 1, p);
        Console.WriteLine($"\ta = {a}, a^{p - 1} = {result} (mod {p})");

        if (result != 1)
        {
            Console.WriteLine("Ошибка! Малая теорема Ферма не выполнена\n");
            
            return;
        }
    }
    Console.WriteLine("Все числа удовлетворяют малой теореме Ферма (значения равны 1)\n");
}

void CheckConverseFermat(int maxM)
{
    for (var m = 2; m <= maxM; m++)
    {
        var holdsForAllCoprimeA = true;

        for (var a = 1; a < m; a++)
        {
            if (BigInteger.GreatestCommonDivisor(a, m) != 1)
            {
                continue;
            }

            var res = BigInteger.ModPow(a, m - 1, m);
            
            if (res != 1)
            {
                holdsForAllCoprimeA = false;
                
                break;
            }
        }

        if (holdsForAllCoprimeA && !IsPrime(m))
        {
            var factorization = PrimeFactorization(m);
            
            Console.WriteLine($"\tЧисло {m} составное, но для всех взаимно простых a выполняется a^(m-1) = 1 (mod {m})");
            Console.WriteLine($"\tРазложение: {string.Join(" * ", factorization)}\n");
        }
    }
}

bool IsPrime(int x)
{
    switch (x)
    {
        case < 2:
            return false;
        case 2:
            return true;
    }
    if (x % 2 == 0)
    {
        return false;
    }

    var limit = (int)Math.Sqrt(x);
    
    for (var i = 3; i <= limit; i += 2)
    {
        if (x % i == 0)
        {
            return false;
        }
    }
    
    return true;
}

IEnumerable<string> PrimeFactorization(int m)
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

    return factors
        .GroupBy(x => x)
        .Select(g => $"{g.Key}^{g.Count()}");
}
