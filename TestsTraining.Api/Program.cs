
using Microsoft.EntityFrameworkCore;
using TestsTraining.Data;
using TestsTraining.Data.Repositories;
using TestsTraining.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ProjectProjectDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("MockDbContextForUnitTesting")));
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseSwaggerUI();
app.UseSwagger(x => x.SerializeAsV2 = true);

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProjectProjectDbContext>();
    if (db.Database.IsRelational())
    {
        db.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
