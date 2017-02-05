using HR_Management.Context;
using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace HR_Management.Repository.Common
{
    public  class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, IEntity<Guid>
    {
        protected ApplicationDbContext _context;
        protected readonly IDbSet<T> _dbset;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbset.AsNoTracking().AsEnumerable<T>();
        }

        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.AsNoTracking().Where(predicate).AsEnumerable();
            return query;
        }

        public T FindById(Guid id)
        {
            return _dbset.AsNoTracking().SingleOrDefault(x => x.Id == id);
            //  return FindBy(x => x.Id == id).FirstOrDefault();
        }
        public async Task<T> GetAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }     

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match);
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().SingleOrDefault(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().Where(match).ToListAsync();
        }


        public virtual T Add(T entity)
        {
            return _dbset.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return _dbset.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
        public int Count()
        {
            return _dbset.Count();
        }

        public Task<int> CountAsync()
        {
            return _dbset.CountAsync();
        }



    }

    //public interface IEntity
    //{
    //    Guid Id { get; set; }
    //}
}
