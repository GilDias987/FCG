using FCG.ApplicationCore.Repository;
using FCG.Domain.Entity;
using FCG.Infrastructure.Contexto;
using FCG.Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Infrastructure.Repository
{
    public class UsuarioJogoRepository : EFRepository<UsuarioJogo>, IUsuarioJogoRepository
    {
        public UsuarioJogoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
