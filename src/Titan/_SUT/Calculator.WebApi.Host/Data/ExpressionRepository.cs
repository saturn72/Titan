using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Calculator.WebApi.Host.Domain;

namespace Calculator.WebApi.Host.Data
{
    public static class ExpressionService
    {
        private static readonly ICollection<Expression> ExpressionList = new List<Expression>();

        private static long _idIndex;
        public static IEnumerable<Expression> All => ExpressionList;

        public static Expression Add(int x, int y)
        {
            return AddToDatabase(new Expression
            {
                Id = ++_idIndex,
                X = x,
                Y = y,
                Operator = Operator.Add,
                Result = x + y
            });
        }

        public static Expression GetById(long id)
        {
            return ExpressionList.FirstOrDefault(e => e.Id == id);
        }

        public static Expression Subtract(int x, int y)
        {
            return AddToDatabase(new Expression
            {
                X = x,
                Y = y,
                Operator = Operator.Subtract,
                Result = x - y
            });
        }


        public static Expression Multiply(int x, int y)
        {
            return AddToDatabase(new Expression
            {
                X = x,
                Y = y,
                Operator = Operator.Multuply,
                Result = x * y
            });
        }

        private static Expression AddToDatabase(Expression expression)
        {
            ExpressionList.Add(expression);
            Log(expression);
            return expression;
        }

        private static void Log(Expression expression)
        {
            const string logMsgFormat = "{0}: {1}{2}{3}={4}";
            var msg = string.Format(logMsgFormat, DateTime.UtcNow, expression.X, GetExpressionChar(expression.Operator), expression.Y, expression.Result);
            Trace.WriteLine(msg);
        }

        private static char GetExpressionChar(Operator op)
        {
            switch (op)
            {
                case Operator.Add:
                    return '+';
                case Operator.Subtract:
                    return '-';
                case Operator.Multuply:
                    return '*';
                case Operator.Divide:
                    return '/';
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
            }
        }

        public static Expression Divide(int x, int y)
        {
            return AddToDatabase(new Expression
                {
                    X = x,
                    Y = y,
                    Operator = Operator.Divide,
                    Result = x / y
                });
        }
    }
}