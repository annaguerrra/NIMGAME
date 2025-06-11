public class Movimento
{
    public int Id { get; set; }
    public int JogoId { get; set; }
    public string Jogador { get; set; }
    public int PalitosRemovidos { get; set; }
    public DateTime DataMovimento { get; set; }
}