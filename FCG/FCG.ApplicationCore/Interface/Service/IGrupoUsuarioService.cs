using FCG.ApplicationCore.Dto.Autenticacao.GrupoUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Interface.Service
{
    public interface IGrupoUsuarioService
    {
        public Task EditarAsync(int id, AddGrupoUsuarioDto addGrupoUsuarioDto);
    }
}
