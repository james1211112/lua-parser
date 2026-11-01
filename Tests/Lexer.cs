using Parser;
using Xunit;

public class LexerTests
{
    [Fact]
    public void LocalDeclaration()
    {
        var tokens = new Lexer().Tokenize("local a = 10");

        Assert.Equal(4, tokens.Count);
        Assert.Equal(Lexer.TokenType.Keyword, tokens[0].Type);
        Assert.Equal("local", tokens[0].Value);
        Assert.Equal(Lexer.TokenType.Identifier, tokens[1].Type);
        Assert.Equal(Lexer.TokenType.Equals, tokens[2].Type);
        Assert.Equal(Lexer.TokenType.Number, tokens[3].Type);
        Assert.Equal("10", tokens[3].Value);
    }

    [Theory]
    [InlineData("123", Lexer.TokenType.Number, "123")]
    [InlineData("true", Lexer.TokenType.Boolean, "true")]
    [InlineData("false", Lexer.TokenType.Boolean, "false")]
    [InlineData("\"hello world\"", Lexer.TokenType.String, "hello world")]
    public void Literals(
        string input,
        Lexer.TokenType expectedType,
        string expectedValue)
    {
        var tokens = new Lexer().Tokenize(input);
        var token = Assert.Single(tokens);

        Assert.Equal(expectedType, token.Type);
        Assert.Equal(expectedValue, token.Value);
    }

    [Fact]
    public void VariableReference()
    {
        var tokens = new Lexer().Tokenize("local b = a");

        Assert.Equal(4, tokens.Count);
        Assert.Equal(Lexer.TokenType.Identifier, tokens[3].Type);
        Assert.Equal("a", tokens[3].Value);
    }

    [Theory]
    [InlineData("function add(a, b) end", false)]
    [InlineData("local function add(a, b) end", true)]
    public void FunctionDeclaration(string input, bool isLocal)
    {
        var tokens = new Lexer().Tokenize(input);

        int i = 0;

        if (isLocal)
        {
            Assert.Equal("local", tokens[i].Value);
            Assert.Equal(Lexer.TokenType.Keyword, tokens[i].Type);
            i++;
        }

        Assert.Equal("function", tokens[i++].Value);
        Assert.Equal("add", tokens[i++].Value);

        Assert.Equal(Lexer.TokenType.Lparen, tokens[i++].Type);
        Assert.Equal("a", tokens[i++].Value);
        Assert.Equal(Lexer.TokenType.Comma, tokens[i++].Type);
        Assert.Equal("b", tokens[i++].Value);
        Assert.Equal(Lexer.TokenType.Rparen, tokens[i++].Type);

        Assert.Equal("end", tokens[i++].Value);
        Assert.Equal(i, tokens.Count);
    }

    [Fact]
    public void MultipleDeclarations()
    {
        var tokens = new Lexer().Tokenize(
            "local a = 10\nlocal b = true"
        );

        Assert.Equal(8, tokens.Count);
    }



    [Fact]
    public void Comma()
    {
        var tokens = new Lexer().Tokenize("local a, b");

        Assert.Equal(Lexer.TokenType.Comma, tokens[2].Type);
    }

    [Fact]
    public void Comments()
    {
        var tokens = new Lexer().Tokenize(
            "-- hello\nlocal a = 10"
        );

        Assert.Equal(4, tokens.Count);
        Assert.Equal("local", tokens[0].Value);
    }
}