using Microsoft.EntityFrameworkCore;

// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FCG.Infrastructure.Context;
using FCG.Infrastructure.Repository.Base;

namespace FCG.Infrastructure.Repository
{
    public class GrupoUsuarioRepository : EFRepository<GrupoUsuario>, IGrupoUsuarioRepository
    {
        public GrupoUsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Checar se o item existe
        /// </summary>
        /// <param name="Nome"></param>
        /// <returns></returns>
        public async Task<bool> ExistePorNomeAsync(string nome)
        {
            return await _dbSet.AnyAsync(a => a.Nome.Trim().ToLower() == nome.Trim().ToLower());
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
