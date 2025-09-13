int[] testValues = [4, 6, 8, 10, 20, 50, 100];
            
foreach (var m in testValues)
{
    AnalyzeRing(m);
}

static void AnalyzeRing(int m)
{
    var zeroDivisors = FindZeroDivisorsAndAnnihilators(m);
    
    Console.WriteLine($"\n\nДелители нуля в Z{m}:");
    
    foreach (var (a, annihilators) in zeroDivisors.OrderBy(x => x.Key))
    {
        var theoreticalAnnihilatorsList = TheoreticalAnnihilators(a, m);
        
        var gcdAm = Gcd(a, m);
        var expectedCount = gcdAm - 1;
        
        Console.WriteLine($"\nЭлемент {a}:");
        Console.WriteLine($"\tНОД({a}, {m}) = {gcdAm}");
        Console.WriteLine($"\tОжидаемое количество аннуляторов (по Теореме 1): {expectedCount}");
        Console.WriteLine($"\tФактическое количество аннуляторов: {annihilators.Count}");
        Console.WriteLine($"\tАннуляторы (найденные перебором): [{string.Join(", ", annihilators)}]");
        Console.WriteLine($"\tАннуляторы (по Теореме 2): [{string.Join(", ", theoreticalAnnihilatorsList)}]");
        
        Console.WriteLine(annihilators.Count == expectedCount
            ? "\tТеорема 1 выполняется"
            : "\tТеорема 1 НЕ выполняется!");

        var sortedPractical = annihilators.OrderBy(x => x).ToList();
        var sortedTheoretical = theoreticalAnnihilatorsList.OrderBy(x => x).ToList();

        Console.Write(sortedPractical.SequenceEqual(sortedTheoretical)
            ? "\tТеорема 2 выполняется"
            : "\tТеорема 2 НЕ выполняется!");
    }
}

static Dictionary<int, List<int>> FindZeroDivisorsAndAnnihilators(int m)
{
    var zeroDivisorsAndAnnihilators = new Dictionary<int, List<int>>();
    
    for (var a = 0; a < m; a++)
    {
        if (a != 0 && Gcd(a, m) == 1)
        {
            continue;
        }
        
        var annihilators = new List<int>();
        
        for (var x = 1; x < m; x++)
        {
            if ((a * x) % m == 0)
            {
                annihilators.Add(x);
            }
        }
        
        if (annihilators.Count > 0)
        {
            zeroDivisorsAndAnnihilators[a] = annihilators;
        }
    }
            
    return zeroDivisorsAndAnnihilators;
}

static int Gcd(int a, int b)
{
    while (b != 0)
    {
        var temp = b;
        b = a % b;
        a = temp;
    }
    
    return a;
}

static List<int> TheoreticalAnnihilators(int a, int m)
{
    if (a == 0)
    {
        return Enumerable.Range(1, m - 1).ToList();
    }
    
    var gcd = Gcd(a, m);
    
    if (gcd == 1)
    {
        return [];
    }
    
    var annihilators = new List<int>();
    var quotient = m / gcd;
    
    for (var k = 1; k < gcd; k++)
    {
        var annihilator = (k * quotient) % m;
        
        annihilators.Add(annihilator);
    }
    
    annihilators.Sort();
    
    return annihilators;
}
