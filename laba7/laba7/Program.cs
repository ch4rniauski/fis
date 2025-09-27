int[] ms = [2, 4, 6, 8, 10, 15, 20, 23, 29, 40, 50, 100, 200];

foreach (var m in ms)
{
    CheckElements(m);
}

void CheckElements(int m)
{
    Console.WriteLine($"m = {m}");
    
    for (var i = 0; i < m; i++)
    {
        var gcd = Gcd(i, m);

        string elementType;
        
        if (i == 0)
        {
            elementType = "Нулевой элемент";
        }
        else if (gcd == 1)
        {
            elementType = "Обратимый элемент";
        }
        else
        {
            elementType = "Делитель нуля";
        }

        Console.WriteLine($"i = {i}, НОД(i,m) = {gcd}, {elementType}");
    }

    Console.WriteLine();
}

int Gcd(int a, int b)
{
    while (b != 0)
    {
        var temp = b;
        
        b = a % b;
        a = temp;
    }
    
    return a;
}
