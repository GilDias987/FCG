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
    internal class JogoConfiguration : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.ToTable("TB_JOGO");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).HasColumnType("INT").ValueGeneratedNever().HasColumnName("ISN_JOGO").UseIdentityColumn();
            builder.Property(g => g.Titulo).HasColumnType("VARCHAR(500)").HasColumnName("DSC_TITULO").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").HasColumnName("DTH_CRIACAO").IsRequired();
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME").HasColumnName("DTH_ATUALIZACAO").IsRequired();
            builder.Property(P => P.Preco).HasColumnType("DECIMAL(18,2)").HasColumnName("VLR_PRECO");
            builder.Property(P => P.Desconto).HasColumnType("DECIMAL(3,2)").HasColumnName("VLR_DESCONTO");
            builder.Property(p => p.PlataformaId).HasColumnType("INT").HasColumnName("ISN_PLATAFORMA").IsRequired();
            builder.Property(p => p.GeneroId).HasColumnType("INT").HasColumnName("ISN_GENERO").IsRequired();

            builder.HasOne(p => p.Plataforma)
               .WithMany(p => p.Jogos)
               .HasPrincipalKey(p => p.Id).IsRequired(true);

            builder.HasOne(p => p.Genero)
               .WithMany(p => p.Jogos)
               .HasPrincipalKey(p => p.Id).IsRequired(true);
        }
    }
}
