// Dependências
using FCG.ApplicationCore.Interface.Repository.Base;
using FCG.Domain.Entities;

namespace FCG.ApplicationCore.Interface.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<bool> VerificarSeExisteUsuarioEmailAsync(string email);
        Task<Usuario?> UsuarioEmailAsync(string email);
        Task<Usuario?> GetUsuarioAsync(int id);

        Task<bool> GetByEmailExistsAsync(int usuarioId, string email);
    }
}
