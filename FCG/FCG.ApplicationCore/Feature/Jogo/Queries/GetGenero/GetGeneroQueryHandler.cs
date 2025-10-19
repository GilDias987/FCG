using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.ApplicationCore.Feature.Jogo.Queries.GetGenero
{
    public class GetGeneroQueryHandler : IRequestHandler<GetGeneroQuery, GeneroDto>
    {
        private readonly IGeneroRepository _generoRepository;

        public GetGeneroQueryHandler(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        /// <summary>
        /// GetGenero
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<GeneroDto> Handle(GetGeneroQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var genero  = await _generoRepository.GetGeneroIdAsync(request.Id);
                if (genero is null)
                {
                    throw new ArgumentException("Gênero não encontrado.");
                }

                return new GeneroDto { Id = genero.Id, Titulo = genero.Titulo};
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
