using MediatR;

namespace FCG.ApplicationCore.Feature.Jogo.Commands.DeleteJogo
{
    public class DeleteJogoCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }
}
