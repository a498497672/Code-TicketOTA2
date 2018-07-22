using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using Ticket.Model.Result;

namespace Ticket.Core.Repository
{
    public class RepositoryBase1<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbset;

        public RepositoryBase1(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Where(predicate).Single();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Where(predicate).FirstOrDefault();
        }

        public List<TEntity> GetAllList()
        {
            return _dbset.ToList();
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Where(predicate).ToList();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbset;
        }

        public TPageResult<T> GetPageList<T>(int pageSize, int pageIndex, IQueryable<T> where)
        {
            var result = new TPageResult<T>();
            var total = where.Count();
            var data = where.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            return result.SuccessResult(data, total);
        }

        public IQueryable<TEntity> GetPageList<S>(int pageSize, int pageIndex, out int total, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, S>> orderbyLambda, bool isAsc = true)
        {
            total = _dbset.Where(whereLambda).Count();
            if (isAsc)
            {
                return _dbset.Where(whereLambda).OrderBy(orderbyLambda).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                return _dbset.Where(whereLambda).OrderByDescending(orderbyLambda).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            }
        }

        public void Add(TEntity entity)
        {
            _dbset.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbContext.SaveChanges();
        }

        public void Update(TEntity entity, string[] propertys)
        {
            if (propertys == null || propertys.Length == 0)
            {
                throw new Exception("当前更新的实体必须至少指定一个字段名称");
            }

            //1.0 关闭EF的 实体验证检查
            _dbContext.Configuration.ValidateOnSaveEnabled = false;

            //2.0 将实体手工追加到EF容器中
            DbEntityEntry entry = _dbContext.Entry(entity);
            //3.0 将EF容器中当前实体的代理类状态修改成Unchanged
            entry.State = EntityState.Unchanged;
            //4.0 遍历用户传入的属性数值，将代理类中的属性对应的IsModified设置成true
            foreach (var item in propertys)
            {
                entry.Property(item).IsModified = true;
            }
            _dbContext.SaveChanges();
        }

        public void Delete(TEntity entity, bool isAddedEFContext = true)
        {
            //表示当前model未追加到EF上下文容器
            if (isAddedEFContext == false)
            {
                _dbset.Attach(entity);
            }
            //将EF容器中当前实体对于的代理类状态修改成deleted
            _dbset.Remove(entity);
            _dbContext.SaveChanges();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
