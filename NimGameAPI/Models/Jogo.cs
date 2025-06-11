public class Jogo
{
    public int Id { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public List<Movimento> Movimentos { get; set; }
}