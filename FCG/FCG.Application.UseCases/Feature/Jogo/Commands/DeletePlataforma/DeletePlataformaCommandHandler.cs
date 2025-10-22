using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.DeletePlataforma
{
    public class DeletePlataformaCommandHandler : IRequestHandler<DeletePlataformaCommand, bool>
    {
        private readonly IPlataformaRepository _plataformaRepository;

        public DeletePlataformaCommandHandler(IPlataformaRepository plataformaRepository)
        {
            _plataformaRepository = plataformaRepository;
        }
        public async Task<bool>Handle(DeletePlataformaCommand request, CancellationToken cancellationToken)
        {
            var plataforma  = await _plataformaRepository.GetByIdAsync(request.Id);
            if (plataforma != null)
            {
                await _plataformaRepository.DeleteAsync(plataforma.Id);

                return true;
            }
            else
            {
                return false;

                throw new ArgumentException("Plataforma não foi encontrado.");
            }
        }
    }
}
