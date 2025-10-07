using FCG.ApplicationCore.Dto.Autenticacao.GrupoUsuario;
using FCG.ApplicationCore.Interface.Repository;
using FCG.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Task CadastrarAsync(AddGrupoUsuarioDto addGrupoUsuarioDto)
        {
            throw new NotImplementedException();
        }

        public Task EditarAsync(int id, AddGrupoUsuarioDto addGrupoUsuarioDto)
        {
            throw new NotImplementedException();
        }

        public Task ExcluirAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
