var values = new List<int>();

Console.Write("Введите значения m для исследования (через пробел): ");
var input = Console.ReadLine();

try
{
    values = input!.Split(' ')
        .Where(s => !string.IsNullOrWhiteSpace(s))
        .Select(int.Parse)
        .Where(m => m > 0)
        .ToList();
}
catch
{
    Console.WriteLine("Некорректный ввод");
    return;
}

if (values.Count == 0)
{
    Console.WriteLine("Некорректное значение");
    return;
}

var results = new List<RingAnalysisResult>(values.Count);

foreach (var m in values)
{
    Console.WriteLine($"\nАнализ кольца Z{m}");

    var result = AnalyzeRing(m);
    
    results.Add(result);
}

Console.WriteLine("\nСравнительный анализ коэффициентов разброса");

PerformComparativeAnalysis(results);

static RingAnalysisResult AnalyzeRing(int m)
{
    var result = new RingAnalysisResult
    {
        M = m
    };

    Console.WriteLine($"\nТаблица сложения по модулю {m}:");
    
    var addTable = GenerateTable(m, (a, b) => (a + b) % m);
    PrintTable(addTable);

    var addStats = AnalyzeTable(addTable, m);
    result.AddScatterDiameter = PrintStatistics(addStats, "сложения", m);
 
    Console.WriteLine($"\nТаблица умножения по модулю {m}:");
    
    var mulTable = GenerateTable(m, (a, b) => (a * b) % m);
    PrintTable(mulTable);

    var mulStats = AnalyzeTable(mulTable, m);
    result.MultiplicationSpread = PrintStatistics(mulStats, "умножения", m);

    MakeConclusions(addStats, mulStats, m);

    return result;
}

static int[,] GenerateTable(int m, Func<int, int, int> operation)
{
    var table = new int[m, m];
    
    for (var i = 0; i < m; i++)
    {
        for (var j = 0; j < m; j++)
        {
            table[i, j] = operation(i, j);
        }
    }
    
    return table;
}

static void PrintTable(int[,] table)
{
    var m = table.GetLength(0);

    Console.Write("   ");
    
    for (var j = 0; j < m; j++)
    {
        Console.Write($"{j, 3}");
    }
    
    Console.WriteLine();

    for (var i = 0; i < m; i++)
    {
        Console.Write($"{i,2} ");
        
        for (var j = 0; j < m; j++)
        {
            Console.Write($"{table[i, j],3}");
        }
        
        Console.WriteLine();
    }
}

static TableStatistics AnalyzeTable(int[,] table, int m)
{
    var countsOfEachNumber = new int[m];
    var total = m * m;

    for (var i = 0; i < m; i++)
    {
        for (var j = 0; j < m; j++)
        {
            countsOfEachNumber[table[i, j]]++;
        }
    }

    var frequenciesOfEachNumber = countsOfEachNumber
        .Select(c => (double)c / total * 100)
        .ToArray();

    return new TableStatistics
    {
        CountsOfEachNumber = countsOfEachNumber,
        FrequenciesOfEachNumber = frequenciesOfEachNumber,
        Total = total
    };
}

static double PrintStatistics(TableStatistics stats, string operationName, int m)
{
    Console.WriteLine($"\nСтатистика для таблицы {operationName}:");

    for (var i = 0; i < m; i++)
    {
        Console.WriteLine($"Вычет {i}: {stats.CountsOfEachNumber[i]} раз ({stats.FrequenciesOfEachNumber[i]}%)");
    }

    var sumFrequencies = stats.FrequenciesOfEachNumber.Sum();
    var sumCounts = stats.CountsOfEachNumber.Sum();

    Console.WriteLine("\nПроверка:");
    Console.WriteLine($"Сумма частот: {sumFrequencies}% (должно быть 100%)");
    Console.WriteLine($"Сумма появлений: {sumCounts} (должно быть {stats.Total})");
    
    var maxFreq = stats.FrequenciesOfEachNumber.Max();
    var minFreq = stats.FrequenciesOfEachNumber.Min();
    var scatterDiameter = maxFreq / minFreq;

    Console.WriteLine($"Максимальная частота: {maxFreq}%");
    Console.WriteLine($"Минимальная частота: {minFreq}%");
    Console.WriteLine($"Диаметр разброса: {scatterDiameter}");

    return scatterDiameter;
}

