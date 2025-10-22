using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.AddPlataforma
{
    public class AddPlataformaCommand : IRequest<PlataformaDto>
    {
        public required string Titulo { get; set; }
    }
}
