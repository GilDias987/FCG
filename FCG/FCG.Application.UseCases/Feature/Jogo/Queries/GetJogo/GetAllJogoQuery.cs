using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.Application.UseCases.Feature.Jogo.Queries.GetJogo
{
    public class GetAllJogoQuery : IRequest<List<JogoDto>>
    {
    }
}
