namespace Parser;

using Token = global::Parser.Lexer.Token;

public partial class Parser // this file will make my life so much easier
{
    private Token? Peek()
    {
        if (pos >= tokens.Count)
            return null;

        return tokens[pos];
    }

    private Token Advance()
    {
        if (pos >= tokens.Count)
            throw new Exception("unexpected end of input");

        return tokens[pos++];
    }

    private bool Match(Lexer.TokenType type)
    {
        if (Peek()?.Type != type)
            return false;

        Advance();
        return true;
    }

    private Token Expect(Lexer.TokenType type, string? message = null)
    {
        if (Peek()?.Type != type)
            throw new Exception(message ?? $"expected {type}");

        return Advance();
    }

    private List<T> ParseList<T>(Func<T> parseItem)
    {
        var items = new List<T> { parseItem() };

        while (Match(Lexer.TokenType.Comma)) // support multiple local declarations on one line
            items.Add(parseItem());

        return items;
    }

}