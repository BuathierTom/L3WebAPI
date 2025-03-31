using L3WebAPI.Common.DAO;

namespace L3WebAPI.DataAccess.Interfaces;

public interface IGamesDataAccess
{
    Task<IEnumerable<GameDAO>> GetAllGames();
    Task<GameDAO?> GetGameById(Guid id);
    Task CreateGame(GameDAO game);
    Task<IEnumerable<GameDAO>> SearchGames(string name);
    Task UpdateGame(GameDAO game);
    Task DeleteGame(Guid id);
}