using FCG.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Infrastructure.Configuration
{
    public class UsuarioJogoConfiguration : IEntityTypeConfiguration<UsuarioJogo>
    {
        public void Configure(EntityTypeBuilder<UsuarioJogo> builder)
        {
            builder.ToTable("TB_USUARIO_JOGO");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().HasColumnName("ISN_USUARIO_JOGO").UseIdentityColumn();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").HasColumnName("DTH_CRIACAO").IsRequired();
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME").HasColumnName("DTH_ATUALIZACAO").IsRequired();
            builder.Property(p => p.JogoId).HasColumnType("INT").HasColumnName("ISN_JOGO").IsRequired();
            builder.Property(p => p.UsuarioId).HasColumnType("INT").HasColumnName("ISN_USUARIO").IsRequired();

            builder.HasOne(p => p.Jogo)
                   .WithMany(p => p.UsuarioJogos)
                   .HasPrincipalKey(p => p.Id).IsRequired(true);

            builder.HasOne(p => p.Usuario)
                   .WithMany(p => p.UsuarioJogos)
                   .HasPrincipalKey(p => p.Id).IsRequired(true);
        }
    }
}
