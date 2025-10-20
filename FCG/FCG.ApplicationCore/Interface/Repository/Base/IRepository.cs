using FCG.Domain.Entities;
namespace FCG.ApplicationCore.Interface.Repository.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        bool GetByIdExists(int id);
        Task<bool> GetByIdExistsAsync(int id);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entidade);
        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
