using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.EditJPlataforma
{
    public class EditPlataformaCommandHandler : IRequestHandler<EditPlataformaCommand, PlataformaDto>
    {
        private readonly IPlataformaRepository _plataformaRepository;

        public EditPlataformaCommandHandler(IPlataformaRepository plataformaRepository)
        {
            _plataformaRepository = plataformaRepository;
        }

        public async Task<PlataformaDto> Handle(EditPlataformaCommand request, CancellationToken cancellationToken)
        {
            var plataforma  = await _plataformaRepository.GetByIdAsync(request.Id);
            if (plataforma != null)
            {
                try
                {
                    await _plataformaRepository.UpdateAsync(plataforma);

                    return new PlataformaDto() { Id = plataforma.Id, Titulo = plataforma.Titulo};
                }
                catch (Exception)
                {
                    throw new Exception("Ao alterar a plataforma ocorreu uma falha inesperada. Tente novamente mais tarde.");
                }
            }
            else
            {
                throw new ArgumentException("Plataforma não foi encontrado.");
            }
        }
    }
}
