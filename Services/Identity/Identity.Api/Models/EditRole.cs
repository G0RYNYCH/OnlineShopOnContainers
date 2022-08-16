namespace Identity.Api.Models;
public class EditRole
{
    public string Id { get; set; }
    public string RoleName { get; set; }
    public List<string> Users { get; set; }
}
