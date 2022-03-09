
namespace DataAccess.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepositoryBase<T> where T : class
    {
        IList<T> GetList();
        IList<T> GetListFiltered(Expression<Func<T, bool>> filter);
        int Create(T data);
        int Update(T data);
        int Delete(T data);
    }
}
