namespace Parser;

public partial class Lexer
{
    private void Operators(string input, ref int i, List<Token> tokens)
{
    string op = input[i++].ToString();

    if (i < input.Length && input[i] == '=') //compound
        op += input[i++];

    tokens.Add(new Token(TokenType.Operator, op));
}
}