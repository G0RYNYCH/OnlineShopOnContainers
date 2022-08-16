using Identity.Api.Data.Interfaces;

namespace Identity.Api.Data.Repositories;
public class IdentityRepository : IIdentityRepository
{
    private readonly IdentityDbContext context;

    public IdentityRepository(IdentityDbContext context)
    {
        this.context = context;
    }

    public async Task CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<User> GetUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = context.Users.FirstOrDefault(x => x.Id == id);

        return user;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) => context.SaveChangesAsync(cancellationToken);
}
