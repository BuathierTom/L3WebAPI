using L3WebAPI.Common.DTO;

namespace L3WebAPI.Business.Interfaces;

public interface IGamesService
{
    Task<IEnumerable<GameDTO>> GetAllGames();
    Task<GameDTO?> GetGameById(Guid id);
    
}