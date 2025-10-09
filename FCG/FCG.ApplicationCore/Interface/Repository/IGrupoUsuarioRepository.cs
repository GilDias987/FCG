using FCG.ApplicationCore.Interface.Repository.Base;
using FCG.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Interface.Repository
{
    public interface IGrupoUsuarioRepository : IRepository<GrupoUsuario>
    {
        public Task<bool> VerificarSeExisteGrupoAsync(string nomeGrupo);
        public Task<IList<GrupoUsuario>> ListarGrupoUsuario();
    }
}
