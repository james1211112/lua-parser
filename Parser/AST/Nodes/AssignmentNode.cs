namespace Parser.AST;

public record AssignmentNode( // global assignment / variable reassignment
    IdentifierNode Name,
    ExprNode Value
) : ExprNode;