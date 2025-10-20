using Microsoft.EntityFrameworkCore;

// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FCG.Infrastructure.Context;
using FCG.Infrastructure.Repository.Base;

namespace FCG.Infrastructure.Repository
{
    public class JogoRepository : EFRepository<Jogo>, IJogoRepository
    {
        /// <summary>
        /// Jogo
        /// </summary>
        /// <param name="context"></param>
        public JogoRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// GetJogo \ Genero \ Plataforma
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Jogo?> GetJogoIdAsync(int id)
        {
            return await _dbSet
                .Include(i => i.Genero)
                .Include(i => i.Plataforma)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
