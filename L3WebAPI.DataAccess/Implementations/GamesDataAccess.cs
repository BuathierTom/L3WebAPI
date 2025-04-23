using L3WebAPI.Common.DAO;
using L3WebAPI.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L3WebAPI.DataAccess.Implementations;

public class GamesDataAccess : IGamesDataAccess
{
    private readonly GameDbContext _dbContext;
    
    public GamesDataAccess(GameDbContext dbContext) {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<GameDAO>> GetAllGames() {
        return _dbContext.Games.Include(x => x.Prices).ToList();
        //return _dbContext.Games.Include(x => x.Prices).Include(x => x.OtherField).ToList();
        //return _dbContext.Games.Include(x => x.Prices).ThenInclude(x => x.PriceField).ToList();
    }
    
    public Task<GameDAO?> GetGameById(Guid id) {
        return _dbContext.Games
            .Include(x => x.Prices)
            .FirstOrDefaultAsync(x => x.AppId == id);
    }
    
    public async Task CreateGame(GameDAO game) {
        _dbContext.Games.Add(game);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<GameDAO>> SearchGames(string name) {
        return _dbContext.Games
            .Include(x => x.Prices)
            .Where(game => game.Name.ToLower().Contains(name.ToLower()));
        //.Where(game => game.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }
    
    public async Task UpdateGame(GameDAO game) {
        // Ici on fait du change tracking
        // vous pouvez getbyid dans le business, le modifier, puis faire juste savechangeasync();

        var oldGame = await GetGameById(game.AppId);
        oldGame.Name = game.Name;
        oldGame.Prices = game.Prices;

        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteGame(Guid id) {
        var oldGame = await GetGameById(id);
        _dbContext.Games.Remove(oldGame);
        await _dbContext.SaveChangesAsync();
    }
}