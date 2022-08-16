namespace Identity.Api;
public class IdentityConfiguration
{
    public static List<TestUser> TestUsers => new()
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "Admin",
            Password = "Admin",
            Claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, "Admin Main"),
                new Claim(JwtClaimTypes.GivenName, "Admin"),
                new Claim(JwtClaimTypes.FamilyName, "Main"),
            }
        }
    };

    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    };

    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
    {
        new ApiScope("Catalog.Api", "Catalog.Api"),
        new ApiScope("Bag.Api", "Bag.Api")
    };

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("Catalog.Api", "Catalog.Api"),
        new ApiResource("Bag.Api", "Bag.Api")
    };

    public static IEnumerable<Client> Clients => new List<Client>
    {
        new Client
        {
            ClientId = "Api",
            ClientName = "MyApi",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            AllowAccessTokensViaBrowser = true,
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedScopes = { "Catalog.Api", "Bag.Api" }
        }
    };
}
