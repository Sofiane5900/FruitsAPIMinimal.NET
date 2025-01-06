using FruitsAPI.NET;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Swagger Doc
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"))
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/fruits", async (AppDbContext db) => await db.Fruits.ToArrayAsync());

app.MapPost(
    "/fruits",
    async (AppDbContext db, Fruits fruits) =>
    {
        db.Fruits.Add(fruits);
        await db.SaveChangesAsync();

        return Results.Created($"/fruits/{fruits.ID}", fruits);
    }
);

app.MapPut(
    "/fruits/{id}",
    async (int id, Fruits updatedFruits, AppDbContext db) =>
    {
        var fruits = await db.Fruits.FindAsync(id);
        if (fruits is null)
        {
            return Results.NotFound();
        }

        fruits.Nom = updatedFruits.Nom;
        fruits.Saison = updatedFruits.Saison;

        await db.SaveChangesAsync();
        return Results.NoContent();
    }
);

app.UseHttpsRedirection();

app.Run();
