using Microsoft.EntityFrameworkCore;
using Group_Quiz.Models;
namespace Group_Quiz.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions options) : base (options) { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
        .Property(q => q.RowVersion)
        .IsConcurrencyToken()
        .ValueGeneratedOnAddOrUpdate();
        }
    }
}
