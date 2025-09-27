using System.Numerics;

int[] primesToCheck = [2, 3, 5, 7, 11, 13, 19, 23, 97, 193];

foreach (var p in primesToCheck)
{
    CheckFermat(p);
}

Console.WriteLine("Проверка обратного утверждения малой теоремы Ферма для m от 2 до 5000");
CheckConverseFermat(5000);

void CheckFermat(int p)
{
    Console.WriteLine($"p = {p}");
    for (var a = 1; a < p; a++)
    {
        var result = BigInteger.ModPow(a, p - 1, p);
        
        Console.WriteLine($"a = {a}, a^{p - 1} mod {p} = {result}");
        
        if (result != 1)
        {
            Console.WriteLine("Ошибка! Малая теорема Ферма не выполнена\n");
            
            return;
        }
    }
    
    Console.WriteLine("Все числа удовлетворяют малой теореме Ферма (значения равны 1)\n");
}

// Простая проверка простоты числа
bool IsPrime(int x)
{
    switch (x)
    {
        case < 2:
            return false;
        case 2:
            return true;
    }

    if (x % 2 == 0)
    {
        return false;
    }

    var limit = (int)Math.Sqrt(x);
    
    for (var i = 3; i <= limit; i += 2)
    {
        if (x % i == 0)
        {
            return false;
        }
    }
    
    return true;
}

// Проверка «обратного» утверждения Ферма для m от 2 до maxM
void CheckConverseFermat(int maxM)
{
    for (var m = 2; m <= maxM; m++)
    {
        var holdsForAllCoprimeA = true;

        for (var a = 1; a < m; a++)
        {
            if (BigInteger.GreatestCommonDivisor(a, m) != 1)
            {
                continue; // a и m не взаимно просты — не проверяем
            }

            var res = BigInteger.ModPow(a, m - 1, m);
            
            if (res != 1)
            {
                holdsForAllCoprimeA = false;
                
                break;
            }
        }

        // Если условие выполнилось для всех взаимно простых a, но m составное — выводим
        if (holdsForAllCoprimeA && !IsPrime(m))
        {
            Console.WriteLine($"Число {m} составное, но для всех взаимно простых a выполняется a^(m-1) ≡ 1 (mod {m})");
        }
    }
}
