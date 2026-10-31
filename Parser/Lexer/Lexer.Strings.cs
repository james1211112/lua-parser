namespace Parser;

public partial class Lexer
{
    private Token String(string input, ref int i)
    {
        char quote = input[i++]; // support both single and double quotes
        int start = i;

        while (i < input.Length && input[i] != quote)
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
