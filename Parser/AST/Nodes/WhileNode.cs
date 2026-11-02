namespace Parser.AST;

public record WhileNode(
    ExprNode Condition,
    List<ExprNode> Body
) : ExprNode;