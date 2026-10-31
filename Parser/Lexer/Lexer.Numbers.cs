namespace Parser;

public partial class Lexer
{
    private Token Number(string input, ref int i)
    {
        int start = i;
        while (i < input.Length && (char.IsDigit(input[i]) || input[i] == '.')) // decimal support
        {
            i++;
        }
        string value = input[start..i];
        return new Token(TokenType.Number, value);
    }
}
