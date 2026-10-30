using System.Globalization;
namespace Parser.AST;

public static class Extensions
{
    public static string ToFullString(this ExprNode node) // ast -> lua :)
    {
        return node switch
        {
            LocalDeclaration local => $"local {local.Name.ToFullString()} = {local.Value.ToFullString()}", 
            IdentifierNode id => id.Name,
            ConstantNode c => c.Kind switch
            {
                Literal.String => $"\"{(string)c.Value}\"", // TODO: add proper string escaping 
                Literal.Boolean => (bool)c.Value ? "true" : "false",
                Literal.Number =>
                    Convert.ToString(c.Value, CultureInfo.InvariantCulture)
                    ?? throw new Exception("invalid number"),

                _ => throw new Exception("unknown literal")
            },

            _ => throw new Exception("unknown node type")
        };
    }
}