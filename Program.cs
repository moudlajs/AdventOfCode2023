using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks.Sources;
using System.Collections;


// Make it with class and unit tests
Dictionary<int, string> digits = new Dictionary<int, string>
{
    { 1, "one" },
    { 2, "two"},
    { 3, "three"},
    { 4, "four"},
    { 5, "five"},
    { 6, "six"},
    { 7, "seven"},
    { 8, "eight"},
    { 9, "nine"}
};

static List<string> GetListOfPuzzles()
{
    var listOfPuzzles = new List<string>();
    var filePath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Day1\\PuzzleInput.txt");
    using StreamReader StreamReader = new StreamReader(filePath);

    while (StreamReader.Peek() > 0)
    {
        var line = StreamReader.ReadLine();
        listOfPuzzles.Add(line);
    }
    return listOfPuzzles;
}

static Dictionary<string, string> WriteResultToFile(Dictionary<string, string> results)
{
    var filePath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Day1\\PuzzleOutput.txt");
    using StreamWriter streamWriter = new StreamWriter(filePath);

    foreach(var line in results)
        streamWriter.WriteLine($"{line.Key} = {line.Value}");
    return results;
}


static Dictionary<int, int> GetFirstNumberAsDigit(string line)
{
    var number = String.Join("", line.ToCharArray().Where(Char.IsDigit));
    var valueOfNumber = number.Substring(0, 1);
    var indexOfNumber = line.IndexOf(valueOfNumber);

    return new Dictionary<int, int>()
    {
        { indexOfNumber, int.Parse(valueOfNumber) }
    };
}

static KeyValuePair<int, string> GetFirstNumberAsString(Dictionary<int, string> numbers, string line)
{
    var allStringDigits = new Dictionary<int, string>();
    foreach (var number in numbers)
    {
        if (line.Contains(number.Value))
        {
            var indexOfNumber = line.IndexOf(number.Value);
            var valueOfNumber = number.Key.ToString();
            allStringDigits.Add(indexOfNumber, valueOfNumber);
        }
    }
    return allStringDigits.Count == 0 ? new KeyValuePair<int, string>(0, String.Empty) : allStringDigits.First();
}

static string GetRealFirstNumber(Dictionary<int, string> numbers, string line)
{
    var numberAsDigit = GetFirstNumberAsDigit(line).Values.First();
    var numberAsString = GetFirstNumberAsString(numbers, line).Value;
    var indexNumberAsDigit = GetFirstNumberAsDigit(line).Keys.First();
    var indexNumberAsString = GetFirstNumberAsString(numbers, line).Key;

    return indexNumberAsDigit < indexNumberAsString ? numberAsDigit.ToString() : numberAsString;
}

static Dictionary<int, int> GetLastNumberAsDigit(string line)
{
    var number = String.Join("", line.ToCharArray().Where(Char.IsDigit));
    var valueOfNumber = number.Substring(number.Length - 1, 1);
    var indexOfNumber = line.IndexOf(valueOfNumber);

    return new Dictionary<int, int>()
    {
        { indexOfNumber, int.Parse(valueOfNumber) }
    };
}

static KeyValuePair<int, string> GetLastNumberAsString(Dictionary<int, string> numbers, string line)
{
    var allStringDigits = new Dictionary<int, string>();
    foreach (var number in numbers)
    {
        if (line.Contains(number.Value))
        {
            var indexOfNumber = line.LastIndexOf(number.Value);
            var valueOfNumber = number.Key.ToString();
            allStringDigits.Add(indexOfNumber, valueOfNumber);
        }
    }
    if (allStringDigits.Count == 0)
        return new KeyValuePair<int, string>(0, String.Empty);
    else
        return allStringDigits.First(x => x.Key == allStringDigits.Keys.Max());
}

static string GetRealLastNumber(Dictionary<int, string> numbers, string line)
{
    var numberAsDigit = GetLastNumberAsDigit(line).Values.First();
    var numberAsString = GetLastNumberAsString(numbers, line).Value;
    var indexNumberAsDigit = GetLastNumberAsDigit(line).Keys.First();
    var indexNumberAsString = GetLastNumberAsString(numbers, line).Key;
 
    return indexNumberAsDigit > indexNumberAsString ? numberAsDigit.ToString() : numberAsString;
}

    static bool IsPuzzleSingleDigit(string line)
{
    var number = String.Join("", line.ToCharArray().Where(Char.IsDigit));
    return number.Length == 1;
}

static string GetNumberIfPuzzleIsSingleDigit(string line)
{
    Regex re = new Regex(@"\d+");
    var match = re.Match(line);
    var number = match.Value;
    if (number.Length == 1)
        return number + number;
    else
        throw new Exception($"Something went wrong.\n Actual number was: {number}");
}
static int GetFinalScore(Dictionary<int, string> digits)
{
    var listOfPuzzles = GetListOfPuzzles();
    var sumsOfnumbers = new List<string>();
    var puzzleResults = new Dictionary<string, string>();

    foreach (var line in listOfPuzzles)
    {
        if (IsPuzzleSingleDigit(line))
        {
            var singleDigitNumber = GetNumberIfPuzzleIsSingleDigit(line);
            sumsOfnumbers.Add(singleDigitNumber);
        }
        else
        {
            var firstNmber = GetRealFirstNumber(digits, line);
            var lastNumber = GetRealLastNumber(digits, line);

            if (!string.IsNullOrEmpty(firstNmber) || !string.IsNullOrEmpty(lastNumber))
            {
                var sumOfNumbers = firstNmber.ToString() + lastNumber.ToString();
                sumsOfnumbers.Add(sumOfNumbers);
                puzzleResults.Add(line, sumOfNumbers);
            }
        }
        WriteResultToFile(puzzleResults);
    }
    return sumsOfnumbers.Select(s => int.Parse(s)).Sum();
}

//Console.WriteLine(GetRealFirstNumber(digits, "4nineeightseven2"));
//Console.WriteLine(GetRealLastNumber(digits, "8ln2"));
//Console.WriteLine(GetFirstNumberAsDigit("adstwo1nine"));
//Console.WriteLine(GetFirstNumberAsString(digits, "8ln2"));
//Console.WriteLine(IsPuzzleSingleDigit("four8flptk"));
//Console.WriteLine(GetLastNumberAsString(digits, "2tqbxgrrpmxqfglsqjkqthree6nhjvbxpflhr1eighthr"));
//Console.WriteLine(GetLastNumberAsDigit("2tqbxgrrpmxqfglsqjkqthree6nhjvbxpflhr1eightwohr"));
Console.WriteLine(GetFinalScore(digits));