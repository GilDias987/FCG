namespace FCG.ApplicationCore.Dto.Jogo
{
    public class JogoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public decimal? Preco { get; set; }
        public decimal? Desconto { get; set; }
        public int GeneroId { get; set; }
        public int PlataformaId { get; set; }
        public string PrecoDesconto { get; set; }
    }
}
