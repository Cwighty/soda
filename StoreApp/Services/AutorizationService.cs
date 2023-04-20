using Microsoft.AspNetCore.Components;
using Supabase;

namespace StoreApp.Services;

public class AuthorizationService
{
    private readonly Client client;

    public AuthorizationService(Client client)
    {
        this.client = client;
    }

    public async Task LogIn(string email, string password)
    {
        await client.Auth.SignInWithPassword(email, password);
    }

    public async Task Register(string email, string password)
    {
        var session = await client.Auth.SignUp(email, password);
    }

    public async Task Logout()
    {
        await client.Auth.SignOut();
    }

    public bool IsLoggedIn()
    {
        return client.Auth.CurrentSession != null;
    }

    public bool IsAdmin()
    {
        if (IsLoggedIn())
        {
            return client.Auth.CurrentUser?.Email.Contains("admin@thesodageyser.com") ?? false;
        }
        return false;
    }
}
