using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.ApplicationCore.Feature.Jogo.Queries.GetPlataforma
{
    public class GetPlataformaQuery : IRequest<PlataformaDto>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}
