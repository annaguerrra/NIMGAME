public class JogoService
{
    private readonly MeuDbContext _context;
    public JogoService(MeuDbContext ctx) => _context = ctx;

    public Jogo Iniciar()
    {
        var jogo = new Jogo { DataInicio = DateTime.Now, Movimentos = new List<Movimento>() };
        _context.Jogos.Add(jogo);
        _context.SaveChanges();
        return jogo;
    }

    public Jogo ObterPorId(int id) =>
        _context.Jogos.Include(j => j.Movimentos).FirstOrDefault(j => j.Id == id);

    public Movimento FazerMovimento(int jogoId, int palitos, string jogador)
    {
        var mov = new Movimento { JogoId = jogoId, PalitosRemovidos = palitos, Jogador = jogador, DataMovimento = DateTime.Now };
        _context.Movimentos.Add(mov);
        _context.SaveChanges();
        return mov;
    }
}