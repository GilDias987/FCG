using FCG.ApplicationCore.Interface.Repository.Base;
using FCG.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Interface.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<bool> VerificarSeExisteUsuarioEmailAsync(string email);
        Task<Usuario?> UsuarioEmailAsync(string email);
        Task<Usuario?> GetUsuarioAsync(int id);
    }
}
