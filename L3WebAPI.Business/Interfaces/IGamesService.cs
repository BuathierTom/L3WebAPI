using L3WebAPI.Common.DTO;
using L3WebAPI.Common.Request;

namespace L3WebAPI.Business.Interfaces;

public interface IGamesService
{
    Task<IEnumerable<GameDTO>> GetAllGames();
    Task<GameDTO?> GetGameById(Guid id);
    Task CreateGame(CreateGameRequest game);
    Task<IEnumerable<GameDTO>> SearchGames(string name);
    Task UpdateGame(Guid id, UpdateGameRequest request);
    Task DeleteGame(Guid id);
}