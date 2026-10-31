namespace Parser.AST;

public enum Literal
{
    Number,
    String,
    Boolean,
    Nil
}
public record ConstantNode(object? Value, Literal Kind) : ExprNode;