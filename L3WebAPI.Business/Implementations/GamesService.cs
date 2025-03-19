using L3WebAPI.Business.Exceptions;
using L3WebAPI.Business.Interfaces;
using L3WebAPI.Common.DAO;
using L3WebAPI.Common.DTO;
using L3WebAPI.Common.Request;
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
            if (string.IsNullOrWhiteSpace(game.Name)) {
                throw new BusinessRuleException("Le nom du jeu ne peut pas être vide");
            }
            
            if (game.Name.Length > 1000) {
                throw new BusinessRuleException("Le nom du jeu ne peut pas dépasser 1000 caractères");
            }
            
            if (game.Prices.Count() < 1) {
                throw new BusinessRuleException("Le jeu doit avoir au moins un prix");
            }
            
            _gamesDataAccess.CreateGame(new GameDAO() {
                    AppId = Guid.NewGuid(),
                    Name = game.Name,
                    Prices = game.Prices.Select(price => new PriceDAO {
                        currency = price.Currency, 
                        valeur = price.Valeur,
                    })
            });
        }
        catch (Exception e) {
            _logger.LogError(e, "Erreur lors de la création du jeu");
            throw;
        }
    }
    
    public async Task<IEnumerable<GameDTO>> SearchGames(string name)
    {
        try
        {
            var games = await _gamesDataAccess.SearchGames(name);
            return games.Select(game => game.ToDTO());
        }
        catch (Exception e) {
            _logger.LogError(e, $"Erreur lors de la recherche des jeux contenant {name}", name); 
            return [];
        }
    }
}