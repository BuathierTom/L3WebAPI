using L3WebAPI.Common.DTO;
using L3WebAPI.Common.Request;

namespace L3WebAPI.Business.Interfaces;

public interface IGamesService
{
    Task<IEnumerable<GameDTO>> GetAllGames();
    Task<GameDTO?> GetGameById(Guid id);
    Task CreateGame(CreateGameRequest game);
    
}