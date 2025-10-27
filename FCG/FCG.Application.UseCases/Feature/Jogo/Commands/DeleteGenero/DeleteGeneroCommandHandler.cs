using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.DeleteGenero
{
    public class DeleteGeneroCommandHandler : IRequestHandler<DeleteGeneroCommand, bool>
    {
        private readonly IGeneroRepository _generoRepository;

        public DeleteGeneroCommandHandler(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }
        public async Task<bool>Handle(DeleteGeneroCommand request, CancellationToken cancellationToken)
        {
            var repGenero  = await _generoRepository.GetByIdAsync(request.Id);
            if (repGenero != null)
            {
                await _generoRepository.DeleteAsync(repGenero.Id);

                return true;
            }
            else
            { 
                throw new ArgumentException("Gênero não foi encontrado.");
            }
        }
    }
}
