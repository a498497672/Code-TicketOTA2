using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ticket.SqlSugar.Repository
{
    public class RepositoryBase<T> where T : class, new()
    {
        public SqlSugarClient db { get { return GetInstance(); } }
        public void BeginTran()
        {
            db.Ado.BeginTran();
        }
        public void CommitTran()
        {
            db.Ado.CommitTran();
        }
        public void RollbackTran()
        {
            db.Ado.RollbackTran();
        }

        public SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(
                new ConnectionConfig()
                {
                    ConnectionString = Config.ConnectionString,
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    IsShardSameThread = true /*Shard Same Thread*/
                });
            //db.Aop.OnLogExecuted = (sql, pars) => //SQL执行完事件
            //{

            //};
            //db.Aop.OnLogExecuting = (sql, pars) => //SQL执行前事件
            //{

            //};
            //db.Aop.OnError = (exp) =>//执行SQL 错误事件
            //{

            //};
            //db.Aop.OnExecutingChangeSql = (sql, pars) => //SQL执行前 可以修改SQL
            //{
            //    //return new KeyValuePair<string, SugarParameter[]>(sql, pars);
            //};
            return db;
        }

        public ISugarQueryable<T> GetAll()
        {
            var entity = db.Queryable<T>();
            return entity;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> expression)
        {
            var list = db.Queryable<T>().Where(predicate).OrderBy(expression).First();
            return list;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            var entity = db.Queryable<T>().First(predicate);
            return entity;
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            var entity = db.Queryable<T>().Single(predicate);
            return entity;
        }

        public List<T> GetAllList()
        {
            var list = db.Queryable<T>().ToList();
            return list;
        }

        public List<T> GetAllList(Expression<Func<T, bool>> predicate)
        {
            var list = db.Queryable<T>().Where(predicate).ToList();
            return list;
        }

        /// <summary>
        /// 根据条件查询分页数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex">当前页面索引</param>
        /// <param name="pageSize">分布大小</param>
        /// <returns></returns>
        public PagedList<T> FindPagedList(Expression<Func<T, bool>> predicate, string orderBy = "", int pageIndex = 1, int pageSize = 20)
        {
            var totalCount = 0;
            var page = db.Queryable<T>().Where(predicate).OrderBy(orderBy).ToPageList(pageIndex, pageSize, ref totalCount);
            var list = new PagedList<T>(page, pageIndex, pageSize, totalCount);
            return list;
        }

        public List<T> GetPageList(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda, Expression<Func<T, object>> orderbyLambda, bool isAsc = true)
        {
            OrderByType type = isAsc == true ? OrderByType.Asc : OrderByType.Desc;
            var totalCount = 0;
            var list = db.Queryable<T>().Where(whereLambda).OrderBy(orderbyLambda, type).ToPageList(pageIndex, pageSize, ref totalCount);
            total = totalCount;
            return list;
        }

        public long Add(T entity)
        {
            //返回插入数据的标识字段值
            var i = db.Insertable(entity).ExecuteReturnBigIdentity();
            return i;
        }

        public long Add(List<T> entity)
        {
            //返回插入数据的标识字段值
            var i = db.Insertable(entity).ExecuteReturnBigIdentity();
            return i;
        }

        /// <summary>
        /// 更新实体数据 这种方式会以主键为条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            //这种方式会以主键为条件
            var i = db.Updateable(entity).ExecuteCommand();
            return i > 0;
        }

        public bool Update(List<T> entity)
        {
            //这种方式会以主键为条件
            var i = db.Updateable(entity).ExecuteCommand();
            return i > 0;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            var i = db.Deleteable(entity).ExecuteCommand();
            return i > 0;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> @where)
        {
            var i = db.Deleteable<T>(@where).ExecuteCommand();
            return i > 0;
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteById(object id)
        {
            var i = db.Deleteable<T>(id).ExecuteCommand();
            return i > 0;
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteByIds(object[] ids)
        {
            var i = db.Deleteable<T>().In(ids).ExecuteCommand();
            return i > 0;
        }
    }
}
