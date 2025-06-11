using Microsoft.AspNetCore.Mvc;
using NimGameAPI.Models;
using NimGameAPI.DTOs;
using System.Net.Http.Json;

namespace NimGameAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    // Armazenamos jogos em memória (dicionário simples)
    private static readonly Dictionary<int, Game> games = new();
    private static int gameCounter = 1;

    // ========== Iniciar um novo jogo ==========
    [HttpPost("start")]
    public IActionResult StartGame(StartGameRequest request)
    {
        var game = new Game(request.TotalPieces, request.MaxRemovePerTurn, request.PlayerStarts);
        game.GameId = gameCounter;
        games[gameCounter++] = game;

        // Retorna o ID do jogo e quantas peças sobraram
        return Ok(new { GameId = game.GameId, Remaining = game.TotalPieces });
    }

    // ========== Jogada do jogador ==========
    [HttpPost("{gameId}/player-move")]
    public IActionResult PlayerMove(int gameId, PlayerMoveRequest request)
    {
        if (!games.TryGetValue(gameId, out var game))
            return NotFound("Jogo não encontrado.");

        try
        {
            game.PlayerMove(request.Pieces);
            return Ok(new { Remaining = game.TotalPieces, NextTurn = "Computer" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // ========== Jogada da IA ==========
    [HttpPost("{gameId}/computer-move")]
    public async Task<IActionResult> ComputerMove(int gameId)
    {
        if (!games.TryGetValue(gameId, out var game))
            return NotFound("Jogo não encontrado.");

        try
        {
            // 1) Chama a equipe Python para decidir a jogada
            int move = await GetComputerMoveAsync(game.TotalPieces, game.MaxRemovePerTurn);

            // 2) Aplica o movimento no modelo
            game.ComputerMove(move);

            return Ok(new {
                Remaining       = game.TotalPieces,
                ComputerRemoved = move,
                NextTurn        = "Player"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // ========== Estado atual do jogo ==========
    [HttpGet("{gameId}/status")]
    public IActionResult GetStatus(int gameId)
    {
        if (!games.TryGetValue(gameId, out var game))
            return NotFound("Jogo não encontrado.");

        return Ok(new {
            TotalPieces  = game.TotalPieces,
            IsPlayerTurn = game.IsPlayerTurn,
            IsGameOver   = game.IsGameOver
        });
    }

    // ======= Chamando a API Python da IA =======
    private async Task<int> GetComputerMoveAsync(int remaining, int max)
    {
        using var client = new HttpClient();
        // Envia JSON { total: remaining, max: max }
        var response = await client.PostAsJsonAsync(
            "http://ENDERECO_DA_API_PYTHON/move",
            new { total = remaining, max }
        );

        response.EnsureSuccessStatusCode();

        // Lê resposta { pieces: X }
        var result = await response.Content.ReadFromJsonAsync<ComputerMoveRequest>();
        return result.Pieces;
    }
}
