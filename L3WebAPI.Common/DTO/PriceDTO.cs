using L3WebAPI.Common.DAO;

namespace L3WebAPI.Common.DTO;

public class PriceDTO
{
    public decimal valeur { set; get; }
    public Currency currency { set; get; }
    
}

public static class PriceDTOExtensions
{
    public static PriceDTO ToDTO(this PriceDAO priceDAO)
    {
        return new PriceDTO
        {
            valeur = priceDAO.valeur,
            currency = priceDAO.currency,
        };
    }
}