namespace NimGameAPI.Models;

public class Game
{
    public int GameId { get; set; }             // identificador único do jogo
    public int TotalPieces { get; private set; }// quantas peças ainda restam
    public int MaxRemovePerTurn { get; private set; } // máximo que pode tirar por vez
    public bool IsPlayerTurn { get; private set; }    // true = vez do jogador, false = vez da IA

    // Construtor: define estado inicial (quantas peças, máximo a remover e quem começa)
    public Game(int totalPieces, int maxRemovePerTurn, bool isPlayerFirst)
    {
        TotalPieces = totalPieces;
        MaxRemovePerTurn = maxRemovePerTurn;
        IsPlayerTurn = isPlayerFirst;
    }

    // Indica se o jogo acabou (sem peças)
    public bool IsGameOver => TotalPieces == 0;

    // Método para o jogador humano remover peças
    public void PlayerMove(int pieces)
    {
        if (!IsPlayerTurn)
            throw new InvalidOperationException("Não é seu turno.");

        if (pieces < 1 || pieces > MaxRemovePerTurn || pieces > TotalPieces)
            throw new ArgumentOutOfRangeException("Movimento inválido.");

        TotalPieces -= pieces;   // subtrai do total
        IsPlayerTurn = false;    // agora é vez da IA
    }

    // Método para a IA remover peças
    public void ComputerMove(int pieces)
    {
        if (IsPlayerTurn)
            throw new InvalidOperationException("Ainda não é vez da IA.");

        if (pieces < 1 || pieces > MaxRemovePerTurn || pieces > TotalPieces)
            throw new ArgumentOutOfRangeException("Movimento inválido.");

        TotalPieces -= pieces;   // subtrai do total
        IsPlayerTurn = true;     // volta a ser vez do jogador
    }
}
