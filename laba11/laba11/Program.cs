const int k = 100;

var testCases = new (long a, long b, long c)[]
{
    (2, 3, 5),
    (3, -6, 23),
    (2, 4, 8),
    (17, 35, 95),
    (15, 95, -205),
    (-279, 2547, 15219),
};

foreach (var (a, b, c) in testCases)
{
    SolveAndPrint(a, b, c, k);
}

static void SolveAndPrint(long a, long b, long c, int k)
{
    Console.WriteLine($"\nУравнение: {a}x + {b}y = {c}");

    var gcd = Gcd(Math.Abs(a), Math.Abs(b));
    Console.WriteLine($"gcd(|a|, |b|) = {gcd}");

    if (c % gcd != 0)
    {
        Console.WriteLine("Решений нет, так как gcd(a, b) не делит c.");
        
        return;
    }

    var (x, y, _) = ExtendedGcd(a, b);

    var factorC = c / gcd;
    var x0 = x * factorC;
    var y0 = y * factorC;

    var stepX = b / gcd;
    var stepY = -a / gcd;

    Console.WriteLine("Частное решение:");
    Console.WriteLine($"\tx0 = {x0}");
    Console.WriteLine($"\ty0 = {y0}");
    
    Console.WriteLine("Общее решение:");
    Console.WriteLine($"\tx = x0 + {stepX} * t, t ∈ Z");
    Console.WriteLine($"\ty = y0 + {stepY} * t, t ∈ Z");

    Console.WriteLine($"Первые {k} решений:");
    
    for (var i = 0; i < k; i++)
    {
        long t = i;
        var xi = x0 + stepX * t;
        var yi = y0 + stepY * t;
        
        Console.WriteLine($"t={t}: x={xi}, y={yi}");
    }
}

static long Gcd(long a, long b)
{
    while (b != 0)
    {
        var t = a % b;
        
        a = b; b = t;
    }
    
    return Math.Abs(a);
}

static (long x, long y, long gcd) ExtendedGcd(long a, long b)
{
    if (b == 0)
    {
        return (1, 0, a);
    }

    var (x1, y1, gcd) = ExtendedGcd(b, a % b);
    
    return (y1, x1 - (a / b) * y1, gcd);
}
