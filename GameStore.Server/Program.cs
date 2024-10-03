using GameStore.Server.Models;

List<Game> games = new()
    {
        new Game()
        {
            Id = 1,
            Name = "Yakuza 0",
            Genre = "Bitemap",
            Price = 29.99M,
            ReleaseDate = new DateOnly(2015, 12, 25)
        },
        new Game()
        {
            Id = 2,
            Name = "Final Fantasy XIV",
            Genre = "Roleplaying",
            Price = 59.99M,
            ReleaseDate = new DateOnly(2010, 9, 30)
        },
        new Game()
        {
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            Price = 69.99M,
            ReleaseDate = new DateOnly(2022, 6, 17)
        }
    };

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.WithOrigins("http://localhost:5161")
           .AllowAnyHeader()
           .AllowAnyMethod();
}));

var app = builder.Build();

app.UseCors();

var group = app.MapGroup("/games")
               .WithParameterValidation();

// GET /games
group.MapGet("/", () => games);

// GET /games/{id}
group.MapGet("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);

    if (game is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(game);
})
.WithName("GetGame");

// POST /games
group.MapPost("/", (Game game) =>
{
    game.Id = games.Max(game => game.Id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game);
});


//PUT /games/{id}
group.MapPut("/{id}", (int id, Game updatedGame) =>
{
    Game? existingGame = games.Find(game => game.Id == id);

    if (existingGame is null)
    {
        updatedGame.Id = id;
        games.Add(updatedGame);
        return Results.CreatedAtRoute("GetGame", new { id = updatedGame.Id }, updatedGame);
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
});


// DELETE /games/{id}
group.MapDelete("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);

    if (game is null)
    {
        return Results.NotFound();
    }

    games.Remove(game);

    return Results.NoContent();
});

app.Run();
