using Parser.AST;
using Xunit;
using Lexer = global::Parser.Lexer;
using LuaParser = global::Parser.Parser;

public class ParserTests
{
    private static List<ExprNode> Parse(string input)
    {
        var tokens = new Lexer().Tokenize(input);
        return new LuaParser().Parse(tokens);
    }

    [Fact]
    public void LocalDeclaration()
    {
        var ast = Parse("local a = 10");

        var local = Assert.IsType<LocalDeclaration>(Assert.Single(ast));
        var value = Assert.IsType<ConstantNode>(local.Value);

        Assert.Equal("a", local.Name.Name);
        Assert.Equal(10, value.Value);
        Assert.Equal(Literal.Number, value.Kind);
    }

    [Fact]
    public void StringLiteral()
    {
        var ast = Parse("local a = \"hello world\"");

        var local = Assert.IsType<LocalDeclaration>(Assert.Single(ast));
        var value = Assert.IsType<ConstantNode>(local.Value);

        Assert.Equal("hello world", value.Value);
        Assert.Equal(Literal.String, value.Kind);
    }

    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    public void BooleanLiteral(string input, bool expected)
    {
        var ast = Parse($"local a = {input}");

        var local = Assert.IsType<LocalDeclaration>(Assert.Single(ast));
        var value = Assert.IsType<ConstantNode>(local.Value);

        Assert.Equal(expected, value.Value);
        Assert.Equal(Literal.Boolean, value.Kind);
    }

    [Fact]
    public void VariableReference()
    {
        var ast = Parse("local b = a");

        var local = Assert.IsType<LocalDeclaration>(Assert.Single(ast));
        var value = Assert.IsType<IdentifierNode>(local.Value);

        Assert.Equal("b", local.Name.Name);
        Assert.Equal("a", value.Name);
    }

    [Fact]
    public void MultipleDeclarations()
    {
        var ast = Parse("local a = 10\nlocal b = a");

        Assert.Equal(2, ast.Count);

        var first = Assert.IsType<LocalDeclaration>(ast[0]);
        var second = Assert.IsType<LocalDeclaration>(ast[1]);

        Assert.Equal("a", first.Name.Name);
        Assert.Equal("b", second.Name.Name);
        Assert.IsType<IdentifierNode>(second.Value);
    }

    [Fact]
    public void InvalidSyntax()
    {
        Assert.Throws<Exception>(() => Parse("local = 10"));
    }

    [Fact]
    public void IncompleteDeclaration()
    {
        Assert.Throws<Exception>(() => Parse("local a ="));
    }
}