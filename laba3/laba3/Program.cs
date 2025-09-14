int[] array = [4, 6, 8, 10, 20, 40, 50, 100];

foreach (var m in array)
{
    FindReversibleAndReverse(m);
}

int Gcd(int a, int b)
{
    while (b != 0)
    {
        (a, b) = (b, a % b);
    }
    
    return a;
}

ExtendedGcd ExtGcd(int a, int b)
{
    if (b == 0)
    {
        return new ExtendedGcd(1, 0, a);
    }
    
    var extendedGcd = ExtGcd(b, a % b);
    
    return new ExtendedGcd(extendedGcd.Y, extendedGcd.X - (a / b) * extendedGcd.Y, extendedGcd.Gcd);
}

// Находит обратимый элемент и его обратный
void FindReversibleAndReverse(int m)
{
    Console.WriteLine($"Z{m}:");
    
    var elems = new List<int>();
    
    for (var a = 1; a < m; a++)
    {
        if (Gcd(a, m) == 1)
        {
            // Найти обратный элемент через расширенный алгоритм Евклида
            var extendedGcd = ExtGcd(a, m);

            // Привести обратный к положительному остатку
            var inv = ((extendedGcd.X % m) + m) % m;

            Console.WriteLine($"\tОбратимый элемент: {a}, обратный: {inv}");
            
            elems.Add(a);
        }
    }
    
    Console.WriteLine($"Всего обратимых элементов: {elems.Count}\n");
}

internal readonly ref struct ExtendedGcd
{
    public int X { get; init; }
    public int Y { get; init; }
    public int Gcd { get; init; }

    public ExtendedGcd(int x, int y, int gcd)
    {
        X = x;
        Y = y;
        Gcd = gcd;
    }
}
