using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day01
{
    public class Trebuchet
    {
        // Make it with class and unit tests
        public static Dictionary<int, string> digits = new Dictionary<int, string>
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

        private static List<string> GetListOfPuzzles()
        {
            var listOfPuzzles = new List<string>();
            var filePath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Day01\\PuzzleInput.txt");
            using StreamReader StreamReader = new StreamReader(filePath);

            while (StreamReader.Peek() > 0)
            {
                var line = StreamReader.ReadLine();
                listOfPuzzles.Add(line);
            }
            return listOfPuzzles;
        }

        private static Dictionary<string, string> WriteResultToFile(Dictionary<string, string> results)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Day01\\PuzzleOutput.txt");
            using StreamWriter streamWriter = new StreamWriter(filePath);

            foreach (var line in results)
                streamWriter.WriteLine($"{line.Key} = {line.Value}");
            return results;
        }

        public static Dictionary<int, string> GetFirstNumberAsDigit(string line)
        {
            var number = String.Join("", line.ToCharArray().Where(Char.IsDigit));
            if (number.Length != 0)
            {
                var valueOfNumber = number.Substring(0, 1);
                var indexOfNumber = line.IndexOf(valueOfNumber);
                return new Dictionary<int, string>()
                {
                    { indexOfNumber, valueOfNumber }
                };
            }
            else
            {
                var valueOfNumber = String.Empty;
                var indexOfNumber = line.IndexOf(valueOfNumber);
                return new Dictionary<int, string>()
                {
                    { indexOfNumber, valueOfNumber }
                };
            }
        }

        public static KeyValuePair<int, string> GetFirstNumberAsString(Dictionary<int, string> numbers, string line)
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

            KeyValuePair<int, string> firstEntry = allStringDigits
                .Where(x => x.Key == allStringDigits.Keys.Min())
                .FirstOrDefault();

            return allStringDigits.Count == 0 ? new KeyValuePair<int, string>(999, String.Empty) : firstEntry;
        }

        public static string GetRealFirstNumber(Dictionary<int, string> numbers, string line)
        {
            var numberAsDigit = GetFirstNumberAsDigit(line).Values.First();
            var numberAsString = GetFirstNumberAsString(numbers, line).Value;
            var indexNumberAsDigit = GetFirstNumberAsDigit(line).Keys.First();
            var indexNumberAsString = GetFirstNumberAsString(numbers, line).Key;

            return indexNumberAsDigit < indexNumberAsString ? numberAsDigit : numberAsString;
        }

        static Dictionary<int, string> GetLastNumberAsDigit(string line)
        {
            var number = String.Join("", line.ToCharArray().Where(Char.IsDigit));
            if (number.Length != 0)
            {
                var valueOfNumber = number.Substring(number.Length - 1, 1);
                var indexOfNumber = line.IndexOf(valueOfNumber);
                return new Dictionary<int, string>()
                {
                    { indexOfNumber, valueOfNumber }
                };
            }
            else
            {
                var valueOfNumber = String.Empty;
                var indexOfNumber = line.IndexOf(valueOfNumber);
                return new Dictionary<int, string>()
                {
                    { indexOfNumber, valueOfNumber }
                };
            }
        }

        public static KeyValuePair<int, string> GetLastNumberAsString(Dictionary<int, string> numbers, string line)
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

            KeyValuePair<int, string> lastEntry = allStringDigits
                .Where(x => x.Key == allStringDigits.Keys.Max())
                .FirstOrDefault();

            return allStringDigits.Count == 0 ? new KeyValuePair<int, string>(-1, String.Empty) : lastEntry;
        }

        public static string GetRealLastNumber(Dictionary<int, string> numbers, string line)
        {
            var numberAsDigit = GetLastNumberAsDigit(line).Values.First();
            var numberAsString = GetLastNumberAsString(numbers, line).Value;
            var indexNumberAsDigit = GetLastNumberAsDigit(line).Keys.First();
            var indexNumberAsString = GetLastNumberAsString(numbers, line).Key;

            return indexNumberAsDigit > indexNumberAsString ? numberAsDigit : numberAsString;
        }

        public static bool IsPuzzleSingleDigit(string line)
        {
            var number = String.Join("", line.ToCharArray().Where(Char.IsDigit));
            return number.Length == 1;
        }

        public static string GetNumberIfPuzzleIsSingleDigit(string line)
        {
            Regex re = new Regex(@"\d+");
            var match = re.Match(line);
            var number = match.Value;
            return number.Length == 1 ? number : String.Empty;
        }

        public static int GetFinalScore(Dictionary<int, string> digits)
        {
            var listOfPuzzles = GetListOfPuzzles();
            var sumsOfnumbers = new List<string>();
            var puzzleResults = new Dictionary<string, string>();

            foreach (var line in listOfPuzzles)
            {
                // Vracat tu aj idnexy 
                var firstNmber = GetRealFirstNumber(digits, line);
                var lastNumber = GetRealLastNumber(digits, line);

                // ostertit ten single digit case ale 
                if (IsPuzzleSingleDigit(line))
                {
                    var singleDigitNumber = GetNumberIfPuzzleIsSingleDigit(line);
                    var sumOfNumbers = singleDigitNumber + lastNumber;
                    sumsOfnumbers.Add(sumOfNumbers);
                    puzzleResults.Add(line, sumOfNumbers);
                }
                else
                {
                    var sumOfNumbers = firstNmber.ToString() + lastNumber.ToString();
                    sumsOfnumbers.Add(sumOfNumbers);
                    puzzleResults.Add(line, sumOfNumbers);
                }
                WriteResultToFile(puzzleResults);
            }
            var finalScore = sumsOfnumbers.Select(s => int.Parse(s)).Sum();

            Console.WriteLine(finalScore);
            return finalScore;
        }
    }
}
