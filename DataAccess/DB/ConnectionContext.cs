
namespace DataAccess.DB
{
    using DataAccess.Dto;
    using Microsoft.EntityFrameworkCore;
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Editorial>().ToTable("Editorial");
        }

        //Tablas de datos
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Editorial> Editorial { get; set; }
    }
}
