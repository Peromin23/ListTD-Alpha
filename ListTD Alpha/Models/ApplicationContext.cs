using Microsoft.EntityFrameworkCore;

namespace ListTD_Alpha.Models
{
    /// <summary>
    /// Контекст для ToDo List API
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// База данных контекста
        /// </summary>
        /// 
        public DbSet<UserTask> UserTasks { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Username=postgres;Database=postgres;Password=123456");

        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
        }
    }
}
