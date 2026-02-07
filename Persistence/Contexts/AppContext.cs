using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Persistence.Entidades;

namespace Persistence.Contexts
{
    public class AppContext
    {
        public class AppContext : DbContext
        {
            public AppContext(DbContextOptions<AppContext> options) : base(options) { }

            public DbSet<Country> Countries { get; set; }
            public DbSet<CountryIndicator> CountryIndicators { get; set; }
            public DbSet<MacroIndicator> MacroIndicators { get; set; }
            public DbSet<SimulationIndicator> SimulationIndicators { get; set; }
            public DbSet<AppSettings>  AppSettings { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            }
        }
}
