// Dependências
using FCG.ApplicationCore.Interface.Repository.Base;
using FCG.Domain.Entities;

namespace FCG.ApplicationCore.Interface.Repository
{
    public interface IPlataformaRepository : IRepository<Plataforma>
    {
        /// <summary>
        /// GetPlataforma
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Plataforma?> GetPlataformaIdAsync(int id);
    }
}
