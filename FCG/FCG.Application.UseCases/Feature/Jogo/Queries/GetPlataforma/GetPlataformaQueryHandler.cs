using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Jogo.Queries.GetPlataforma
{
    public class GetPlataformaQueryHandler : IRequestHandler<GetPlataformaQuery, PlataformaDto>
    {
        private readonly IPlataformaRepository _plataformaRepository;

        public GetPlataformaQueryHandler(IPlataformaRepository plataformaRepository)
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
        public async Task<PlataformaDto> Handle(GetPlataformaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var plataforma = await _plataformaRepository.GetByIdAsync(request.Id);
                if (plataforma is null)
                {
                    throw new ArgumentException("Plataforma não encontrada.");
                }

                // Dto
                return new PlataformaDto { Id = plataforma.Id, Titulo = plataforma.Titulo};
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
