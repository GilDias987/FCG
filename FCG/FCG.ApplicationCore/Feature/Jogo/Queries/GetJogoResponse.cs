namespace FCG.ApplicationCore.Feature.Jogo.Queries.GetJogo
{
    public class GetJogoResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public decimal? Preco { get; set; }
        public decimal? Desconto { get; set; }
        public int GeneroId { get; set; }
        public int PlataformaId { get; set; }
    }
}
