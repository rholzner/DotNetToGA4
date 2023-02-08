namespace DotNetToGA4.Domain.Models.System;

public class SignUp : Core
{
    public SignUp(string signUpTo)
    {
        SignUpTo = signUpTo;
    }

    public string SignUpTo { get; }
}