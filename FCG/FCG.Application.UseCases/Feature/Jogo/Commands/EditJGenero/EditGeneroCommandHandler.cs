using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.EditJGenero
{
    public class EditGeneroCommandHandler : IRequestHandler<EditGeneroCommand, GeneroDto>
    {
        private readonly IGeneroRepository _generoRepository;

        public EditGeneroCommandHandler(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<GeneroDto> Handle(EditGeneroCommand request, CancellationToken cancellationToken)
        {
            var objGenero  = await _generoRepository.GetByIdAsync(request.Id);
            if (objGenero != null)
            {
                try
                {
                    await _generoRepository.UpdateAsync(objGenero);

                    return new GeneroDto() { Id = objGenero.Id, Titulo = objGenero.Titulo};
                }
                catch (Exception)
                {
                    throw new Exception("Ao alterar o gênero ocorreu uma falha inesperada. Tente novamente mais tarde.");
                }
            }
            else
            {
                throw new ArgumentException("Gênero não foi encontrado.");
            }
        }
    }
}
