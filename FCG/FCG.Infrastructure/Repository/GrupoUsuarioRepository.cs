using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entity;
using FCG.Infrastructure.Contexto;
using FCG.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> VerificarSeExisteGrupoAsync(string nomeGrupo)
        {
            var grupo = await _dbSet.FirstOrDefaultAsync(g => g.Nome.ToLower() == nomeGrupo.ToLower());
            return grupo != null ? true : false;
        }

        public async Task<IList<GrupoUsuario>> ListarGrupoUsuario()
        {
            return await _dbSet.OrderBy(x => x.Nome).ToListAsync();
        
        }
    }
}
