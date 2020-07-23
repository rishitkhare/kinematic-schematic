using UnityEngine;

public class BinaryExpression : Expression {

    private Expression operand1;
    private Expression operand2;
    private char expressionOperator;

    //constructor can also take String that gets converted to UnaryExpression instead of Expression

    public BinaryExpression(Expression operand1, Expression operand2, char expressionOperator) {
        SetIsKnown(operand1.GetIsKnown() && operand2.GetIsKnown());
        this.operand1 = operand1;
        this.operand2 = operand2;
        this.expressionOperator = expressionOperator;
    }

    // Assuming that operand2 is a valid number
    public BinaryExpression(Expression operand1, string operand2, char expressionOperator) : this(operand1, new UnaryExpression(operand2), expressionOperator) { }

    // Assuming that operand1 is a valid number
    public BinaryExpression(string operand1, Expression operand2, char expressionOperator) : this(new UnaryExpression(operand1), operand2, expressionOperator) { }


    //toString method for committing the equation to Steps object
    override
    public string ToString() {
        if (operand1.GetIsKnown() && operand2.GetIsKnown()) {
            return Evaluate().ToString();
        }
        else if (operand1.GetIsKnown()) {
            return (operand1.Evaluate().ToString()) + " " + expressionOperator + " " + operand2.ToString();
        }
        else if (operand2.GetIsKnown()) {
            return operand1.ToString() + " " + expressionOperator + " " + (operand2.Evaluate());
        }
        else {
            return ("(" + operand1.ToString() + " " + expressionOperator + " " + operand2.ToString() + ")");
        }
    }

    //*** ACCESSORS ***\\

    public Expression GetOperand1() {
        return operand1;
    }

    public Expression GetOperand2() {
        return operand2;
    }

    public char GetOperator() {
        return expressionOperator;
    }


    // Evaluates by calling the .evaluate() of its branches and combining them with the operator
    override
    public float Evaluate() {
        // Only runs when isKnown
        if (expressionOperator == '^') {
            return Mathf.Pow(operand1.Evaluate(), operand2.Evaluate());
        }
        else if (expressionOperator == '*') {
            return operand1.Evaluate() * operand2.Evaluate();
        }
        else if (expressionOperator == '/') {
            return operand1.Evaluate() / operand2.Evaluate();
        }
        else if (expressionOperator == '+') {
            return operand1.Evaluate() + operand2.Evaluate();
        }
        else if (expressionOperator == '-') {
            return operand1.Evaluate() - operand2.Evaluate();
        }
        else {
            throw new System.ArgumentException("ERROR: Invalid operator" + expressionOperator);
        }
    }
}
