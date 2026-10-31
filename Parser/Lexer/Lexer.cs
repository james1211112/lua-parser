using System;

namespace Parser;

public partial class Lexer
{
    public enum TokenType
    {
        Keyword,
        Identifier,
        Equals,
        Number,
        String,
        Boolean,
        Nil,
        Comma,
        // Semicolon
    }

    public record Token(TokenType Type, string Value);

    public List<Token> Tokenize(string input)
    {
        var tokens = new List<Token>();

        int i = 0;
        while (i < input.Length)
        {
            char c = input[i];

            if (char.IsWhiteSpace(c))
            {
                i++; // skip whitespace
                continue;
            }
            if (c == '-' && i + 1 < input.Length && input[i + 1] == '-') // TODO: make this more lossless
            {
                Comment(input, ref i);
                continue;
            }

            if (c == '=')
            {
                tokens.Add(new Token(TokenType.Equals, "="));
                i++;
                continue;
            }
            if (char.IsLetter(c) || c == '_')                 // supported variables starting with _
            {
                tokens.Add(Identifier(input, ref i));
                continue;
            }

            if (char.IsDigit(c))
            {
                tokens.Add(Number(input, ref i));
                continue;
            }
            if (c == '"')
            {
                tokens.Add(String(input, ref i));
                continue;
            }

            if (c == ';')
            {
                i++;
                continue;
            }

            if (c == ',')
            {
                tokens.Add(new Token(TokenType.Comma, ",")); // still no support for multiple local decl on same line
                i++;
                continue;
            }
        }
        return tokens;
    }
}
