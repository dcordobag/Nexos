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

    public class EditorialDao : ConnectionContext, IEditorialDao
    {
        protected DbContextOptions<ConnectionContext> _options;
        public EditorialDao(DbContextOptions<ConnectionContext> options) : base(options)
        {
            _options = options;
        }

        public int Create(Editorial data)
        {
            using (var context = new ConnectionContext(_options))
            {
                context.Entry(data).State = EntityState.Added;
                return context.SaveChanges();
            }
        }

        public int Delete(Editorial data)
        {
            using (var context = new ConnectionContext(_options))
            {
                context.Entry(data).State = EntityState.Deleted;
                return context.SaveChanges();
            }
        }

        public IList<Editorial> GetList()
        {
            using (var context = new ConnectionContext(_options))
            {
                return context.Editorial.ToList();
            }
        }

        public IList<Editorial> GetListFiltered(Expression<Func<Editorial, bool>> filter)
        {
            using (var context = new ConnectionContext(_options))
            {
                return context.Editorial.Where(filter).ToList();
            }
        }

        public int Update(Editorial data)
        {
            using (var context = new ConnectionContext(_options))
            {
                context.Entry(data).State = EntityState.Modified;
                return context.SaveChanges();
            }
        }
    }
}
