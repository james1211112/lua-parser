namespace Parser.AST;

public record ElseNode(
    List<ExprNode> Body
);