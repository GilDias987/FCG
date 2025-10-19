// Deependências
using FCG.ApplicationCore.Interface.Repository.Base;
using FCG.Domain.Entities;

namespace FCG.ApplicationCore.Interface.Repository
{
    public interface IJogoRepository: IRepository<Jogo>
    {
        /// <summary>
        /// GetJogo \ Genero \ Plataforma
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Jogo?> GetJogoIdAsync(int id);
    }
}
