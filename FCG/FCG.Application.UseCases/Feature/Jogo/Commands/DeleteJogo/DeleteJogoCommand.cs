using MediatR;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.DeleteJogo
{
    public class DeleteJogoCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }
}
