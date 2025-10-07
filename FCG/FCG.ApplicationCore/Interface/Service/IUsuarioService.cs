using FCG.ApplicationCore.Dto.Autenticacao.GrupoUsuario;
using FCG.ApplicationCore.Dto.Autenticacao.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Interface.Service
{
    public interface IUsuarioService
    {
        public Task CadastrarAsync(AddUsuarioDto addUsuarioDto);
        public Task EditarAsync(int id, AddGrupoUsuarioDto addGrupoUsuarioDto);
        public Task ExcluirAsync(int id);
    }
}
