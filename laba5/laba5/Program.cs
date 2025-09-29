int[] primes = [2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 97];

foreach (var p in primes)
{
    Console.WriteLine($"\nПроверка Z{p}:");

    var isField = true;

    // Проверка отсутствия нетривиальных делителей нуля и обратимости элементов
    for (var a = 1; a < p; a++) // 0 не проверяем: 0 всегда делитель нуля
    {
        var hasInverse = false;
        
        for (var b = 1; b < p; b++)
        {
            var result = (a * b) % p;

            // Если произведение дает 0 при ненулевых множителях, есть делитель нуля.
            if (result == 0)
            {
                isField = false;
                Console.WriteLine($"\tНайден нетривиальный делитель нуля: {a}*{b} ≡ 0 (mod {p})");
            }

            if (result == 1)
            {
                hasInverse = true; // Нашелся обратимый элемент
            }
        }
        
        if (!hasInverse)
        {
            isField = false;
            Console.WriteLine($"\tНе найден обратный элемент для {a} в Z{p}");
        }
    }

    Console.WriteLine(isField
        ? $"\tZ{p} — поле: только тривиальный делитель 0 (нулевой элемент), остальные элементы обратимы."
        : $"\tZ{p} не является полем.");
}
