(int, int)[] pairs =
[
    (6,8)
];

foreach (var (a, b) in pairs)
{
    Console.WriteLine($"НОД({a},{b}) = {Gcd(a, b)}");
}

int Gcd(int a, int b)
{
    while (b != 0)
    {
        var t = b;
        
        b = a % b;
        a = t;
    }
    
    return Math.Abs(a);
}
