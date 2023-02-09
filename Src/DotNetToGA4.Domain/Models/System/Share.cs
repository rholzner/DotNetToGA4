namespace DotNetToGA4.Domain.Models.System;

public class Share : Core
{
    public Share(string sharedAs, string typOfItemShared, string id)
    {
        SharedAs = sharedAs;
        TypOfItemShared = typOfItemShared;
        Id = id;
    }

    public string SharedAs { get; }
    public string TypOfItemShared { get; }
    public string Id { get; }
}
