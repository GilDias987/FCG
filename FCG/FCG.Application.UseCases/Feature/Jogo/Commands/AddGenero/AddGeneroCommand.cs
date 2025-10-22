using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.AddGenero
{
    public class AddGeneroCommand : IRequest<GeneroDto>
    {
        public required string Titulo { get; set; }
    }
}
