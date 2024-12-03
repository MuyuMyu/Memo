using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Memo.Context
{
    public class MemoContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 配置 SQLite 数据库连接字符串
            //optionsBuilder.UseSqlite("Data Source=memo.db");
            optionsBuilder
            .UseSqlite("Data Source=memo.db")
            .LogTo(Console.WriteLine, LogLevel.Information);

        }


        public DbSet<ToDo> ToDo { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Memo> Memo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // 确保主键配置正确
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Account)
                .IsUnique(); // 如果 `Account` 需要唯一
        }

    }
}
