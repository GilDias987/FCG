using MediatR;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.DeleteGenero
{
    public class DeleteGeneroCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }
}
