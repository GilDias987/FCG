using FCG.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FCG.Infrastructure.Contexto
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region DbSet

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<GrupoUsuario> GrupoUsuario { get; set; }
        
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Plataforma> Plataforma { get; set; }
        public DbSet<Jogo> Jogo { get; set; }
        public DbSet<UsuarioJogo> UsuarioJogo { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, builder => builder.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

    }
}
