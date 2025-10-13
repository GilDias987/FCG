using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entity;
using FCG.Domain.ValueObjects;
using FCG.Infrastructure.Contexto;
using FCG.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
