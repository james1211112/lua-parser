namespace Parser.AST;

public record LocalDeclaration(
    IdentifierNode Name,
    ExprNode? Value
) : ExprNode;