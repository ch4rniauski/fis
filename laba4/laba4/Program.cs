Console.Write("Введите число m: ");
var m = int.Parse(Console.ReadLine()!);

PrintTable(m);

void PrintTable(int m)
{
    Console.WriteLine($"\nТаблица для Z{m}");
    Console.WriteLine($"{"n",3} | {"НОД(i,m)",8} | {"Обратимый",9} | {"Делитель нуля",13}");

    for (var i = 1; i < m; i++)
    {
        var gcd = Gcd(i, m);

        var isInvertible = i != 0 && gcd == 1;
        var isZeroDivisor = i != 0 && gcd != 1;

        Console.WriteLine(
            $"{i,3} | {gcd,8} | {(isInvertible ? "Да" : "Нет"),9} | {(isZeroDivisor ? "Да" : "Нет"),13}"
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
