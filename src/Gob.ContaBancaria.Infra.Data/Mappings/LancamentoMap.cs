using Gob.ContaBancaria.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gob.ContaBancaria.Infra.Data.Mappings
{
    public class LancamentoMap : IEntityTypeConfiguration<Lancamento>
    {
        public void Configure(EntityTypeBuilder<Lancamento> builder)
        {
            builder.ToTable(nameof(Lancamento));
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Conta)
                   .WithMany(x => x.Lancamentos)
                   .HasForeignKey(x => x.IdConta);

            builder.HasOne(x => x.ContaOrigem)
                   .WithMany()
                   .HasForeignKey(x => x.IdContaOrigem)
                   .IsRequired(false);

            builder.HasIndex(x => x.TipoLancamento);
        }
    }
}
