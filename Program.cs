using radius_minimal_api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "RADIUS_API",
        Version = "2.1",
        Description =
        "Esta API consome a API da Intranet do IFCE-Maracanaú para autenticação de usuários " +
        "e verifica se o usuário é um servidor. Caso seja, " +
        "o usuário é autorizado a se conectar à rede RADIUS.",
        Contact = new()
        {
            Name = "Victor Guilherme",
            Url = new Uri("https://github.com/1-LEI-DE-NEWTON")
        }
    });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("api/v2/authentication", async (string username, string password) =>
{
    var response = await AuthService.Authenticate(username, password);

    if (response is null)
    {
        return Results.Unauthorized();
    }
    
    else
    {        
        if (IntranetDataResponseHandler.ConnectionsHandler(response) is true)
        {
            return Results.Ok();
        }
        else
        {
            return Results.Forbid();
        }
    }
});

app.Run();