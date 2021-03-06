﻿using UnityEngine;
using System;

//TODO: needs the Equals() method!

public class Algebra {

   public static readonly string[] standardUnits = { "m/s", "m/s", "s", "m/s^2", "m" };

   //prevents instantiation
   private Algebra() { }

    public static bool IsEqualEquation(Expression leftSide, Expression rightSide) {
        

        return Mathf.Abs(leftSide.Evaluate() - rightSide.Evaluate()) / leftSide.Evaluate() < 0.1;
    }

    public static bool IsEqualFloat(float leftSide, float rightSide) {
        return Mathf.Abs(leftSide - rightSide) / leftSide < 0.1;
    }

    // checks if a String can be parsed as Double
    public static bool IsNumber(string component) {
        try {
            float num = float.Parse(component);
            return true;
        }
        catch {
            return false;
        }
    }

    // given a left and right side of an equation, a Steps object, and if the unknown is time (for special cases)
    public static void solveEquation(bool isTime, Steps showYourWork, Expression l, Expression r, int unknownQuantityIndex) {

        //write down current step of equation
        showYourWork.AddStep(l.ToString() + " = " + r.ToString());

        //if the unknown is on the right side, then flip them
        Expression rightSide = null;
        Expression leftSide = null;

        if (l.GetIsKnown()) {
            // leftSide is known, rightSide is NOT known
            leftSide = r;
            rightSide = l;
        }
        else if (r.GetIsKnown()) {
            // leftSide is unknown, right is (leave the equation as is)
            leftSide = l;
            rightSide = r;
        }
        else {
            //if there are variables on both sides, then something went wrong
            throw new System.ArgumentException("ERROR: Both sides of equation contain an unknown value");
        }

        //add step to work
        showYourWork.AddStep(leftSide + " = " + rightSide.Evaluate());

        //essentially does algebra until the variable is isolated on one side
        while (leftSide is BinaryExpression) {

            BinaryExpression binaryLeftSide = (BinaryExpression) leftSide;
            char expressionOperator = binaryLeftSide.GetOperator();
            bool isOperand1Known;
            Expression knownExpression;

            // change the left side by taking the unknown branch and isolating it
            if (binaryLeftSide.GetOperand1().GetIsKnown()) {
                knownExpression = binaryLeftSide.GetOperand1();
                isOperand1Known = true;

                leftSide = ((BinaryExpression)leftSide).GetOperand2();
            }
            else if (binaryLeftSide.GetOperand2().GetIsKnown()) {
                knownExpression = binaryLeftSide.GetOperand2();
                isOperand1Known = false;

                leftSide = ((BinaryExpression)leftSide).GetOperand1();

            }
            else {
                throw new System.ArgumentException("ERROR: Neither expression in the leftside contains a known value");
            }

            // Change right side in accordance with what was removed from the left side
            if (expressionOperator == '+') {
                rightSide = new BinaryExpression(rightSide, knownExpression, '-');
            } else if (expressionOperator == '-') {
                if (isOperand1Known) {
                    rightSide = new BinaryExpression(knownExpression, rightSide, '-');
                }
                else {
                    rightSide = new BinaryExpression(rightSide, knownExpression, '+');
                }
            } else if (expressionOperator == '*') {
                rightSide = new BinaryExpression(rightSide, knownExpression, '/');
            } else if (expressionOperator == '/') {
                if (isOperand1Known) {
                    rightSide = new BinaryExpression(knownExpression, rightSide, '/');
                }
                else {
                    rightSide = new BinaryExpression(rightSide, knownExpression, '*');
                }
            } else if (expressionOperator == '^' && !isOperand1Known) {
                float exponent = Mathf.Pow(knownExpression.Evaluate(), -1);
                UnaryExpression exponentExpression = new UnaryExpression(exponent.ToString());
                rightSide = new BinaryExpression(rightSide, exponentExpression, '^');
            } else {
                throw new System.ArgumentException("ERROR: Invalid operator " + expressionOperator);
            }

            showYourWork.AddStep(leftSide + " = " + rightSide.Evaluate());
            showYourWork.SetAnswer(rightSide.Evaluate());
        }

        //If we are solving for time, the answer cannot be negative

        if (rightSide.Evaluate() < 0 && isTime) {
            throw new System.ArgumentException("ERROR: Time cannot be negative");
        }
        showYourWork.ReplaceLastValue(leftSide + " = " + rightSide.Evaluate() + " " + standardUnits[unknownQuantityIndex]);
    }


    //Quadratic equation for special case (AIR) (Also cannot return negative because isTime is guaranteed)
    public static void getPositiveQuadraticRoot(Steps showYourWork, float a, float b, float c) {
        showYourWork.AddStep("Δt = (-b ± √(b^2 - 4ac) / 2a");
        showYourWork.AddStep("Δt = (" + (-1 * b) + " ± √(" + Mathf.Pow(b, 2) + " - " + (4 * a * c) + ") / " + (2 * a));
        double[] roots = new double[2];
        roots[0] = ((b * -1) + Mathf.Sqrt(Mathf.Pow(b, 2) - 4 * a * c)) / (2 * a);
        roots[1] = ((b * -1) - Mathf.Sqrt(Mathf.Pow(b, 2) - 4 * a * c)) / (2 * a);
        Array.Sort(roots);
        if (roots[1] > 0) {
            showYourWork.AddStep("Δt = " + roots[1]);
        }
        else {
            throw new System.ArgumentException("ERROR: Both values for possible time are negative");
        }
    }

    
    public static Steps[] ShadowSpecialCase(string[] quantities) {
        Steps column1;
        Steps column2;

       // Debug.Log(string.Format("{0}, {1}, {2}, {3}, {4}", quantities[0], quantities[1], quantities[2], quantities[3], quantities[4]));

        bool[] knowns = { false, true, false, true, true }; //KILL ME

        //COLUMN 1
        Fire zuko = new Fire(knowns, quantities);
        zuko.DoAlgebra();
        //should've solved for Vi
        column1 = zuko.GetWork();

        //COLUMN 2
        string[] newQuantities = { "-" + quantities[0], quantities[1], "Δt", quantities[3], quantities[4] };
        bool[] newKnowns = { true, true, false, true, true };
        column2 = zuko.GetWork();

        //Debug.Log(string.Format("{0}, {1}, {2}, {3}, {4}", quantities[0], quantities[1], quantities[2], quantities[3], quantities[4]));

        Water katara = new Water(knowns, quantities);
        katara.DoAlgebra();
        column1.AppendMoreSteps(katara.GetWork());

        //Debug.Log(string.Format("{0}, {1}, {2}, {3}, {4}", quantities[0], quantities[1], quantities[2], quantities[3], quantities[4]));

        //adds '-' to 'zuko's work
        string newLastValue = zuko.GetWork().GetLastStep();
        newLastValue.Insert(newLastValue.IndexOf("=") + 1, "-");
        zuko.GetWork().ReplaceLastValue(newLastValue);


        Water korra = new Water(newKnowns, newQuantities);
        korra.DoAlgebra();
        column2.AppendMoreSteps(korra.GetWork());

        return new Steps[] { column1, column2 };
    }
}
