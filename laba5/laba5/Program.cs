Console.Write("Введите число m: ");
var m = int.Parse(Console.ReadLine()!);

CheckField(m);

void CheckField(int m)
{
    Console.Write($"\nПроверка Z{m}: ");

    for (var a = 1; a < m; a++)
    {
        for (var b = 1; b < m; b++)
        {
            var result = (a * b) % m;

            if (result == 0 && a != 0 && b != 0)
            {
                Console.WriteLine("Найден нетривиальный делитель нуля");
            }
        }
    }

    PrintTable(m);
}

void PrintTable(int m)
{
    Console.WriteLine($"\nТаблица для Z{m}");
    Console.WriteLine($"{"n",3} | {"Обратимый",9} | {"Делитель нуля",13}");

    for (var i = 0; i < m; i++)
    {
        var isInvertible = i != 0 && Gcd(i, m) == 1;
        var isZeroDivisor = i == 0 || Gcd(i, m) != 1;

        Console.WriteLine(
            $"{i,3} | {(isInvertible ? "Да" : "Нет"),9} | {(isZeroDivisor ? "Да" : "Нет"),13}"
        );
    }
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
