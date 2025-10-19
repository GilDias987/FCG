// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FCG.Infrastructure.Context;
using FCG.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Repository
{
    public class PlataformaRepository : EFRepository<Plataforma>, IPlataformaRepository
    {
        public PlataformaRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// GetPlataforma
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Plataforma?> GetPlataformaIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
