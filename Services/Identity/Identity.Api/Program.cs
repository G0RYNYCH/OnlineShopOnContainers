var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddIdentityServer()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("DbConnection"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(assembly);
            });
    })
    .AddTestUsers(IdentityConfiguration.TestUsers)
    .AddDeveloperSigningCredential();

builder.Services.AddPersistance(builder.Configuration);

builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "609996360456 - 00en6qqut1tip3mtrnp1ofukmjol52pe.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIdentityServer();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
