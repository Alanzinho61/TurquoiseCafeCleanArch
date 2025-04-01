
using TurqoiseEatary.Api;
using TurqoiseEatary.Api.Middleware;
using TurqoiseEatary.Application;
using TurqoiseEatary.Application.Services.Authentication;
using TurqoiseEatary.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);


    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddOpenApi();
}



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

