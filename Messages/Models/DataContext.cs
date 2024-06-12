using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Messages.Models
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite("Data Source=.\\messages.db");
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}