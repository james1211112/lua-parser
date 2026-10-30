namespace Parser.AST;

public enum Literal
{
    Number,
    String,
    Boolean
}
public record ConstantNode(object Value, Literal Kind) : ExprNode;