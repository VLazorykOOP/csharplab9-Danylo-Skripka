using System;
using System.Collections;
using System.Collections.Generic;

class PrefixExpression : IEnumerable, ICloneable
{
    private ArrayList expression;

    public PrefixExpression(string expr)
    {
        expression = new ArrayList(expr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
    }

    public IEnumerator GetEnumerator()
    {
        return expression.GetEnumerator();
    }

    public object Clone()
    {
        return new PrefixExpression(string.Join(" ", expression.ToArray()));
    }

    public double Evaluate()
    {
        Stack<double> stack = new Stack<double>();

        for (int i = expression.Count - 1; i >= 0; i--)
        {
            string token = (string)expression[i];

            if (IsOperator(token))
            {
                double operand1 = stack.Pop();
                double operand2 = stack.Pop();
                double result = ApplyOperator(token, operand1, operand2);
                stack.Push(result);
            }
            else
            {
                stack.Push(double.Parse(token));
            }
        }

        return stack.Pop();
    }

    private bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/";
    }

    private double ApplyOperator(string operatorToken, double operand1, double operand2)
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

class PrefixExpressionComparer : IComparer
{
    public int Compare(object x, object y)
    {
        PrefixExpression expr1 = x as PrefixExpression;
        PrefixExpression expr2 = y as PrefixExpression;

        if (expr1 == null || expr2 == null)
        {
            throw new ArgumentException("Objects are not PrefixExpression instances");
        }

        double value1 = expr1.Evaluate();
        double value2 = expr2.Evaluate();

        return value1.CompareTo(value2);
    }
}

class Program
{
    static void Main(string[] args)
    {
        PrefixExpression expr1 = new PrefixExpression("+ 9 * 2 6"); // Префіксний вираз
        PrefixExpression expr2 = new PrefixExpression("- 5 + 3 2");

        Console.WriteLine($"Expression 1: {string.Join(" ", expr1)}");
        Console.WriteLine($"Result 1: {expr1.Evaluate()}");

        Console.WriteLine($"Expression 2: {string.Join(" ", expr2)}");
        Console.WriteLine($"Result 2: {expr2.Evaluate()}");

        PrefixExpressionComparer comparer = new PrefixExpressionComparer();
        int comparisonResult = comparer.Compare(expr1, expr2);
        string comparisonString = comparisonResult == 0 ? "equal to" : comparisonResult > 0 ? "greater than" : "less than";

        Console.WriteLine($"Expression 1 is {comparisonString} Expression 2");

        PrefixExpression clonedExpr = (PrefixExpression)expr1.Clone();
        Console.WriteLine($"Cloned Expression 1: {string.Join(" ", clonedExpr)}");
        Console.WriteLine($"Cloned Result 1: {clonedExpr.Evaluate()}");
    }
}
