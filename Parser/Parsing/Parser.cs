using Parser.AST;
using System;
namespace Parser;

using Token = global::Parser.Lexer.Token;
public partial class Parser
{
    private List<Token> tokens = new();
    private int pos = 0;

    public List<ExprNode> Parse(List<Token> input)
    {
        var nodes = new List<ExprNode>();
        pos = 0;
        tokens = input;
        while (pos < tokens.Count)
        {
            nodes.Add(ParseStatement());
        }

        return nodes;
    }
}
