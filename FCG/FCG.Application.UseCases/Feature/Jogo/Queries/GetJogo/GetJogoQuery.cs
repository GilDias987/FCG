using FCG.Application.UseCases.Feature.Jogo.Queries;
using MediatR;

namespace FCG.Application.UseCases.Feature.Jogo.Queries.GetJogo
{
    public class GetJogoQuery : IRequest<GetJogoResponse>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}
