namespace TCSA.Calculator.Console.Models;

public class Calculation
{
    public DateTime DateTime { get; set; }
    public Operations Operation { get; set; }
    public double[] Numbers { get; set; }
    public double Result { get; set; }
}

public enum Operations
{
    None,
    Addition,
    Subtraction,
    Multiplication,
    Division,
    Power,
    SquareRoot,
    TimesTen,
    Sin,
    Cos,
    Tan,
    History
}

public enum SingleOperations
{
    SquareRoot,
    TimesTen,
    Sin,
    Cos,
    Tan
}