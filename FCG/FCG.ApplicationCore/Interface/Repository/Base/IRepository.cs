using FCG.Domain.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Interface.Repository.Base
{
    public interface IRepository<T> where T : EntityBase
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entidade);
        void Delete(int id);
        Task DeleteAsync(int id);

    }
}
