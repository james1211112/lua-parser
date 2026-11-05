namespace Parser.AST;
public record RepeatNode(List<ExprNode> Body, ExprNode Condition) : ExprNode;