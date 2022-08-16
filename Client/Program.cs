using IdentityModel.Client;
using Newtonsoft.Json.Linq;

var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}

//var tokenClient = new TokenClient(disco.TokenEndpoint, "clientId", "secret");
//var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");
var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
{
    Address = disco.TokenEndpoint,

    ClientId = "Api",
    ClientSecret = "secret",
    Scope = "Catalog.Api",

    UserName = "Admin",
    Password = "Admin"
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

Console.WriteLine(tokenResponse.Json);

var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync("https://localhost:6001/identity");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
}
else
{
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(JArray.Parse(content));
}

Console.ReadLine();
