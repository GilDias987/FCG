using MediatR;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.DeletePlataforma
{
    public class DeletePlataformaCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }
}
