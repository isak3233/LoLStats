using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLApi.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<LoLAccount> LoLAccounts { get; set; }
        public DbSet<SummonerAccount> SummonerAccounts { get; set; }
        public DbSet<RankedInfo> RankedInfo { get; set; }
        public DbSet<LoLMatch> LoLMatch { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    var config = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.Development.json", optional: true)
        //        .Build();
        //    options.UseSqlServer(config["ConnectionStrings:DefaultConnection"]);
        //}
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
    }
}
