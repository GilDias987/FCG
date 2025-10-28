namespace FCG.Application.Dto.Autenticacao
{

    public class LoginUsuarioDto
    {
        public required string Email { get; set; }

        public required string Senha { get; set; }
    }
}
