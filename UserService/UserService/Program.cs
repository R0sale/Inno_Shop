using Microsoft.AspNetCore.Mvc;
using UserService.Extensions;
using Newtonsoft.Json;
using UserRepository;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using MediatR;
using FluentValidation;
using Application.Behaviors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.ConfigureIdentity();
builder.Services.AddAutoMapper(typeof(Application.AssemblyReferense).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyReferense).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>),
typeof(ValidationBehavior<,>));
builder.Services.ConfigureEmailService();
builder.Services.AddValidatorsFromAssembly(typeof(Application.AssemblyReferense).Assembly);

builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddControllers()
    .AddNewtonsoftJson();

if (builder.Environment.IsEnvironment("Test"))
{
    builder.Services.AddDbContext<UserRepositoryContext>(options =>
        options.UseInMemoryDatabase("TestDb"));
}
else
{
    builder.Services.ConfigureSqlContext(builder.Configuration);
}

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<UserRepositoryContext>();
    context.Database.Migrate();
}

app.UseRouting();

app.ConfigureExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }