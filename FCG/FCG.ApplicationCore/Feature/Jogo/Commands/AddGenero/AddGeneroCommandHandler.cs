// Dependências
using FCG.ApplicationCore.Dto.Jogo;
using FCG.ApplicationCore.Interface.Repository;
using MediatR;

namespace FCG.ApplicationCore.Feature.Jogo.Commands.AddGenero
{
    public class AddGeneroCommandHandler : IRequestHandler<AddGeneroCommand, GeneroDto>
    {
        private readonly IGeneroRepository _generoRepository;

        public AddGeneroCommandHandler(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<GeneroDto> Handle(AddGeneroCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objGenero = await _generoRepository.AddAsync(new Domain.Entities.Genero(request.Titulo));

                return new GeneroDto() { Id = objGenero.Id, Titulo = objGenero.Titulo };
            }
            catch (Exception)
            {
                throw new Exception("Ao cadastrar o Genero ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}