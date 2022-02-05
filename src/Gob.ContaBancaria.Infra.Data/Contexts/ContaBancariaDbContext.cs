using Gob.ContaBancaria.Domain.Models;
using Gob.ContaBancaria.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Gob.ContaBancaria.Infra.Data.Contexts
{
    public class ContaBancariaDbContext : DbContext
    {
        public ContaBancariaDbContext(DbContextOptions<ContaBancariaDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Conta> Contas { get; set; }
        public DbSet<Lancamento> Lancamentos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<string>()
                .AreUnicode(false)
                .HaveMaxLength(1024);

            configurationBuilder
                .Properties<decimal>()
                .HavePrecision(18, 2);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContaMap());
            modelBuilder.ApplyConfiguration(new LancamentoMap());
            modelBuilder.ApplyConfiguration(new PessoaMap());
        }
    }
}
