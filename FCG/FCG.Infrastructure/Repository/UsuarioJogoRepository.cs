// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FCG.Infrastructure.Context;
using FCG.Infrastructure.Repository.Base;

namespace FCG.Infrastructure.Repository
{
    public class UsuarioJogoRepository : EFRepository<UsuarioJogo>, IUsuarioJogoRepository
    {
        public UsuarioJogoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
