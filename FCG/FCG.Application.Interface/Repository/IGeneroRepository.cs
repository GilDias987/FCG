using FCG.ApplicationCore.Interface.Repository.Base;
using FCG.Domain.Entities;

namespace FCG.ApplicationCore.Interface.Repository
{
    public interface IGeneroRepository : IRepository<Genero>
    {
        /// <summary>
        /// GetGenero
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Genero?> GetGeneroIdAsync(int id);
    }
}
