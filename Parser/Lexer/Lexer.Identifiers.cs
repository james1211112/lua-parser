namespace Parser;

public partial class Lexer
{
    private Token Identifier(string input, ref int i)
    {
        int start = i;
        while (i < input.Length && (char.IsLetterOrDigit(input[i]) || input[i] == '_')) // fixed it breaking on variable names with numbers in them
        {
            i++;

        }
        string value = input[start..i];
        if (value == "local")
        {
            return new Token(TokenType.Keyword, value);
        }
        if (value == "true" || value == "false")
        {
            return new Token(TokenType.Boolean, value);
        }
        if (value == "nil")
        {
            return new Token(TokenType.Nil, value);
        }
        return new Token(TokenType.Identifier, value);
    }
}
