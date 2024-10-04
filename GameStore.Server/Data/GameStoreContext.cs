using GameStore.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Server.Data;

public class GameStoreContext : DbContext
{
    public DbSet<Game> Games => Set<Game>();
    public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
    {

    }


}