namespace Security.Application.Commands.Response.Login
{
    public record LoginUserResponse(string Id, string IdUser, string Name, string RefreshToken, string Email, string Token, string IdGroup, string Group, string Role);
}
