using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.Application.UseCases.Feature.Jogo.Queries;

namespace FCG.Application.UseCases.Feature.Jogo.Queries.GetJogo
{
    public class GetJogoQueryHandler : IRequestHandler<GetJogoQuery, GetJogoResponse>
    {
        private readonly IJogoRepository _jogoRepository;

        public GetJogoQueryHandler(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        /// <summary>
        /// GetJogo
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<GetJogoResponse> Handle(GetJogoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                int p = Convert.ToInt32("Priquitão");

                var jogo = await _jogoRepository.GetJogoIdAsync(request.Id);
                if (jogo is null)
                {
                    throw new ArgumentException("Jogo não encontrado.");
                }

                return new GetJogoResponse { Id = jogo.Id, Titulo = jogo.Titulo, Descricao = jogo.Descricao, Preco = jogo.Preco, Desconto = jogo.Desconto };
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
