Console.Write("Введите число a: ");
var a = int.Parse(Console.ReadLine()!);

Console.Write("Введите число b: ");
var b = int.Parse(Console.ReadLine()!);

var factorsA = PrimeFactorization(a);
var factorsB = PrimeFactorization(b);

Console.WriteLine($"\nРазложение {a}: {FormatFactorization(factorsA)}");
Console.WriteLine($"Разложение {b}: {FormatFactorization(factorsB)}");

var gcd = CalculateByPrimeFactorsWithFunc(
    factorsX: factorsA,
    factorsY: factorsB,
    func: Math.Min);

var lcm = CalculateByPrimeFactorsWithFunc(
    factorsX: factorsA,
    factorsY: factorsB,
    func: Math.Max);

Console.WriteLine($"\nНОД({a},{b}) = {gcd} ({FormatFactorization(PrimeFactorization(gcd))})");
Console.WriteLine($"НОК({a},{b}) = {lcm} ({FormatFactorization(PrimeFactorization(lcm))})");

Console.WriteLine($"\nПроверка тождества: НОД(a,b)*НОК(a,b) = a * b:" +
                  $"\n\t{gcd * lcm} == {a * b} - {(gcd * lcm == a * b
                      ? "Выполняется"
                      : "Не выполняется")}");

int CalculateByPrimeFactorsWithFunc(
    Dictionary<int,int> factorsX,
    Dictionary<int,int> factorsY,
    Func<int, int, int> func)
{
    var localNum = 1;
    
    foreach (var p in factorsX
                 .Keys
                 .Concat(factorsY.Keys)
                 .Distinct())
    {
        factorsX.TryGetValue(p, out var expX);
        factorsY.TryGetValue(p, out var expY);
        
        var requiredExp = func(expX, expY);
        
        localNum *= (int)Math.Pow(p, requiredExp);
    }
    
    return localNum;
}

Dictionary<int, int> PrimeFactorization(int n)
{
    var factors = new Dictionary<int, int>();
    var num = n;
    
    for (var i = 2; i * i <= num; i++)
    {
        while (num % i == 0)
        {
            factors.TryAdd(i, 0);
            
            factors[i]++;
            
            num /= i;
        }
    }
    
    if (num > 1)
    {
        factors.TryAdd(num, 0);
        
        factors[num]++;
    }
    
    return factors;
}

string FormatFactorization(Dictionary<int,int> factors)
{
    return string.Join(" * ", factors.Select(f => $"{f.Key}^{f.Value}"));
}
