// Dependências
using FCG.ApplicationCore.Dto.Autenticacao.Usuario;
using FCG.ApplicationCore.Interface.Repository;
using FCG.ApplicationCore.Interface.Service;
using FCG.Domain.Entities;

namespace FCG.ApplicationCore.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;
        public UsuarioService(IGrupoUsuarioRepository grupoUsuarioRepository, IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        public async Task<UsuarioDto> LoginUsuario(LoginUsuarioDto loginUsuarioDto)
        {
            try
            {
                var usuarioEmail = await _usuarioRepository.UsuarioEmailAsync(loginUsuarioDto.Email.Trim());

                if (usuarioEmail == null)
                    throw new ArgumentException("Email não cadastrado na aplicação.");

                var senhaValida = usuarioEmail.ValidarSenha(loginUsuarioDto.Senha.Trim(), usuarioEmail.Senha);

                if (!senhaValida)
                    throw new ArgumentException("E-mail ou senha inválidos.");

                return new UsuarioDto { Email = usuarioEmail.Email, Grupo = usuarioEmail.GrupoUsuario.Nome };
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao efetuar login do usuário.");
            }
        }

        public async Task CadastrarAsync(AddUsuarioDto addUsuarioDto)
        {
            try
            {
                var grupoUsuario = await _grupoUsuarioRepository.GetByIdAsync(addUsuarioDto.GrupoUsuarioId);               
                if (grupoUsuario == null)
                    throw new ArgumentException("Grupo de usuário não encontrado.");

                var usuarioEmail = await _usuarioRepository.VerificarSeExisteUsuarioEmailAsync(addUsuarioDto.Email);               
                if (usuarioEmail)
                    throw new ArgumentException("Existe um usuário com esse e-mail.");

                var usuario = new Usuario(addUsuarioDto.Nome, addUsuarioDto.Email, addUsuarioDto.Senha, addUsuarioDto.GrupoUsuarioId);

                await _usuarioRepository.AddAsync(usuario);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception("Error ao cadastar usuário");
            }
        }

        public async Task EditarAsync(int id, AddUsuarioDto addUsuarioDto)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                var grupoUsuario = await _grupoUsuarioRepository.GetByIdAsync(addUsuarioDto.GrupoUsuarioId);

                if (grupoUsuario == null)
                    throw new ArgumentException("Grupo de usuário não encontrado.");

                if (usuario.Email.Trim() != addUsuarioDto.Email.Trim())
                {
                    var usuarioEmail = await _usuarioRepository.VerificarSeExisteUsuarioEmailAsync(addUsuarioDto.Email);

                    if (usuarioEmail)
                        throw new ArgumentException("Existe um usuário com esse e-mail.");
                }

                usuario.Inicializar(addUsuarioDto.Nome, addUsuarioDto.Email, addUsuarioDto.Senha, addUsuarioDto.GrupoUsuarioId);

                await _usuarioRepository.AddAsync(usuario);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error ao cadastar usuário");
            }
        }

        public Task ExcluirAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
