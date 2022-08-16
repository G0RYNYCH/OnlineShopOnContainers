namespace Identity.Api.Controllers;

public class IdentityController : BaseController
{
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UserManager<User> userManager;

    public IdentityController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    /// <summary>Creates the role asynchronous.</summary>
    /// <param name="name">The name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync(string name, CancellationToken cancellationToken)
    {
        var result = await roleManager.CreateAsync(new IdentityRole(name));

        return Ok(cancellationToken);
    }

    /// <summary>Edits the role asynchronous.</summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> EditRoleAsync(string id, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(id);

        if (role == null)
        {
            return BadRequest();
        }

        var model = new EditRole
        {
            Id = role.Id,
            RoleName = role.Name
        };

        foreach (var user in userManager.Users)
        {
            if(await userManager.IsInRoleAsync(user, role.Name))
            {
                model.Users.Add(user.UserName);
            }
        }

        return Ok(model);
    }
}
