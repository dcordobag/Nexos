namespace DataAccess.Dao
{
    using DataAccess.DB;
    using DataAccess.Dto;
    using DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class BookDao : IBookDao
    {
        protected DbContextOptions<ConnectionContext> _options;
        public BookDao(DbContextOptions<ConnectionContext> options)
        {
            _options = options;
        }

        public int Create(Book data)
        {
            using (var context = new ConnectionContext(_options))
            {
                context.Entry(data).State = EntityState.Added;
                return context.SaveChanges();
            }
        }

        public int Delete(Book data)
        {
            using (var context = new ConnectionContext(_options))
            {
                context.Entry(data).State = EntityState.Deleted;
                return context.SaveChanges();
            }
        }

        public IList<Book> GetList()
        {
            using (var context = new ConnectionContext(_options))
            {
                return context.Book.Include(b => b.Author).Include(b => b.Editorial).ToList();
            }
        }

        public IList<Book> GetListFiltered(Expression<Func<Book, bool>> filter)
        {
            using (var context = new ConnectionContext(_options))
            {
                return context.Book.Include(b => b.Author).Include(b => b.Editorial).Where(filter).ToList();
            }
        }

        public int Update(Book data)
        {
            using (var context = new ConnectionContext(_options))
            {
                context.Entry(data).State = EntityState.Modified;
                return context.SaveChanges();
            }
        }
    }
}
