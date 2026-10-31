namespace Parser.AST;

public record CompoundAssignmentNode(
    IdentifierNode Name,
    string Operator,
    ExprNode Value
) : ExprNode;