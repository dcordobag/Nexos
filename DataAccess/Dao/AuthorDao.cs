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

    public class AuthorDao : ConnectionContext, IAuthorDao
    {
        protected DbContextOptions<ConnectionContext> _options;
        public AuthorDao(DbContextOptions<ConnectionContext> options) : base(options)
        {
            _options = options;
        }

        public int Create(Author data)
        {
            using (var context = new ConnectionContext(_options))
            {
                context.Entry(data).State = EntityState.Added;
                return context.SaveChanges();
            }
        }

        public int Delete(Author data)
        {
            using (var context = new ConnectionContext(_options))
            {
                context.Entry(data).State = EntityState.Deleted;
                return context.SaveChanges();
            }
        }

        public IList<Author> GetList()
        {
            using (var context = new ConnectionContext(_options))
            {
                return context.Author.ToList();
            }
        }

        public IList<Author> GetListFiltered(Expression<Func<Author, bool>> filter)
        {
            using (var context = new ConnectionContext(_options))
            {
                return context.Author.Where(filter).ToList();
            }
        }

        public int Update(Author data)
        {
            using (var context = new ConnectionContext(_options))
            {
                context.Entry(data).State = EntityState.Modified;
                return context.SaveChanges();
            }
        }
    }
}
