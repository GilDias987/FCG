using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// Dependências
using FCG.Domain.Entities;

namespace FCG.Infrastructure.Configuration
{
    public class PlataformaConfiguration : IEntityTypeConfiguration<Plataforma>
    {
        public void Configure(EntityTypeBuilder<Plataforma> builder)
        {
            builder.ToTable("TB_PLATAFORMA");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).HasColumnType("INT").HasColumnName("ISN_PLATAFORMA").UseIdentityColumn();
            builder.Property(g => g.Titulo).HasColumnType("VARCHAR(500)").HasColumnName("DSC_TITULO").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").HasColumnName("DTH_CRIACAO").IsRequired();
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME").HasColumnName("DTH_ATUALIZACAO").IsRequired();
        }
    }
}
