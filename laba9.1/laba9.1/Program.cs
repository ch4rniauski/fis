using System.Numerics;

const int p = 5000;

Console.WriteLine($"Проверка обратного утверждения малой теоремы Ферма для p = {p}:");
CheckConverseFermat(5000);

void CheckConverseFermat(int maxM)
{
    for (var localM = 2; localM <= maxM; localM++)
    {
        var holdsForAllCoprimeA = true;

        for (var a = 1; a < localM; a++)
        {
            if (BigInteger.GreatestCommonDivisor(a, localM) != 1)
            {
                continue;
            }

            var res = BigInteger.ModPow(a, localM - 1, localM);
            
            if (res != 1)
            {
                holdsForAllCoprimeA = false;
                
                break;
            }
        }

        if (holdsForAllCoprimeA && !IsPrime(localM))
        {
            Console.WriteLine($"\tЧисло {localM} составное, но для всех взаимно простых a выполняется a^(m-1) = 1 (mod {localM})");
        }
    }
}

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
