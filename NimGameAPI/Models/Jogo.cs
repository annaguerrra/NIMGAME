public class Jogo
{
    public int Id { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public List<Movimento> Movimentos { get; set; }
}

public class Movimento
{
    public int Id { get; set; }
    public int JogoId { get; set; }
    public string Jogador { get; set; }
    public int PalitosRemovidos { get; set; }
    public DateTime DataMovimento { get; set; }
}