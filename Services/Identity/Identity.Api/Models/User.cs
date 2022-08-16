using Identity.Api.Models;

namespace Identity.Domain;
public class User : IdentityUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UserRoles Role { get; set; }
}
