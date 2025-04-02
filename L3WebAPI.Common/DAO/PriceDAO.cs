namespace L3WebAPI.Common.DAO;

public class PriceDAO
{
    public decimal valeur { set; get; }
    public Currency currency { set; get; }
    
    public Guid GameId { set; get; }
    
    public GameDAO Game { set; get; }
}