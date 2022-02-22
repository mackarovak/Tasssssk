using System;
using Microsoft.EntityFrameworkCore;

namespace Tasssssk
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=relationsdb;Trusted_Connection=True;");
            optionsBuilder.UseInMemoryDatabase("Ksenia");
        }
    }
}
