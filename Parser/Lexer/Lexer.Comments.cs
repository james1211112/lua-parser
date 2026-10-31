namespace Parser;

public partial class Lexer
{
    private void Comment(string input, ref int i)
    {
        i += 2; // skip
        while (i < input.Length && input[i] != '\n')
        {
            i++;
        }
    }
}
