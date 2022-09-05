using lngCollector.Model;
using lngCollector.Model.UserDt;
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

        public AppDataDb(string path, IUserInfo userInfo) : base()
        {
            _path = path;
            UserInfo = userInfo;
        }

        public IUserInfo UserInfo { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_path}");
            //optionsBuilder.LogTo(Console.WriteLine);
        }

        public DbSet<EWord> EWords { get; set; }
        public DbSet<Lng> Lngs { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<Matrix> Matrixs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserValue> Variables { get; set; }
        public DbSet<Story> Stories { get; set; }
    }
}
