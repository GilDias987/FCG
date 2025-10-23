// Dependências
using FCG.ApplicationCore.Dto.Jogo;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FCG.Application.UseCases.Feature.Jogo.Queries.GetPlataforma
{
    public class GetAllPlataformaQueryHandler : IRequestHandler<GetAllPlataformaQuery, List<PlataformaDto>>
    {
        private readonly IPlataformaRepository _plataformaRepository;

        public GetAllPlataformaQueryHandler(IPlataformaRepository plataformaRepository)
        {
            _plataformaRepository = plataformaRepository;
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<PlataformaDto>> Handle(GetAllPlataformaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var plataforma = await _plataformaRepository.All.Select(s => new PlataformaDto 
                {
                    Id = s.Id, Titulo = s.Titulo,
                }).ToListAsync();

                if (!plataforma.Any())
                {
                    throw new ArgumentException("Nenhum registro encontrado.");
                }

                return plataforma;
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
