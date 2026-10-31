using Parser.AST;
using System;
using System.Globalization;
namespace Parser;

using Token = global::Parser.Lexer.Token;
public partial class Parser
{
    private object ParseNumber(string value)
    {
        if (value.Contains('.') || value.Contains('e', StringComparison.OrdinalIgnoreCase))
            return double.Parse(value, CultureInfo.InvariantCulture);

        return long.Parse(value, CultureInfo.InvariantCulture);
    }

    private ExprNode ParseValue()
    {
        Token token = Advance();

        if (token.Type == Lexer.TokenType.Identifier)
            return new IdentifierNode(token.Value);

        return ParseConstant(token);
    }

      private ExprNode ParseExpression()
    {
        ExprNode left = ParseValue();

        while (Peek()?.Type == Lexer.TokenType.Operator) // support more than one operator
        {
            Token op = Advance();
            ExprNode right = ParseValue();
            left = new BinaryExpressionNode(left, op.Value, right);
        }

        return left;
    }
    

    private IdentifierNode ParseIdentifier() // parse Identifier
    {
        Token token = Expect(Lexer.TokenType.Identifier, "Expected identifier");

        return new IdentifierNode(token.Value);
    }

    private ConstantNode ParseConstant(Token token)
    {
        return token.Type switch
        {
            Lexer.TokenType.Number =>
                new ConstantNode(
                    ParseNumber(token.Value), // was using long.parse and int.parse, but that doesnt support decimals, so we gonna use this method instead :)
                    Literal.Number
                ),

            Lexer.TokenType.String =>
                new ConstantNode(
                    token.Value,
                    Literal.String
                ),

            Lexer.TokenType.Boolean =>
                new ConstantNode(
                    bool.Parse(token.Value),
                    Literal.Boolean
                ),
            Lexer.TokenType.Nil =>
                new ConstantNode(
                    null,
                    Literal.Nil
                ),

            _ => throw new Exception("expected a constant")
        };
    }
}
