int[] testMs = [4, 6, 8, 10, 20, 40, 50, 100];

foreach (var m in testMs)
{
    Console.WriteLine($"\n\nm = {m}");

    var invertible = new List<int>();
    var zeroDivisors = new List<int>();

    for (var a = 1; a < m; a++)
    {
        if (Gcd(a, m) == 1)
        {
            invertible.Add(a);
        }
        else
        {
            zeroDivisors.Add(a);
        }
    }

    Console.WriteLine("Обратимые элементы:");
    Console.WriteLine(string.Join(", ", invertible));

    Console.WriteLine("Делители нуля:");
    Console.WriteLine(string.Join(", ", zeroDivisors));

    var hasIntersection = invertible.Exists(x => zeroDivisors.Contains(x));
    Console.WriteLine(hasIntersection
        ? "Нарушение: есть элемент и обратимый, и делитель нуля"
        : "Множества не пересекаются");

    var total = invertible.Count + zeroDivisors.Count;
    Console.WriteLine(total == m - 1
        ? "Все элементы покрыты"
        : "Есть элементы вне обоих множеств");
}

int Gcd(int a, int b)
{
    while (b != 0)
    {
        var t = b;
        
        b = a % b;
        a = t;
    }
    
    return a;
}
