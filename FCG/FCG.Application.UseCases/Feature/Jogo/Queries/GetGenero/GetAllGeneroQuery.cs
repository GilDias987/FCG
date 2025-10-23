using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.Application.UseCases.Feature.Jogo.Queries.GetGenero
{
    public class GetAllGeneroQuery : IRequest<List<GeneroDto>>
    {
    }
}
