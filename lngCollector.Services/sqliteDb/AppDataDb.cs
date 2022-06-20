using lngCollector.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services.sqliteDb
{
    public class AppDataDb : DbContext
    {
        string _path;

        public AppDataDb(string path): base()
        {
            _path = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_path}");
            //optionsBuilder.LogTo(Console.WriteLine);
        }

        public DbSet<EWord> EWords { get; set; }
        public DbSet<Lng> Lngs { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<Matrix> Matrixs { get; set; }
        //public DbSet<User>
    }
}
