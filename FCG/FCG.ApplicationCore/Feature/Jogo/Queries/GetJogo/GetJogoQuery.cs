using MediatR;

namespace FCG.ApplicationCore.Feature.Jogo.Queries.GetJogo
{
    public class GetJogoQuery : IRequest<GetJogoResponse>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}
