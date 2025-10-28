using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.Application.UseCases.Feature.Jogo.Queries.GetAllPlataforma
{
    public class GetAllPlataformaQuery : IRequest<List<PlataformaDto>>
    {
    }
}
