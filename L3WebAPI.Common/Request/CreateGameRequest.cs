namespace L3WebAPI.Common.Request;

public class CreateGameRequest
{
    public string Name { get; set; } = null!;
    public IEnumerable<CreateGameRequestPrice> Prices { get; set; } = null!;
}

public class CreateGameRequestPrice
{
    public decimal Valeur { get; set; }
    public Currency Currency { get; set; }
}