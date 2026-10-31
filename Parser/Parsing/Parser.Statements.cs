using Parser.AST;
using System;
namespace Parser;

using Token = global::Parser.Lexer.Token;
public partial class Parser
{
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
}
