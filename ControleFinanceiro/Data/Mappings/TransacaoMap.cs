using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace ControleFinanceiro.Data.Mappings
{
    public class TransacaoMap : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .IsRequired()              
                .HasMaxLength(180);

            builder.Property(x => x.Valor)
                .IsRequired();

            builder.Property(x => x.Tipo)
                .IsRequired();

            builder.Property(x => x.DataCriacao)
                .HasDefaultValue(DateTime.Now.ToUniversalTime())
                .IsRequired();

            builder.Property(x => x.DataAtualizacao)
                .HasDefaultValue(DateTime.Now.ToUniversalTime())
                .IsRequired();
        }
    }
}
