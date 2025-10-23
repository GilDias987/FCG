using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.Application.UseCases.Feature.Jogo.Queries.GetJogo
{
    public class GetJogoQuery : IRequest<JogoDto>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}
