using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.EditJogo
{
    public class EditJogoCommand : IRequest<JogoDto>
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public decimal? Desconto { get; set; }
        public int GeneroId { get; set; }
        public int PlataformaId { get; set; }
    }
}
