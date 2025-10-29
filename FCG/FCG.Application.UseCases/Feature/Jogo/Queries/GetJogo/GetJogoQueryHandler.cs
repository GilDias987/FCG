using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Jogo.Queries.GetJogo
{
    public class GetJogoQueryHandler : IRequestHandler<GetJogoQuery, JogoDto>
    {
        private readonly IJogoRepository _jogoRepository;

        public GetJogoQueryHandler(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        /// <summary>
        /// GetJogoIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<JogoDto> Handle(GetJogoQuery request, CancellationToken cancellationToken)
        {

            var jogo = await _jogoRepository.GetByIdAsync(request.Id);
           
            if (jogo is null)
            {
                throw new ArgumentException("Jogo não encontrado.");
            }

            return new JogoDto { Id = jogo.Id, Titulo = jogo.Titulo, Descricao = jogo.Descricao, Preco = jogo.Preco, Desconto = jogo.Desconto, PlataformaId = jogo.PlataformaId, GeneroId = jogo.GeneroId };

        }
    }
}
