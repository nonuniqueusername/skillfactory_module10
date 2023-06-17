using System.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        ILogger logger = new Logger();
        ICalculator calculator = new Calculator(logger);
        try
        {
            Console.Write("Введите первое число: ");
            int number1 = int.Parse(Console.ReadLine());

            Console.Write("Введите второе число: ");
            int number2 = int.Parse(Console.ReadLine());

            int result = calculator.Add(number1, number2);
            Console.WriteLine("Результат сложения: " + result);
        }
        catch (FormatException)
        {
            Console.WriteLine("Введено не число");
        }
        catch
        {
            Console.WriteLine("Ошибка");
        }
    }
}

public interface ICalculator
{
    int Add(int a, int b);
}

public class Calculator : ICalculator
{
    private readonly ILogger _logger;

    public Calculator(ILogger logger)
    {
        _logger = logger;
    }

    public int Add(int a, int b)
    {
        try
        {
            int result = a + b;
            _logger.WriteInfo($"Выполнено сложение {a} + {b} = {result}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.WriteError($"Ошибка при сложении: {ex.Message}");
            throw ex;
        }
    }
}

public interface ILogger
{
    void WriteInfo(string message);
    void WriteError(string message);
}
public class Logger : ILogger
{
    public void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void WriteInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

