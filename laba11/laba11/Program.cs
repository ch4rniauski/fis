const int k = 100;

SolveDiophantine(2, 3, 5, k);
SolveDiophantine(3, -6, 23, k);
SolveDiophantine(2, 4, 8, k);
SolveDiophantine(17, 35, 95, k);
SolveDiophantine(15, 95, -205, k);
SolveDiophantine(-279, 2547, 15219, k);

// Решение уравнения a*x + b*y = c
static void SolveDiophantine(long a, long b, long c, int k)
{
    Console.WriteLine($"\nУравнение: {a}*x + {b}*y = {c}");

    var (g, x0, y0) = ExtendedGcd(Math.Abs(a), Math.Abs(b));

    if (c % g != 0)
    {
        Console.WriteLine("Нет решений (gcd не делит c).");
        
        return;
    }

    // Частное решение
    x0 *= c / g;
    y0 *= c / g;

    if (a < 0) x0 = -x0;
    if (b < 0) y0 = -y0;

    Console.WriteLine($"Частное решение: x0 = {x0}, y0 = {y0}");

    // Формулы общего решения:
    // x = x0 + (b/g)*t
    // y = y0 - (a/g)*t
    var dx = b / g;
    var dy = -a / g;

    Console.WriteLine($"Общее решение: x = {x0} + {dx}*t, y = {y0} + {dy}*t");
    Console.WriteLine($"\nПервые {k} решений:");
    
    for (var t = 0; t < k; t++)
    {
        var x = x0 + dx * t;
        var y = y0 + dy * t;
        
        Console.WriteLine($"t={t}: x={x}, y={y}");
    }
}

static (long gcd, long x, long y) ExtendedGcd(long a, long b)
{
    if (b == 0)
    {
        return (a, 1, 0);
    }
    
    var (g, x1, y1) = ExtendedGcd(b, a % b);
    
    return (g, y1, x1 - (a / b) * y1);
}
