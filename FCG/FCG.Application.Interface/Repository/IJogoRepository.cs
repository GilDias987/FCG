// Deependências
using FCG.ApplicationCore.Interface.Repository.Base;
using FCG.Domain.Entities;

namespace FCG.ApplicationCore.Interface.Repository
{
    public interface IJogoRepository: IRepository<Jogo>
    {
        /// <summary>
        /// GetJogoIdAsync \ Gênero \ Plataforma
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Jogo?> GetJogoIdAsync(int id);
    }
}
