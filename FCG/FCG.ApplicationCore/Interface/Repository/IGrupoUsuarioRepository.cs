// Dependências
using FCG.ApplicationCore.Interface.Repository.Base;
using FCG.Domain.Entities;

namespace FCG.ApplicationCore.Interface.Repository
{
    public interface IGrupoUsuarioRepository : IRepository<GrupoUsuario>
    {
        /// <summary>
        /// Checar se o item existe
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        Task<bool> ExistePorNomeAsync(string nome);


        public Task<bool> VerificarSeExisteGrupoAsync(string nomeGrupo);
        public Task<IList<GrupoUsuario>> ListarGrupoUsuario();
    }
}
