using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entity;
using FCG.Domain.ValueObjects;
using FCG.Infrastructure.Contexto;
using FCG.Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Infrastructure.Repository
{
    public class UsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public bool VerificarSeExisteUsuarioEmail(string email)
        {
            var grupo = _dbSet.FirstOrDefault(g => g.Email.ToLower() == email.ToLower());
            return grupo != null ? true : false;
        }
    }
}
