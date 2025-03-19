namespace L3WebAPI.Common.Request;

public class UpdateGameRequest
{
    public string Name { get; set; } = null!;
    public IEnumerable<UpdateGameRequestPrice> Prices { get; set; } = null!;
}

public class UpdateGameRequestPrice
{
    public decimal Valeur { get; set; }
    public Currency Currency { get; set; }
}