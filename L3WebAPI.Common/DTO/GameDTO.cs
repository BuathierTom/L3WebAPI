using L3WebAPI.Common.DAO;

namespace L3WebAPI.Common.DTO;

public class GameDTO
{
    public Guid AppId { get; set; }
    public string Name { get; set; } = null!;

    public IEnumerable<PriceDTO> Prices { get; set; } = null!;

    public Uri LogoUri { get; set; } = null!;

}

public static class GameDTOExtensions
{
    public static GameDTO ToDTO(this GameDAO gameDAO)
    {
        return new GameDTO
        {
            AppId = gameDAO.AppId,
            Name = gameDAO.Name,
            Prices = gameDAO.Prices.Select(price => price.ToDTO()),
        };
    }
}
