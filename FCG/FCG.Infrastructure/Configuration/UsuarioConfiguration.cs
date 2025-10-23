using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// Dependências
using FCG.Domain.Entities;

namespace FCG.Infrastructure.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TB_USUARIO");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").HasColumnName("ISN_USUARIO").UseIdentityColumn();
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(1000)").HasColumnName("DSC_NOME").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").HasColumnName("DTH_CRIACAO").IsRequired();
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME").HasColumnName("DTH_ATUALIZACAO").IsRequired();
            builder.Property(p => p.GrupoUsuarioId).HasColumnType("INT").HasColumnName("ISN_GRUPO");
            
            #region Value Object
         
            builder.OwnsOne(p => p.Email, nbuilder =>
            {
                 nbuilder.Property(p => p.Endereco).HasColumnType("VARCHAR(500)").HasColumnName("DSC_EMAIL").IsRequired();
                 nbuilder.HasIndex(p => p.Endereco).IsUnique();
            });

            builder.OwnsOne(p => p.Senha, nbuilder =>
            {
                nbuilder.Property(p => p.Valor).HasColumnType("VARCHAR(500)").HasColumnName("DSC_SENHA").IsRequired();
            });


            #endregion


            builder.HasOne(p => p.GrupoUsuario)
                   .WithMany(p => p.Usuarios)
                   .HasPrincipalKey(p => p.Id).IsRequired(true);
        }
    }
}
