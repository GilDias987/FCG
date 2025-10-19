using MediatR;

namespace FCG.ApplicationCore.Feature.Jogo.Commands.DeleteGenero
{
    public class DeleteGeneroCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }
}
