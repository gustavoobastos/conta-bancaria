using Gob.ContaBancaria.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gob.ContaBancaria.Infra.Data.Mappings
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable(nameof(Conta));
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Titular)
                   .WithMany(x => x.Contas)
                   .HasForeignKey(x => x.IdTitular);
        }
    }
}
