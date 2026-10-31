using System.Globalization;
namespace Parser.AST;

public static class Extensions
{
    public static string ToFullString(this ExprNode node) // ast -> lua :)
    {
        return node switch
        {
            LocalDeclaration l =>$"local {string.Join(", ", l.Names.Select(n => n.ToFullString()))}" + (l.Values.Count > 0 ? $" = {string.Join(", ", l.Values.Select(v => v.ToFullString()))}" : ""),  
            IdentifierNode id => id.Name,
            ConstantNode c => c.Kind switch
            {
                Literal.String => $"\"{(string)c.Value}\"", // TODO: add proper string escaping 
                Literal.Boolean => (bool)c.Value ? "true" : "false",
                Literal.Number =>
                    Convert.ToString(c.Value, CultureInfo.InvariantCulture)
                    ?? throw new Exception("invalid number"),
                Literal.Nil => "nil",
                _ => throw new Exception("unknown literal")
            },

            BinaryExpressionNode b => $"{b.Left.ToFullString()} {b.Operator} {b.Right.ToFullString()}",
            AssignmentNode a => $"{a.Name.ToFullString()} = {a.Value.ToFullString()}",
            CompoundAssignmentNode c => $"{c.Name.ToFullString()} {c.Operator} {c.Value.ToFullString()}", // compound operators!!!!!
            _ => throw new Exception("unknown node type")
        };
    }
}