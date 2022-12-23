using ControleFinanceiro.Data.Mappings;
using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Data
{
    public class ControleFinanceiroDataContext : DbContext
    {
        public ControleFinanceiroDataContext(DbContextOptions<ControleFinanceiroDataContext> options) : base(options) { }
        
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransacaoMap());
        }
    }
}
