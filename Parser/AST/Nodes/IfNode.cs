namespace Parser.AST;

public record IfNode(
    ExprNode Condition,
    List<ExprNode> Body,
    List<ElseIfNode> ElseIfClauses,
    ElseNode? ElseClause
) : ExprNode;