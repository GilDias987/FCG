using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.ApplicationCore.Feature.Jogo.Commands.EditPlataforma
{
    public class EditPlataformaCommand : IRequest<PlataformaDto>
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
    }
}
