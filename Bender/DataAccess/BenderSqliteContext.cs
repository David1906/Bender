using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bender.DataAccess
{
    public class BenderContext : DbContext
    {
        static private string DB_NAME = "dbSqlite.db";

        public DbSet<LabelDAO> Labels { get; set; }
        public DbSet<LabelItemDAO> LabelItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                connectionString: "Filename=" + DB_NAME,
                sqliteOptionsAction: op =>
                {
                    op.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            base.OnConfiguring(optionsBuilder);
        }

        public void Initialize()
        {
            if (this.Database.GetPendingMigrations().Count() > 0)
            {
                new BenderContext().Database.Migrate();
            }
            var labelDAO = new LabelDAO(this);
            if (labelDAO.IsTableEmpty())
            {
                labelDAO.Clear();
                labelDAO.Seed();
            }
        }
    }
}
