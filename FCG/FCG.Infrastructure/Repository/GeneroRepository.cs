// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FCG.Infrastructure.Context;
using FCG.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Repository
{
    public class GeneroRepository : EFRepository<Genero>, IGeneroRepository
    {
        public GeneroRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// GetGenero
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Genero?> GetGeneroIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
