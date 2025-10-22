namespace FCG.Application.UseCases.Feature.Usuario.Queries
{
    public record GetUsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Grupo { get; set; }
    }
}
