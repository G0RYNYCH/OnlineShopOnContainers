namespace Identity.Api.Data.Interfaces;
public interface IIdentityRepository
{
    Task CreateUserAsync(User user, CancellationToken cancellationToken);

    Task<User> GetUserAsync(int id, CancellationToken cancellationToken);
    
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
