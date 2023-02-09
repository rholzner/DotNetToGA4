namespace DotNetToGA4.Domain.Models.System;

public class Login : Core
{
	public Login(string loginWithSystem)
	{
        LoginWithSystem = loginWithSystem;
    }

    public string LoginWithSystem { get; }
}