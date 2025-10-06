using FCG.ApplicationCore.Repository.Base;
using FCG.Domain.Entity.Abstract;
using FCG.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FCG.Infrastructure.Repository.Base
{
    public class EFRepository<T> : IRepository<T> where T : EntityBase
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T GetById(int id) => 
             _dbSet.Find(id);

        public async Task<T> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        public T Add(T entidade)
        {
            entidade.DataCriacao = DateTime.Now;
            entidade.DataAtualizacao = DateTime.Now;
            _dbSet.Add(entidade);
            _context.SaveChanges();
            return entidade;
        }

        public async Task<T> AddAsync(T entidade)
        {
            entidade.DataCriacao = DateTime.Now;
            entidade.DataAtualizacao = DateTime.Now;
            _dbSet.Add(entidade);
            await _context.SaveChangesAsync();
            return entidade;
        }

        public void Delete(int id)
        {
            _dbSet.Remove(GetById(id));
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            _dbSet.Remove(GetById(id));
            await _context.SaveChangesAsync();
        }

        public void Update(T entidade)
        {
            entidade.DataAtualizacao = DateTime.Now;
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(T entidade)
        {
            entidade.DataAtualizacao = DateTime.Now;
            _dbSet.Update(entidade);
            await _context.SaveChangesAsync();
        }
    }
}
