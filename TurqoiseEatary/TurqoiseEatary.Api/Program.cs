
using TurqoiseEatary.Application;
using TurqoiseEatary.Application.Services.Authentication;
using TurqoiseEatary.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddOpenApi();
}



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

