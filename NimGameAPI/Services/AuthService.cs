public class AuthService
{
    // Usuários fixos só para teste
    private readonly List<Usuario> _usuarios = new()
    {
        new Usuario { Nome = "ana", Senha = "1234" },
        new Usuario { Nome = "bia", Senha = "abcd" }
    };

    public bool ValidarCredenciais(string nome, string senha)
    {
        return _usuarios.Any(u => u.Nome == nome && u.Senha == senha);
    }
}
