using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.DeleteJogo
{
    public class DeleteJogoCommandHandler : IRequestHandler<DeleteJogoCommand, bool>
    {
        private readonly IJogoRepository _jogoRepository;

        public DeleteJogoCommandHandler(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<bool> Handle(DeleteJogoCommand request, CancellationToken cancellationToken)
        {
            var repJogo  = await _jogoRepository.GetByIdAsync(request.Id);
            if (repJogo != null)
            {
                await _jogoRepository.DeleteAsync(repJogo.Id);

                return true;
            }
            else
            {
                return false;

                throw new ArgumentException("Jogo não foi encontrado.");
            }
        }
    }
}
