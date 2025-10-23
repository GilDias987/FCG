// Dependências
using FCG.ApplicationCore.Dto.Jogo;
using FCG.ApplicationCore.Interface.Repository;
using MediatR;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.AddJogo
{
    public class AddJogoCommandHandler : IRequestHandler<AddJogoCommand, JogoDto>
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IPlataformaRepository _plataformaRepository;

        public AddJogoCommandHandler(IJogoRepository jogoRepository, IGeneroRepository generoRepository, IPlataformaRepository plataformaRepository)
        {
            _jogoRepository = jogoRepository;
            _generoRepository = generoRepository;
            _plataformaRepository = plataformaRepository;
        }

        public async Task<JogoDto> Handle(AddJogoCommand request, CancellationToken cancellationToken)
        {
            // Usando FluentValidation
            var repGenero = await _generoRepository.GetByIdExistsAsync(request.GeneroId);
            if (repGenero is false)
                throw new ArgumentException("O Gênero do jogo não foi encontrado.");

            // Usando FluentValidation
            var repPlataforma = await _plataformaRepository.GetByIdExistsAsync(request.PlataformaId);
            if (repPlataforma is false)
                throw new ArgumentException("A Plataforma do jogo não foi encontrado.");

            try
            {
                var objJogo = await _jogoRepository.AddAsync(new Domain.Entities.Jogo(request.Titulo, request.Descricao, request.Preco, request.Desconto, request.GeneroId, request.PlataformaId));
                var dtoJogo = new JogoDto()
                {
                    Id = objJogo.Id, Titulo = objJogo.Titulo, Descricao = objJogo.Descricao, Preco = objJogo.Preco, Desconto = objJogo.Desconto, GeneroId = objJogo.GeneroId, PlataformaId = objJogo.PlataformaId 
                };

                return dtoJogo;
            }
            catch (Exception)
            {
                throw new Exception("Ao cadastrar o jogo ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}