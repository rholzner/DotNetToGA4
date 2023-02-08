namespace DotNetToGA4.Domain.Models.System;

public class Share : Core
{
    public Share(string sharedAs, string name, string id)
    {
        SharedAs = sharedAs;
        Name = name;
        Id = id;
    }

    public string SharedAs { get; }
    public string Name { get; }
    public string Id { get; }
}
