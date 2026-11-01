using Parser.AST;

using TokenType = global::Parser.Lexer.TokenType;
namespace Parser;

public partial class Parser
{
    private ExprNode ParseFunction(bool isLocal)
    {
        IdentifierNode name = ParseIdentifier(); // function name

        Expect(TokenType.Lparen);

        List<IdentifierNode> parameters = new();
        if (Peek()?.Type != TokenType.Rparen)
        {
            parameters = ParseList(ParseIdentifier);
        }
        Expect(TokenType.Rparen); // closing paren
        List<ExprNode> body = new(); // function body

        while (Peek() is not null && Peek()!.Value != "end")
        {
            body.Add(ParseStatement());
        }

        if (Peek()?.Value != "end")
            throw new Exception("Expected 'end' after function");

        Advance();
        return new FunctionDeclarationNode(name, parameters, body, isLocal);
    }
}