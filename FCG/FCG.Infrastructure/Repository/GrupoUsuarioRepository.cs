using FCG.ApplicationCore.Interface.Repository;
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
    public class GrupoUsuarioRepository : EFRepository<GrupoUsuario>, IGrupoUsuarioRepository
    {
        public GrupoUsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public bool VerificarSeExisteGrupo(string nomeGrupo)
        {
            var grupo = _dbSet.FirstOrDefault(g => g.Nome.ToLower() == nomeGrupo.ToLower());
            return grupo != null ? true : false;
        }
    }
}
