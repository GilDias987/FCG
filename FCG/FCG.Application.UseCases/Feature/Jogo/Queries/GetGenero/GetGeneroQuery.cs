using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.Application.UseCases.Feature.Jogo.Queries.GetGenero
{
    public class GetGeneroQuery : IRequest<GeneroDto>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}
