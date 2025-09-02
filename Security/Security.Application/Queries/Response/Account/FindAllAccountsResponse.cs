namespace Security.Application.Queries.Response.Account
{
    public record FindAllAccountsResponse(string Id, string Name, string IdUser, string Email, string Phone, string Group, string Role, string StartDate, string EndDate, string CreatedAt);
}
