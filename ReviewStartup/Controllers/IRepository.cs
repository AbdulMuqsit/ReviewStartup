using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ReviewStartup.EntityFramework;

namespace ReviewStartup.Controllers
{
    public interface IRepository<T>
    {

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<long> CountAsync();
        Task<List<T>> AllAsync();
        Task<T> GetAsync(int id);
        Task<int> AddAsync(T entity);
        Task<int> Update(T entity);
        int Count(Func<T, bool> pradicate);
    }

    class Repository<T> : IRepository<T> where T : class
    {
        private ReviewsStartupDbContext Context { get; set; }



        public Repository(ReviewsStartupDbContext context)
        {
            this.Context = context;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> AllAsync()
        {
            return Context.Set<T>().ToListAsync();
        }

        public Task<T> GetAsync(int id)
        {
            return Context.Set<T>().FindAsync(id);
        }

        public Task<int> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

       

        public Task<int> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public int Count(Func<T, bool> pradicate)
        {
            throw new NotImplementedException();
        }
    }
}