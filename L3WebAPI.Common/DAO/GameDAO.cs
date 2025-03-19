namespace L3WebAPI.Common.DAO;

public class GameDAO
{
    public Guid AppId { get; set; }
    public string Name { get; set; } = null!;

    public IEnumerable<PriceDAO> Prices { get; set; } = null!;



}