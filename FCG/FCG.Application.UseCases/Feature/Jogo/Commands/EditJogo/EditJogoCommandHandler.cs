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
            if (objJogo != null)
            {
                // Usando FluentValidation
                var repGenero = await _generoRepository.GetByIdExistsAsync(request.GeneroId);
                if (repGenero is false)
                    throw new ArgumentException("O Gênero do jogo não foi encontrado.");

                // Usando FluentValidation
                var repPlataforma = await _plataformaRepository.GetByIdExistsAsync(request.GeneroId);
                if (repPlataforma is false)
                    throw new ArgumentException("A Plataforma do jogo não foi encontrado.");

                try
                {
                    await _jogoRepository.UpdateAsync(objJogo);
                    var dtoJogo = new JogoDto()
                    {
                        Id = objJogo.Id, Titulo = objJogo.Titulo, Descricao = objJogo.Descricao, Preco = objJogo.Preco, Desconto = objJogo.Desconto, GeneroId = objJogo.GeneroId, PlataformaId = objJogo.PlataformaId 
                    };

                    return dtoJogo;
                }
                catch (Exception)
                {
                    throw new Exception("Ao alterar o jogo ocorreu uma falha inesperada. Tente novamente mais tarde.");
                }
            }
            else
            {
                throw new ArgumentException("Jogo não foi encontrado.");
            }
        }
    }
}
