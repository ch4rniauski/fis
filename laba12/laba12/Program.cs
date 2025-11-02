Console.Write("Введите число: ");
var n = int.Parse(Console.ReadLine()!);

Console.WriteLine($"phi({n}) = {EulerPhi(n)}");

static int EulerPhi(int m)
{
    var result = m;
    var temp = m;

    for (var p = 2; p * p <= temp; p++)
    {
        if (temp % p == 0)
        {
            while (temp % p == 0)
            {
                temp /= p;
            }

            result -= result / p;
        }
    }

    if (temp > 1)
    {
        result -= result / temp;
    }

    return result;
}
