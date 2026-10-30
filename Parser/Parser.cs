using Parser.AST;
using System;
namespace Parser;

using Token = global::Parser.Lexer.Token;
public class Parser 
{
    private List<Token> tokens = new();
    private int pos = 0;
    private ExprNode ParseStatement() // TODO: global variable support
    {
        Token token = tokens[pos];
        if (token.Type == Lexer.TokenType.Keyword)
        {
            if (token.Value == "local")
            {
                return ParseVariable(tokens);
            }
        }
        throw new Exception($"unexpected token: {token.Value}");
    }
    private ConstantNode ParseConstant(Token token)
    {
        return token.Type switch
        {
            Lexer.TokenType.Number =>
                new ConstantNode(
                    int.Parse(token.Value),
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

            _ => throw new Exception("expected a constant")
        };
    }
    /*
    TODO:
    - add support for multiple variable declarations on one line
    - support local declarations with no supplied value e.g: `local a`
    - support global variables
    */
    private ExprNode ParseVariable(List<Token> tokens) 
    {
        pos++;
        if (pos + 2 >= tokens.Count) 
            throw new Exception("Incomplete variable declaration");

        if (tokens[pos].Type != Lexer.TokenType.Identifier)
            throw new Exception("Expected identifier");

        Token name = tokens[pos++];

        if (tokens[pos].Type != Lexer.TokenType.Equals)
            throw new Exception("Expected '='");

        pos++;

        Token valueToken = tokens[pos++];

        ExprNode value;

        if (valueToken.Type == Lexer.TokenType.Identifier) // variable ref
            value = new IdentifierNode(valueToken.Value); // var name
        else
            value = ParseConstant(valueToken);

        return new LocalDeclaration(
            new IdentifierNode(name.Value),
            value
        );
    }
    public List<ExprNode> Parse(List<Token> input)
    {
        var nodes = new List<ExprNode>();
        pos = 0;
        tokens = input;
        while (pos < tokens.Count)
        {
            nodes.Add(ParseStatement());
        }

        return nodes;
    }
}