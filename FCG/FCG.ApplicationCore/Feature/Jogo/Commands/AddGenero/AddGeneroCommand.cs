using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.ApplicationCore.Feature.Jogo.Commands.AddGenero
{
    public class AddGeneroCommand : IRequest<GeneroDto>
    {
        public required string Titulo { get; set; }
    }
}
