namespace Parser.AST;

public record BinaryExpressionNode(
    ExprNode Left,
    string Operator,
    ExprNode Right
) : ExprNode;