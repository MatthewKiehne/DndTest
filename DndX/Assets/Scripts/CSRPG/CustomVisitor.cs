using Antlr4.Runtime.Misc;
using Calculator;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomVisitor : CalculatorBaseVisitor<double> {

    public override double VisitAdditionOrSubtraction([NotNull] CalculatorParser.AdditionOrSubtractionContext context) {
        double left = Visit(context.left);
        double right = Visit(context.right);

        if (context.@operator.Type == CalculatorLexer.SUB) {
            return left - right;
        }

        return left + right;
    }

    public override double VisitMultiplicationOrDivision([NotNull] CalculatorParser.MultiplicationOrDivisionContext context) {
        double left = Visit(context.left);
        double right = Visit(context.right);

        if (context.@operator.Type == CalculatorLexer.MUL) {
            return left * right;
        }

        return left / right;
    }

    public override double VisitNumber([NotNull] CalculatorParser.NumberContext context) {
        return Double.Parse(context.GetText());
    }

    public override double VisitParentheses([NotNull] CalculatorParser.ParenthesesContext context) {
        return Visit(context.inner);
    }

    public override double VisitPower([NotNull] CalculatorParser.PowerContext context) {
        double left = Visit(context.left);
        double right = Visit(context.right);
        return Math.Pow(left, right);
    }
}
