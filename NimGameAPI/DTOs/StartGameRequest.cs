namespace NimGameAPI.DTOs;

// “Data Transfer Object” para criar um novo jogo
public class StartGameRequest
{
    public int TotalPieces { get; set; }       // quantas peças colocar no início
    public int MaxRemovePerTurn { get; set; }  // máximo que pode tirar por jogada
    public bool PlayerStarts { get; set; }     // true = jogador começa, false = IA começa
}
