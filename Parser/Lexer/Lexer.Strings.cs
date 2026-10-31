namespace Parser;

public partial class Lexer
{
    private Token String(string input, ref int i)
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
        i++;
        return new Token(TokenType.String, value);
    }
}
