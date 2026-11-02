using Parser.AST;
using System;
namespace Parser;

using Token = global::Parser.Lexer.Token;
public partial class Parser
{
    private ExprNode ParseIf()
    {
        ExprNode Condition = ParseExpression();
        ExpectKeyword("then");
        List<ExprNode> Body = new();
        while (Peek() is not null && Peek()!.Value is not ("elseif" or "else" or "end"))
        {
            Body.Add(ParseStatement());
        }
        List<ElseIfNode> elseifs = new();

        while (Peek()?.Value == "elseif")
        {
            Advance();

            ExprNode elseifCond = ParseExpression();
            ExpectKeyword("then");

            List<ExprNode> elseifBody = new();

            while (Peek() is not null &&
                   Peek()!.Value is not ("elseif" or "else" or "end"))
            {
                elseifBody.Add(ParseStatement());
            }

            elseifs.Add(new ElseIfNode(elseifCond, elseifBody));
        }

        // outside the elseif loop
        ElseNode? elseClause = null;

        if (Peek()?.Value == "else")
        {
            Advance();

            List<ExprNode> elseBody = new();

            while (Peek() is not null && Peek()!.Value != "end")
            {
                elseBody.Add(ParseStatement());
            }

            elseClause = new ElseNode(elseBody);
        }

        ExpectKeyword("end");

        return new IfNode(
            Condition,
            Body,
            elseifs,
            elseClause
        );

    }
    private ExprNode ParseWhile()
    {
        ExprNode Condition = ParseExpression(); // 🤤
        ExpectKeyword("do");
        List<ExprNode> Body = new();
        while (Peek() is not null && Peek()!.Value != "end")
        {
            Body.Add(ParseStatement());
        }
        ExpectKeyword("end");
        return new WhileNode(Condition, Body);
    }

}