using MediatR;

namespace FCG.ApplicationCore.Feature.Jogo.Commands.DeletePlataforma
{
    public class DeletePlataformaCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }
}
