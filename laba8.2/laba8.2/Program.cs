Console.Write("Введите числа через пробел: ");

var numbers = Console.ReadLine()!
    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

foreach (var t in numbers)
{
    var factors = PrimeFactorization(t);
    
    Console.WriteLine($"Разложение {t}: {FormatFactorization(factors)}");
}

var gcdByFactors = CalculateByPrimeFactors(numbers, Math.Min);
var lcmByFactors = CalculateByPrimeFactors(numbers, Math.Max);

Console.WriteLine("\nРазложение на простые множители:");
Console.WriteLine($"НОД = {gcdByFactors} ({FormatFactorization(PrimeFactorization(gcdByFactors))})");
Console.WriteLine($"НОК = {lcmByFactors} ({FormatFactorization(PrimeFactorization(lcmByFactors))})");

var gcdRecursive = numbers.Aggregate(Gcd);
var lcmRecursive = numbers.Aggregate(Lcm);

Console.WriteLine("\nРекуррентно:");
Console.WriteLine($"НОД = {gcdRecursive}");
Console.WriteLine($"НОК = {lcmRecursive}");

Console.WriteLine("\nПроверка тождества: НОД * НОК = произведение всех чисел");
Console.WriteLine($"\t{gcdRecursive * lcmRecursive} == {numbers.Aggregate(1, (acc, x) => acc * x)}");

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

int CalculateByPrimeFactors(int[] nums, Func<int, int, int> func)
{
    var allFactors = nums
        .Select(PrimeFactorization)
        .ToArray();
    
    var primes = allFactors
        .SelectMany(f => f.Keys)
        .Distinct();

    var result = 1;
    
    foreach (var p in primes)
    {
        var exps = allFactors.Select(f => f.GetValueOrDefault(p, 0));
        var requiredExp = exps.Aggregate(func);
        
        result *= (int)Math.Pow(p, requiredExp);
    }
    
    return result;
}

int Gcd(int x, int y)
{
    while (y != 0)
    {
        var temp = y;
        y = x % y;
        x = temp;
    }
    
    return x;
}

int Lcm(int x, int y)
    => x / Gcd(x, y) * y;

string FormatFactorization(Dictionary<int, int> factors)
    => string.Join(" * ", factors.Select(f => $"{f.Key}^{f.Value}"));
