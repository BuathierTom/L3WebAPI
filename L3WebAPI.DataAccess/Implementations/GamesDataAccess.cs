using L3WebAPI.Common;
using L3WebAPI.Common.DAO;
using L3WebAPI.DataAccess.Interfaces;

namespace L3WebAPI.DataAccess.Implementations;

public class GamesDataAccess : IGamesDataAccess
{
    public static List<GameDAO> games = [
        new GameDAO {
            AppId = Guid.NewGuid(),
            Name = "Portal 2",
            Prices =
            [
                new PriceDAO
                {
                    valeur = 19.99M,
                    currency = Currency.USD
                }
            ]
        },
        new GameDAO {
            AppId = Guid.NewGuid(),
            Name = "Half-Life 2",
            Prices =
            [
                new PriceDAO
                {
                    valeur = 14.99M,
                    currency = Currency.EUR
                },
                new PriceDAO
                {
                    valeur = 15.99M,
                    currency = Currency.USD
                }
            ]
        },
    ];
    public async Task<IEnumerable<GameDAO>> GetAllGames()
    {
        return games;
    }
    
    public async Task<GameDAO?> GetGameById(Guid id)
    {
        return games.FirstOrDefault(game => game.AppId == id);
    }
    
    public async Task CreateGame(GameDAO game)
    {
        games.Add(game);
    }
    
    public async Task<IEnumerable<GameDAO>> SearchGames(string name)
    {
        return games.Where(game => game.Name.Contains(name));
    }
    
    public async Task UpdateGame(GameDAO game)
    {
        var existingGame = games.FirstOrDefault(g => g.AppId == game.AppId);
        if (existingGame is null)
        {
            throw new Exception("Game not found");
        }
        existingGame.Name = game.Name;
        existingGame.Prices = game.Prices;
    }
}