using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.GetGrupoUsuario
{
    public class GetAllGrupoUsuarioQuery : IRequest<List<GrupoUsuarioDto>>
    {
    }
}



