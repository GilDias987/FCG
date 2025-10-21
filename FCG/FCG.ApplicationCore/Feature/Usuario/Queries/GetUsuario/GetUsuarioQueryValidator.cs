using FluentValidation;

// Dependências
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.ApplicationCore.Feature.Usuario.Queries.GetUsuario
{
    public sealed class GetUsuarioQueryValidator: AbstractValidator<GetUsuarioQuery>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public GetUsuarioQueryValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Informe o id do usuário.")
              .GreaterThan(0)
              .WithMessage("O id deve ser maior que zero.")
              .MustAsync(async (Id, cancellation) => (await _usuarioRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id informado não foi encontrado.");
        }
    }
}
