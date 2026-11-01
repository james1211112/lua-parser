namespace Parser;

public partial class Lexer
{

    private static readonly HashSet<string> Keywords = new() // thank you claude 
{
    "and",
    "break",
    "do",
    "else",
    "elseif",
    "end",
    "false",
    "for",
    "function",
    "if",
    "in",
    "local",
    "nil",
    "not",
    "or",
    "repeat",
    "return",
    "then",
    "true",
    "until",
    "while"
};
    private Token Identifier(string input, ref int i)
    {
        int start = i;
        while (i < input.Length && (char.IsLetterOrDigit(input[i]) || input[i] == '_')) // fixed it breaking on variable names with numbers in them
        {
            i++;

        }
        string value = input[start..i];

        if (value == "true" || value == "false")
        {
            return new Token(TokenType.Boolean, value);
        }
        if (value == "nil")
        {
            return new Token(TokenType.Nil, value);
        }
        if (Keywords.Contains(value)) // has to BE after it checks for true, false, and nil
        {
            return new Token(TokenType.Keyword, value);
        }
        return new Token(TokenType.Identifier, value);
    }
}
