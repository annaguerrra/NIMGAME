namespace NimGameAPI.DTOs;

// DTO que representa a resposta da IA (equipe Python)
public class ComputerMoveRequest
{
    public int Pieces { get; set; }  // quantas peças a IA decidiu remover
}
