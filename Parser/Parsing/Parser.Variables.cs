using Parser.AST;
using System;
namespace Parser;

using Token = global::Parser.Lexer.Token;
public partial class Parser
{
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
}
