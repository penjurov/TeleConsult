namespace TeleConsult.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using TeleConsult.Contracts;

    public interface IRepository<T> where T : class, IDeletableEntity
    {
        IQueryable<T> All();

        IQueryable<T> AllActive();

        IQueryable<T> SearchFor(Expression<Func<T, bool>> conditions);

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Detach(T entity);
    }
}
