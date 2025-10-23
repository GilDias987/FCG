// Dependências
using FCG.ApplicationCore.Dto.Jogo;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FCG.Application.UseCases.Feature.Jogo.Queries.GetJogo
{
    public class GetAllJogoQueryHandler : IRequestHandler<GetAllJogoQuery, List<JogoDto>>
    {
        private readonly IJogoRepository _jogoRepository;

        public GetAllJogoQueryHandler(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<JogoDto>> Handle(GetAllJogoQuery request, CancellationToken cancellationToken)
        {
            var jogo = await _jogoRepository.All.Select(s => new JogoDto
            {
                Id = s.Id, Titulo = s.Titulo, Descricao = s.Descricao, Preco = s.Preco, Desconto = s.Desconto, GeneroId = s.GeneroId, PlataformaId = s.PlataformaId 
            }).ToListAsync();

            if (!jogo.Any())
            {
                throw new ArgumentException("Nenhum registro encontrado.");
            }

            return jogo;
        }
    }
}
