using FCG.ApplicationCore.Dto.Autenticacao.GrupoUsuario;
using FCG.ApplicationCore.Interface.Repository;
using FCG.ApplicationCore.Interface.Service;
using FCG.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Service
{
    public class GrupoUsuarioService : IGrupoUsuarioService
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;
        public GrupoUsuarioService(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        public async Task EditarAsync(int id, AddGrupoUsuarioDto addGrupoUsuarioDto)
        {
            try
            {
                var grupoUsuario = await _grupoUsuarioRepository.GetByIdAsync(id);

                if (grupoUsuario == null)
                    throw new ArgumentException("Grupo de usuário não encontrado.");

                if (grupoUsuario.Nome != addGrupoUsuarioDto.Nome)
                {
                    //if (VerificarGrupoUsuarioExistente(addGrupoUsuarioDto.Nome))
                    //    throw new ArgumentException("Já existe um grupo de usuário com esse nome.");

                    grupoUsuario.Inicializar(addGrupoUsuarioDto.Nome);

                    await _grupoUsuarioRepository.UpdateAsync(grupoUsuario);
                }

            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error ao editar grupo de usuário");
            }
        }

    }
}
