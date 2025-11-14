using System;
using TCSA.Calculator.Console.Models;

var calculationList = new List<Calculation>();
var repeatProcess = true;
var processCounter = 0;

do
{
    if (processCounter > 0)
        Console.WriteLine($"Calculator has been used {processCounter} times.");

    Console.Clear();
    Console.WriteLine(@"Select an operation: ");
    Console.WriteLine("\tA/a - Addition");
    Console.WriteLine("\tS/s - Subtraction");
    Console.WriteLine("\tM/m - Multiplication");
    Console.WriteLine("\tD/d - Division");
    Console.WriteLine("\tP/p - Power");
    Console.WriteLine("\tSR/sr - Square Root");
    Console.WriteLine("\tX/x - Times Ten (10X)");
    Console.WriteLine("\tSIN/sin - Sin");
    Console.WriteLine("\tCOS/cos - Cos");
    Console.WriteLine("\tTAN/tan - Tan");
    Console.WriteLine("\tH/h - Show calculator history");

    var operation = GetOperationInput();

    if (operation == Operations.History)
    {
        if (calculationList.Count == 0)
        {
            Console.WriteLine("No History Yet.");
        }
        else
        {
            Console.WriteLine("Date\tOperation\tNumber/s\tResult");

            foreach (var calculation in calculationList)
            {
                Console.WriteLine($"{calculation.DateTime}\t{calculation.Operation.ToString()}\t{calculation.Numbers[0]}{(calculation.Numbers[1] != 0 ? $", {calculation.Numbers[1]}" : "")}\t{calculation.Result}");
            }

            Console.WriteLine("\nDelete list? (Y/N)");

            if (ValidateYesOrNo())
                calculationList.Clear();
        }

        Console.ReadLine();
        continue;
    }
    else
    {
        double[] numbers = GetInputNumbers(operation);

        double result = RunCalculation(operation, numbers);
        StoreCalculations(operation, numbers, result);

        Console.WriteLine($"Result for {operation.ToString()} operation is: {result}");
    }

    Console.WriteLine("Calculate again? (Y/N)");
    if (!ValidateYesOrNo())
    {
        repeatProcess = false;
    }

} while (repeatProcess);

double[] GetInputNumbers(Operations operation)
{
    double[] numbers = new double[2];

    if (VerifyUsePreviousResult())
    {
        numbers[0] = calculationList.Last().Result;
    }
    else
    {
        Console.WriteLine(@"Input first number:");
        numbers[0] = ValidateNumberInput();
    }

    if (!Enum.TryParse<SingleOperations>(operation.ToString(), out _))
    {
        Console.WriteLine(@"Input second number:");
        numbers[1] = ValidateNumberInput();
    }

    return numbers;
}

void StoreCalculations(Operations operation, double[] numbers, double result)
{
    calculationList.Add(
        new Calculation { 
            DateTime = DateTime.Now,
            Operation = operation, 
            Numbers = numbers, 
            Result = result 
        }
    );
}

double RunCalculation(Operations operation, double[] numbers)
{
    double result = double.NaN;

    result = operation switch
    {
        Operations.Addition => numbers[0] + numbers[1],
        Operations.Subtraction => numbers[0] - numbers[1],
        Operations.Multiplication => numbers[0] * numbers[1],
        Operations.Division => numbers[0] / numbers[1],
        Operations.Power => Math.Pow(numbers[0], numbers[1]),
        Operations.SquareRoot => Math.Sqrt(numbers[0]),
        Operations.TimesTen => numbers[0] * 10,
        Operations.Sin => Math.Sin(numbers[0]),
        Operations.Cos => Math.Cos(numbers[0]),
        Operations.Tan => Math.Tan(numbers[0]),
    };

    return result;
}

Operations GetOperationInput()
{
    Operations operation = default;
    do
    {
        var result = Console.ReadLine();

        operation = result.ToLower() switch
        {
            "a" => Operations.Addition,
            "s" => Operations.Subtraction,
            "m" => Operations.Multiplication,
            "d" => Operations.Division,
            "p" => Operations.Power,
            "sr" => Operations.SquareRoot,
            "x" => Operations.TimesTen,
            "sin" => Operations.Sin,
            "cos" => Operations.Cos,
            "tan" => Operations.Tan,
            "h" => Operations.History,
            _ => Operations.None
        };

        if (operation == Operations.None)
            Console.WriteLine("Invalid input! Select a proper operation from the list");

    } while (operation == default);

    return operation;
}

bool VerifyUsePreviousResult()
{
    bool isUseResult = false;

    if (calculationList.Count > 0)
    {
        Console.WriteLine($"Use previous result in new calculation? (Y/N)");
        isUseResult = ValidateYesOrNo();
    }

    return isUseResult;
}

double ValidateNumberInput()
{
    var value = double.NaN;
    var isInputCorrect = false;
    do
    {
        var input = Console.ReadLine();
        isInputCorrect = double.TryParse(input, out value);

        if (!isInputCorrect)
            Console.WriteLine("Incorrect Input! Please input a proper number.");
    } while (!isInputCorrect);

    return value;
}

bool ValidateYesOrNo()
{
    bool yesOrNo = false;
    bool isValid = false;

    do
    {
        var input = Console.ReadLine();

        if (input.ToLower() == "y" || input.ToLower() == "n")
        {
            yesOrNo = input.ToLower() switch
            {
                "y" => true,
                "n" => false
            };
            isValid = true;
        }

        if (!isValid)
            Console.WriteLine("Invalid Input! Select 'Y' if yes and 'N' for no");
    } while (!isValid);

    return yesOrNo;
}