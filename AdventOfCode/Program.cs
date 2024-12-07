namespace AdventOfCode;

public class Program
{
    public static void Main(string[] args)
    {
        var inputNumber = EvaluateInputNumber(args);

        var filePath = Path.Combine("Day" + inputNumber, "Code.bf");

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        var brainfuckCode = File.ReadAllText(filePath);
        Console.WriteLine($"Loaded Brainfuck code from {filePath}:");
        Console.WriteLine(brainfuckCode);

        var interpreter = new BrainfuckInterpreter();
        var inputFilePath = Path.Combine("Day" + inputNumber, "input.txt");
        interpreter.Execute(brainfuckCode, inputFilePath);
    }

    private static int EvaluateInputNumber(string[] args)
    {
        if (args.Length > 0 && int.TryParse(args[0], out var inputNumber))
        {
            return inputNumber;
        }

        Console.Write("Enter a number: ");
        while (!int.TryParse(Console.ReadLine(), out inputNumber))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
            Console.Write("Enter a number: ");
        }

        return inputNumber;
    }
}