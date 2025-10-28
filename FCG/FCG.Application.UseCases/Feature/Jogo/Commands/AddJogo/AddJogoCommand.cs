using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.AddJogo
{
    public class AddJogoCommand : IRequest<JogoDto>
    {
        public required string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public decimal? Desconto { get; set; }
        public int GeneroId { get; set; }
        public int PlataformaId { get; set; }
    }
}
