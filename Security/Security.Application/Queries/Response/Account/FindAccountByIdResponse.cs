namespace Security.Application.Queries.Response.Account
{
    public record FindAccountByIdResponse(string Id, string Name, string IdUser, string Email, string Phone, string Role, string Group, string IdGroup, string StartDate, string EndDate, string CreatedAt);
}
