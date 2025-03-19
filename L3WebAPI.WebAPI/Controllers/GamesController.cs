using L3WebAPI.Business.Interfaces;
using L3WebAPI.Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace L3WebAPI.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IGamesService _gamesService;

    public GamesController(IGamesService gamesService)
    {
        _gamesService = gamesService;
    }

    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GameDTO>>> GetAllGames()
    {
        return Ok(await _gamesService.GetAllGames());
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GameDTO>> GetGame(Guid id)
    {
        var game = await _gamesService.GetGameById(id);
        if (game is null)
        {
            return NotFound();
        }
        return Ok(game);
    }
    
    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GameDTO>> CreateGame(GameDTO game)
    {
        if (game is null)
        {
            return BadRequest();
        }
        var createdGame = await _gamesService.CreateGame(game);
        return CreatedAtAction(nameof(GetGame), new { id = createdGame.AppId }, createdGame);
    }
    
}