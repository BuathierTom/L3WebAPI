using L3WebAPI.Business.Interfaces;
using L3WebAPI.Common.DTO;
using L3WebAPI.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace L3WebAPI.Business.Implementations;

public class GamesService : IGamesService
{
    private readonly IGamesDataAccess _gamesDataAccess;
    private readonly ILogger<GamesService> _logger;
    
    public GamesService(IGamesDataAccess gamesDataAccess, ILogger<GamesService> logger)
    {
        _gamesDataAccess = gamesDataAccess;
        _logger = logger;
    }
    
    public async Task<IEnumerable<GameDTO>> GetAllGames()
    {
        try
        {
            var games = await _gamesDataAccess.GetAllGames();
            return games.Select(game => game.ToDTO());
        }
        catch (Exception e) {
            _logger.LogError(e, "Erreur lors de la récuperation des jeux"); 
            return [];
        }
    }
    
    public async Task<GameDTO?> GetGameById(Guid id)
    {
        try
        {
            var game = await _gamesDataAccess.GetGameById(id);
            return game?.ToDTO();
        }
        catch (Exception e) {
            _logger.LogError(e, $"Erreur lors de la récuperation du jeu {id}", id); 
            return null;
        }
    }
    
    public async Task CreateGame (CreateGameRequest game) {
        try
        {
            await _gamesDataAccess.CreateGame(game.ToDAO());
        }
        catch (Exception e) {
            _logger.LogError(e, "Erreur lors de la création du jeu"); 
        }
    }
}