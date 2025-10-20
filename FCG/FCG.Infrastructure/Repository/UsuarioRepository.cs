using Microsoft.EntityFrameworkCore;

// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FCG.Infrastructure.Context;
using FCG.Infrastructure.Repository.Base;

namespace FCG.Infrastructure.Repository
{
    public class UsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> VerificarSeExisteUsuarioEmailAsync(string email)
        {
            var usuario = await _dbSet.FirstOrDefaultAsync(g => g.Email.Endereco.ToLower() == email.ToLower());
            return usuario != null ? true : false;
        }

        public async Task<bool> GetByEmailExistsAsync(int usuarioId, string email)
        {
            return await _dbSet
                .Include(i => i.GrupoUsuario)
               .AnyAsync(a => a.Id != usuarioId && a.Email.ToLower() == email.ToLower());
        }

        public async Task<Usuario?> UsuarioEmailAsync(string email)
        {
            return await _dbSet.Include(x => x.GrupoUsuario).FirstOrDefaultAsync(g => g.Email.Endereco.ToLower() == email.ToLower());
        }

        public async Task<Usuario?> GetUsuarioAsync(int id)
        {
            return await _dbSet.Include(x => x.GrupoUsuario).FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
