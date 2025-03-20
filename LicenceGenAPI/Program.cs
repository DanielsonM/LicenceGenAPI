using Asp.Versioning.Routing;
using LicenceGenAPI.DbConnection;
using LicenceGenAPI.Repository.Implementation;
using LicenceGenAPI.Repository.Service;
using LicenceGenAPI.Rules.Implementation;
using LicenceGenAPI.Rules.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<PostgreDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionPostgre"), version =>
version.SetPostgresVersion(17, 4)));

builder.Services.AddApiVersioning();

builder.Services.AddScoped<ILicenceRuleService, LicenceRule>();
builder.Services.AddScoped<ILicenceRepositoryService, LicenceRepository>();

var app = builder.Build();

// Aplica as migrações automaticamente ao iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<PostgreDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
