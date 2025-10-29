using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Jogo.Queries.GetGenero
{
    public class GetGeneroQueryHandler : IRequestHandler<GetGeneroQuery, GeneroDto>
    {
        private readonly IGeneroRepository _generoRepository;

        public GetGeneroQueryHandler(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<GeneroDto> Handle(GetGeneroQuery request, CancellationToken cancellationToken)
        {
            var genero = await _generoRepository.GetByIdAsync(request.Id);
            if (genero is null)
            {
                throw new ArgumentException("Gênero não encontrado.");
            }

            return new GeneroDto { Id = genero.Id, Titulo = genero.Titulo };
        }
    }
}
