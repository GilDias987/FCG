using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.EditJogo
{
    public class EditJogoCommandHandler : IRequestHandler<EditJogoCommand, JogoDto>
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IPlataformaRepository _plataformaRepository;

        public EditJogoCommandHandler(IJogoRepository jogoRepository, IGeneroRepository generoRepository, IPlataformaRepository plataformaRepository)
        {
            _jogoRepository = jogoRepository;
            _generoRepository = generoRepository;
            _plataformaRepository = plataformaRepository;
        }

        public async Task<JogoDto> Handle(EditJogoCommand request, CancellationToken cancellationToken)
        {
            var objJogo = await _jogoRepository.GetByIdAsync(request.Id);
            objJogo.Inicializar(request.Titulo, request.Descricao, request.Preco, request.Desconto, request.GeneroId, request.PlataformaId);
            await _jogoRepository.UpdateAsync(objJogo);
            
            var dtoJogo = new JogoDto()
            {
                Id = objJogo.Id,
                Titulo = objJogo.Titulo,
                Descricao = objJogo.Descricao,
                Preco = objJogo.Preco,
                Desconto = objJogo.Desconto,
                GeneroId = objJogo.GeneroId,
                PlataformaId = objJogo.PlataformaId,
                PrecoDesconto = objJogo.CalcularPrecoComDesconto().ToString("N2")
            };

            return dtoJogo;

        }
    }
}
