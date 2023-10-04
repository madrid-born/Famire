using Microsoft.EntityFrameworkCore;
using Music_Player.Models;

namespace Music_Player.Methods ;

    public class AppDbContext : DbContext
    {
        public DbSet<Song> NextOns { get; set; }
        //public DbSet<Song> Queue { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the database connection string for Android
            //string dbPath = Path.Combine(FileSystem.AppDataDirectory, "database.db3");
            //optionsBuilder.UseSqlite($"Filename={dbPath}");
            
            // Configure the SQLite database connection string
            string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "myapp.db");
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
    