using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string expression = "+ 9 * 2 6"; // Префіксний вираз
        Console.WriteLine($"Expression: {expression}");
        double result = EvaluatePrefixExpression(expression);
        Console.WriteLine($"Result: {result}");
    }

    static double EvaluatePrefixExpression(string expression)
    {
        Stack<double> stack = new Stack<double>();
        string[] tokens = expression.Split(' ');

        // Проходимо по виразу справа наліво
        for (int i = tokens.Length - 1; i >= 0; i--)
        {
            string token = tokens[i];

            if (IsOperator(token))
            {
                // Оператор - виймаємо два операнди зі стеку
                double operand1 = stack.Pop();
                double operand2 = stack.Pop();

                // Обчислюємо результат
                double result = ApplyOperator(token, operand1, operand2);

                // Поміщаємо результат назад у стек
                stack.Push(result);
            }
            else
            {
                // Операнд - конвертуємо в число і поміщаємо у стек
                stack.Push(double.Parse(token));
            }
        }

        // Єдиний залишок у стеку - це результат виразу
        return stack.Pop();
    }

    static bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/";
    }

    static double ApplyOperator(string operatorToken, double operand1, double operand2)
    {
        switch (operatorToken)
        {
            case "+":
                return operand1 + operand2;
            case "-":
                return operand1 - operand2;
            case "*":
                return operand1 * operand2;
            case "/":
                return operand1 / operand2;
            default:
                throw new ArgumentException($"Unknown operator: {operatorToken}");
        }
    }
}
