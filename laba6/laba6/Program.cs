(int, int)[] pairs =
[
    (2,4), (0,10), (10,12), (16,20), (25,45), (100,175), (375,400)
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
    