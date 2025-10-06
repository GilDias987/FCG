using FCG.ApplicationCore.Repository;
using FCG.Domain.Entity;
using FCG.Infrastructure.Contexto;
using FCG.Infrastructure.Repository.Base;

namespace FCG.Infrastructure.Repository
{
    public class GeneroRepository : EFRepository<Genero>, IGeneroRepository
    {
        public GeneroRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
