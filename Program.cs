using LeagueOfLegendsCharachters.Data;
using LeagueOfLegendsCharachters.Mapper;
using LeagueOfLegendsCharachters.Models;
using LeagueOfLegendsCharachters.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("ConnectionString returned null");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connection);
});

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var _scope = app.Services.CreateScope())
{
    var _context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!_context.Charachters.Any())
    {
        _context.Charachters.AddRange(
        new Charachter()
        {
            Name = "Jhin",
            BlueEssence = 6200,
            RolePosition = RolePositionEnum.Bottom,
            Status = new Status
            {
                Damage = 59,
                Armor = 24,
                Health = 665,
                AttackRange = 550,
                MagicResist = 30,
                MovementSpeed = 330, 
            }
        },
        new Charachter()
        {
            Name = "Urgot",
            BlueEssence = 1575,
            RolePosition = RolePositionEnum.Top,
            Status = new Status
            {
                Damage = 63,
                Armor = 36,
                Health = 655,
                AttackRange = 350,
                MagicResist = 32,
                MovementSpeed = 330
            }
        },
        new Charachter()
        {
            Name = "Daruis",
            BlueEssence = 225,
            RolePosition = RolePositionEnum.Top,
            Status = new Status
            {
                Damage = 64,
                Armor = 37,
                Health = 652,
                AttackRange = 175,
                MagicResist = 32,
                MovementSpeed = 340
            }
        }
        );
        _context.SaveChanges();
    }
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
