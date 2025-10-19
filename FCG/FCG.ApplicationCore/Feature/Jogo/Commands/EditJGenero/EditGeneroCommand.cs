using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.ApplicationCore.Feature.Jogo.Commands.EditGenero
{
    public class EditGeneroCommand : IRequest<GeneroDto>
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
    }
}
