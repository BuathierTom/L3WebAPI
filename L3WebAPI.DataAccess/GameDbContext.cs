using L3WebAPI.Common.DAO;
using Microsoft.EntityFrameworkCore;

namespace L3WebAPI.DataAccess;

public class GameDbContext : DbContext
{
    public DbSet<GameDAO> Games { get; set; }
    
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var gameDaoBuilder = modelBuilder.Entity<GameDAO>();
        
        gameDaoBuilder.Property(g => g.AppId)
            .HasColumnName("app_id")
            .HasColumnType("uuid");
        
        // Primary key
        gameDaoBuilder.HasKey(g => g.AppId);
        gameDaoBuilder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasMaxLength(1000)
            .IsUnicode(true);
        
        var priceDaoBuilder = modelBuilder.Entity<PriceDAO>();

        priceDaoBuilder.Property(x => x.valeur)
            .HasColumnName("valeur")
            .HasPrecision(5, 2);

        priceDaoBuilder.Property(x => x.currency);

        priceDaoBuilder.HasKey(x => new { x.GameId, x.currency });
        
        // Foreign key
        gameDaoBuilder.HasMany(x => x.Prices)
            .WithOne(x => x.Game)
            .HasForeignKey(x => x.GameId);
        
        
    }
}
