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
        Operator,
        Rparen,
        Lparen,
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
                if (i + 1 < input.Length && input[i + 1] == '=')
                {
                    tokens.Add(new Token(TokenType.Operator, "=="));
                    i += 2;
                    continue;
                }
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
            if (c == '"' || c == '\'')
            {
                tokens.Add(String(input, ref i));
                continue;
            }
            if (c == '(')
            {
                tokens.Add(new Token(TokenType.Lparen, "("));
                i++;
                continue;
            }
            if (c == ')')
            {
                tokens.Add(new Token(TokenType.Rparen, ")"));
                i++;
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
            if (c == '>')
            {
                if (i + 1 < input.Length && input[i + 1] == '=')  // check if it's >= instead of >
                {
                    tokens.Add(new Token(TokenType.Operator, ">="));
                    i += 2;
                    continue;
                }
                tokens.Add(new Token(TokenType.Operator, ">"));
                i++;
                continue;
            }
            if (c == '<')
            {
                if (i + 1 < input.Length && input[i + 1] == '=')
                {
                    tokens.Add(new Token(TokenType.Operator, "<="));
                    i += 2;
                    continue;
                }
                tokens.Add(new Token(TokenType.Operator, "<"));
                i++;
                continue;
            }

            if (c is '+' or '-' or '*' or '/') 
            {
                Operators(input, ref i, tokens);
                continue;
            }
            throw new Exception($"unexpected character: {c}");
        }
        return tokens;
    }
}
