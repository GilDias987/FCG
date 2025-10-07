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
    public class GrupoUsuarioService : IGrupoUsuarioService
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;
        public GrupoUsuarioService(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }
        public async Task CadastrarAsync(AddGrupoUsuarioDto addGrupoUsuarioDto)
        {
            try
            {
                if (_grupoUsuarioRepository.VerificarSeExisteGrupo(addGrupoUsuarioDto.Nome))
                    throw new Exception("Já existe um grupo com esse nome.");

                var grupoUsuario = await _grupoUsuarioRepository.AddAsync(new Domain.Entity.GrupoUsuario
                {
                    Nome = addGrupoUsuarioDto.Nome
                });

            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao cadastrar grupo de usuário. " + ex.Message);
            }
        }

        public async Task EditarAsync(int id, AddGrupoUsuarioDto addGrupoUsuarioDto)
        {
            try
            {
                var grupoUsuario = await _grupoUsuarioRepository.GetByIdAsync(id);

               
                if (grupoUsuario == null)
                    throw new Exception("Grupo de usuário não encontrado.");

                grupoUsuario.Nome = addGrupoUsuarioDto.Nome;

                await _grupoUsuarioRepository.UpdateAsync(grupoUsuario);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar grupo de usuário. " + ex.Message);
            }
        }

        public async Task ExcluirAsync(int id)
        {
            try
            {
                var grupoUsuario = await _grupoUsuarioRepository.GetByIdAsync(id);

                if (grupoUsuario == null)
                    throw new Exception("Grupo de usuário não encontrado para exclusão.");

                await _grupoUsuarioRepository.DeleteAsync(id);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir grupo de usuário. " + ex.Message);
            }
        }
    }
}
