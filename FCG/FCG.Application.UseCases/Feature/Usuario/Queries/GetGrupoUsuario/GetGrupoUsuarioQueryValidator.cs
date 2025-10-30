using FluentValidation;

// Dependências
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.GetGrupoUsuario
{
    public sealed class GetGrupoUsuarioQueryValidator
    : AbstractValidator<GetGrupoUsuarioQuery>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;
        public GetGrupoUsuarioQueryValidator(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;

            RuleFor(x => x.Id)
              .GreaterThan(0)
              .WithMessage("O id deve ser maior que zero.")
              .MustAsync(async (Id, cancellation) => (await _grupoUsuarioRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id informado não foi encontrado.");

        }
    }
}
