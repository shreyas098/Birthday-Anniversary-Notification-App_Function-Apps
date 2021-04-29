using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSendingFunctionApp.Database
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Associates> Associates { get; set; }
        public DbSet<Associate_Birthday_Wishes_Inputs> Associate_Birthday_Wishes_Inputs { get; set; }
        public DbSet<AppSettings> AppSettings { get; set; }
    }
}
