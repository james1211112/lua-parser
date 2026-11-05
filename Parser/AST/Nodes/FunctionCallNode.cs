namespace Parser.AST;

public record FunctionCallNode(
    ExprNode Function,
    List<ExprNode> Arguments
) : ExprNode;