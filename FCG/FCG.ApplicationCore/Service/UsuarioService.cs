using FCG.ApplicationCore.Dto.Autenticacao.GrupoUsuario;
using FCG.ApplicationCore.Dto.Autenticacao.Usuario;
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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task CadastrarAsync(AddUsuarioDto addUsuarioDto)
        {
            try
            {
                if (_usuarioRepository.VerificarSeExisteUsuarioEmail(addUsuarioDto.Email))
                    throw new Exception("Já existe um usuário com esse e-mail.");

                var usuario = new Usuario(addUsuarioDto.Nome, addUsuarioDto.Email, addUsuarioDto.Senha, addUsuarioDto.GrupoUsuarioId);

                var Usuario = await _usuarioRepository.AddAsync(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar usuário. " + ex.Message);
            }
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
