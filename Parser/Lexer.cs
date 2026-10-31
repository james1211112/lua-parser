using System;

namespace Parser;

public class Lexer
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
                i += 2; // skip
                while (i < input.Length && input[i] != '\n')
                {
                    i++;
                }
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
                int start = i;
                while (i < input.Length && (char.IsLetterOrDigit(input[i]) || input[i] == '_')) // fixed it breaking on variable names with numbers in them
                {
                    i++;

                }
                string value = input[start..i];
                if (value == "local")
                {
                    tokens.Add(new Token(TokenType.Keyword, value));
                    continue;
                }
                if (value == "true" || value == "false")
                {
                    tokens.Add(new Token(TokenType.Boolean, value));
                    continue;
                }
                if (value == "nil")
                {
                    tokens.Add(new Token(TokenType.Nil, value));
                    continue;
                }
                // if (value == "--")
                // {
                //     i++;
                //     continue;
                // }
                // if (c == ';')
                // {
                //     tokens.Add(new Token(TokenType.Semicolon, ";"));
                //     i++;
                //     continue;
                // }
                tokens.Add(new Token(TokenType.Identifier, value));
                continue;
            }

            if (char.IsDigit(c))
            {
                int start = i;
                while (i < input.Length && (char.IsDigit(input[i]) || input[i] == '.')) // decimal support
                {
                    i++;
                }
                string value = input[start..i];
                tokens.Add(new Token(TokenType.Number, value));
                continue;
            }
            if (c == '"')
            {
                i++;
                int start = i;
                while (i < input.Length && input[i] != '"')
                {
                    i++;
                }
                if (i >= input.Length)
                {
                    throw new Exception("unterminated string");
                }
                string value = input[start..i];
                tokens.Add(new Token(TokenType.String, value));
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
                tokens.Add(new Token(TokenType.Comma, ","));
                i++;
                continue;
            }


        }
        return tokens;
    }
}
