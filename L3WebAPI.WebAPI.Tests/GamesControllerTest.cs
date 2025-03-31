using System.Net;
using System.Text;
using System.Text.Json;
using L3WebAPI.Common.DTO;
using L3WebAPI.Common.Request;
using Microsoft.AspNetCore.Mvc.Testing;

namespace L3WebAPI.WebAPI.Tests;

public class GamesControllerTest
{
    private readonly HttpClient client;
    
    public GamesControllerTest()
    {
        var webApplicationFactory = new WebApplicationFactory<Program>();
        client = webApplicationFactory.CreateClient();
    }
    
    [Fact]
    public async Task Should_GetAllGames_ArrayOfGames_OK()
    {
        var res = await client.GetAsync("/api/Games");
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        
        var data = JsonSerializer.Deserialize<IEnumerable<GameDTO>>(
            await res.Content.ReadAsStringAsync()
        );

        Assert.NotNull(data);
        Assert.True((data.Count() > 0));
    }
    
    [Theory]
    [InlineData("a", 2)]
    [InlineData("A", 2)]
    [InlineData("al", 2)]
    [InlineData("alf", 1)]
    [InlineData("z", 0)]
    public async Task Should_SearchByName_ArrayOfGames_OK(string name, int expectedCount)
    {
        var res = await client.GetAsync($"/api/Games/search/{name}");
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        
        var data = JsonSerializer.Deserialize<IEnumerable<GameDTO>>(
            await res.Content.ReadAsStringAsync()
        );

        Assert.NotNull(data);
        Assert.Equal(expectedCount, data.Count());
    }
    
    [Fact]
    public async Task Should_CreateGame_OK() {
        var game = new CreateGameRequest {
            Name = "Minecraft",
            Prices = [
            new()
            {
                Valeur = 19.99M,
                Currency = Common.Currency.USD,
            }
            
            ]
        };
        
        var content = new StringContent(
            JsonSerializer.Serialize(game),
            Encoding.UTF8,
            "application/json"
        );
        
        var res = await client.PostAsync("/api/Games", content);
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
    }

    [Fact]
    public async Task Should_CreateGame_KO() {
        var game = new CreateGameRequest {
            Name = "",
            Prices = [
                new()
                {
                    Valeur = 19.99M,
                    Currency = Common.Currency.USD,
                }
            
            ]
        };
        
        var content = new StringContent(
            JsonSerializer.Serialize(game),
            Encoding.UTF8,
            "application/json"
        );
        
        var res = await client.PostAsync("/api/Games", content);
        Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
        Assert.Equal("Le nom du jeu ne peut pas être vide", await res.Content.ReadAsStringAsync());
    }
}