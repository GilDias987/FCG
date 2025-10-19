// Dependências
using FCG.ApplicationCore.Dto.Jogo;
using FCG.ApplicationCore.Interface.Repository;
using MediatR;

namespace FCG.ApplicationCore.Feature.Jogo.Commands.AddPlataforma
{
    public class AddPlataformaCommandHandler : IRequestHandler<AddPlataformaCommand, PlataformaDto>
    {
        private readonly IPlataformaRepository _plataformaRepository;

        public AddPlataformaCommandHandler(IPlataformaRepository plataformaRepository)
        {
            _plataformaRepository = plataformaRepository;
        }

        public async Task<PlataformaDto> Handle(AddPlataformaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var plataforma = await _plataformaRepository.AddAsync(new Domain.Entities.Plataforma(request.Titulo));

                return new PlataformaDto() { Id = plataforma.Id, Titulo = plataforma.Titulo };
            }
            catch (Exception)
            {
                throw new Exception("Ao cadastrar uma Plataforma ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}