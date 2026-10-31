using Parser.AST;
using System;
namespace Parser;

using Token = global::Parser.Lexer.Token;
public partial class Parser
{
    private ExprNode ParseStatement() // TODO: global variable support
    {
        Token token = Peek()!; // using new helper now
        if (token.Type == Lexer.TokenType.Keyword)
        {
            if (token.Value == "local")
            {
                return ParseVariable();
            }
        }
        if (token.Type == Lexer.TokenType.Identifier)
        {
            return ParseAssignment();
        }

        throw new Exception($"unexpected token: {token.Value}");
    }

    private ExprNode ParseVariable()
    {
        Advance();

        if (pos >= tokens.Count)
            throw new Exception("Incomplete variable declaration");

        List<IdentifierNode> names = ParseList(ParseIdentifier);

        if (pos >= tokens.Count)
            throw new Exception("Incomplete variable declaration");

        Expect(Lexer.TokenType.Equals, "Expected '='");

        if (pos >= tokens.Count)
            throw new Exception("Incomplete variable declaration");

        List<ExprNode> values = ParseList(ParseExpression);

        return new LocalDeclaration(names, values);
    }
    private ExprNode ParseAssignment()
    {
        Token name = Expect(Lexer.TokenType.Identifier, "Expected identifier");

        if (Match(Lexer.TokenType.Equals))
        {
            ExprNode value = ParseExpression();

            return new AssignmentNode(
                new IdentifierNode(name.Value),
                value
            );
        }

        if (Peek()?.Type == Lexer.TokenType.Operator &&
            Peek()!.Value is "+=" or "-=" or "*=" or "/=")
        {
            Token op = Advance();
            ExprNode value = ParseExpression();

            return new CompoundAssignmentNode(
                new IdentifierNode(name.Value),
                op.Value,
                value
            );
        }

        throw new Exception("Expected '='");
    }
}