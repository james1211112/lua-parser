namespace Parser.AST;

public record LocalDeclaration(
    List<IdentifierNode> Names,
    List<ExprNode> Values
) : ExprNode;