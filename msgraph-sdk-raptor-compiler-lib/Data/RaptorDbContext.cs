using Microsoft.EntityFrameworkCore;
using MsGraphSDKSnippetsCompiler.Models;

namespace MsGraphSDKSnippetsCompiler.Data
{
    public class RaptorDbContext : DbContext
    {
        private readonly string _connectionString;

        public RaptorDbContext(string connectionString)
          : base()
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<CompileCycle> CompileCycle { get; set; }
        public DbSet<CompileResult> CompileResult { get; set; }
        public DbSet<CompileResultsError> CompileResultsError { get; set; }
        public DbSet<CompileResultsView> CompileResultsView { get; set; }
    }
}