[ApiController]
[Route("api/jogos")]
public class JogosController : ControllerBase
{
    private readonly JogoService _service;
    public JogosController(JogoService service) => _service = service;

    [HttpPost]
    public ActionResult<Jogo> CriarJogo()
    {
        var jogo = _service.Iniciar();
        return CreatedAtAction(nameof(Obter), new { id = jogo.Id }, jogo);
    }

    [HttpGet("{id}")]
    public ActionResult<Jogo> Obter(int id)
    {
        var jogo = _service.ObterPorId(id);
        if (jogo == null) return NotFound();
        return Ok(jogo);
    }

    [HttpPost("{id}/movimentos")]
    public ActionResult<Movimento> Jogar(int id, [FromBody] MovimentoDTO dto)
    {
        var movimento = _service.FazerMovimento(id, dto.Palitos, dto.Jogador);
        return Ok(movimento);
    }
}