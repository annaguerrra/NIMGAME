using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;

    public AuthController(AuthService auth) => _auth = auth;

    [HttpPost("login")]
    public IActionResult Login([FromBody] Usuario usuario)
    {
        bool valido = _auth.ValidarCredenciais(usuario.Nome, usuario.Senha);
        if (!valido)
            return Unauthorized(new { mensagem = "Usuário ou senha inválidos" });

        return Ok(new { mensagem = "Login bem-sucedido", usuario = usuario.Nome });
    }
}
