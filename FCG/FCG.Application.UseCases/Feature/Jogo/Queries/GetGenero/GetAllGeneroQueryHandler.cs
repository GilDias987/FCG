// Dependências
using FCG.ApplicationCore.Dto.Jogo;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FCG.Application.UseCases.Feature.Jogo.Queries.GetGenero
{
    public class GetAllGeneroQueryHandler : IRequestHandler<GetAllGeneroQuery, List<GeneroDto>>
    {
        private readonly IGeneroRepository _generoRepository;

        public GetAllGeneroQueryHandler(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<GeneroDto>> Handle(GetAllGeneroQuery request, CancellationToken cancellationToken)
        {
            var genero = await _generoRepository.All.Select(s => new GeneroDto 
            {
                Id = s.Id, Titulo = s.Titulo,
            }).ToListAsync();

            if (!genero.Any())
            {
                throw new ArgumentException("Nenhum registro encontrado.");
            }

            return genero;
        }
    }
}