static void MakeConclusions(TableStatistics addStats, TableStatistics mulStats, int m)
{
    Console.WriteLine($"\nВыводы для Z{m}:");

    Console.WriteLine("\nТаблица сложения:");
    var addMax = addStats.FrequenciesOfEachNumber.Max();
    var addMin = addStats.FrequenciesOfEachNumber.Min();

    if (addMax != addMin)
    {
        var mostFrequent = Array.IndexOf(addStats.FrequenciesOfEachNumber, addMax);
        var leastFrequent = Array.IndexOf(addStats.FrequenciesOfEachNumber, addMin);
        Console.WriteLine($"- Чаще всего встречается вычет {mostFrequent} ({addMax}%)");
        Console.WriteLine($"- Реже всего встречается вычет {leastFrequent} ({addMin}%)");
    }
    else
    {
        Console.WriteLine("- Все вычеты встречаются одинаково часто (равномерное распределение)");
    }

    Console.WriteLine("\nТаблица умножения:");
    var mulMax = mulStats.FrequenciesOfEachNumber.Max();
    var mulMin = mulStats.FrequenciesOfEachNumber.Min();

    if (mulMin != mulMax)
    {
        var mostFrequent = Array.IndexOf(mulStats.FrequenciesOfEachNumber, mulMax);
        var leastFrequent = Array.IndexOf(mulStats.FrequenciesOfEachNumber, mulMin);
        Console.WriteLine($"- Чаще всего встречается вычет {mostFrequent} ({mulMax}%)");
        Console.WriteLine($"- Реже всего встречается вычет {leastFrequent} ({mulMin}%)");
    }
    else
    {
        Console.WriteLine("- Все вычеты встречаются одинаково часто (равномерное распределение)");
    }
}

static void PerformComparativeAnalysis(List<RingAnalysisResult> results)
{
    Console.WriteLine("\nТаблица коэффициентов разброса:");
    Console.WriteLine("m\tСложение\tУмножение\tРазность");

    foreach (var result in results)
    {
        var diff = result.MultiplicationSpread - result.AddScatterDiameter;
        Console.WriteLine($"{result.M}\t{result.AddScatterDiameter}\t\t{result.MultiplicationSpread}\t\t{diff}");
    }

    Console.WriteLine("\nЗакономерности:");

    var alwaysAdditionLarger = results.All(r => r.AddScatterDiameter >= r.MultiplicationSpread);
    var alwaysMultiplicationLarger = results.All(r => r.MultiplicationSpread >= r.AddScatterDiameter);

    if (alwaysAdditionLarger)
    {
        Console.WriteLine("Коэффициент разброса для сложения всегда больше или равен коэффициенту для умножения");
    }
    else if (alwaysMultiplicationLarger)
    {
        Console.WriteLine("Коэффициент разброса для умножения всегда больше или равен коэффициенту для сложения");
    }
    else
    {
        Console.WriteLine("Коэффициенты разброса изменяются по-разному для разных m");
    }

    var addSpreads = results
        .Select(r => r.AddScatterDiameter)
        .ToList();
    var mulSpreads = results
        .Select(r => r.MultiplicationSpread)
        .ToList();

    Console.WriteLine("\nДиапазоны изменения:");
    Console.WriteLine($"Сложение: от {addSpreads.Min()} до {addSpreads.Max()}");
    Console.WriteLine($"Умножение: от {mulSpreads.Min()} до {mulSpreads.Max()}");
}

internal class TableStatistics
{
    public int[] CountsOfEachNumber { get; init; } = null!;
    public double[] FrequenciesOfEachNumber { get; init; } = null!;
    public int Total { get; init; }
}

internal class RingAnalysisResult
{
    public int M { get; init; }
    public double AddScatterDiameter { get; set; }
    public double MultiplicationSpread { get; set; }
}
