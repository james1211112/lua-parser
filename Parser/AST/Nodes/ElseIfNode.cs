namespace Parser.AST;

public record ElseIfNode(
    ExprNode Condition,
    List<ExprNode> Body
);