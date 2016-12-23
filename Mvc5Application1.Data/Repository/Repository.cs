using Mvc5Application1.Data.Model;
using Mvc5Application1.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace Mvc5Application1.Data.Repository
{
    /// <summary>
    /// The EF-dependent, generic repository for data access
    /// </summary>
    /// <typeparam name="T">Type of entity for this Repository.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");
            DbContext = unitOfWork as DbContext;
            DbSet = DbContext.Set<T>();
        }

        protected IUnitOfWork UnitOfWork;

        protected DbContext DbContext { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }

        public virtual List<T> ExecuteSearch(string spName, object criteria, string paramName = "searchCriteria")
        {
            return ExecuteSearch<T>(spName, criteria, paramName);
        }

        public virtual List<TEntity> ExecuteSearch<TEntity>(string spName, object criteria, string paramName = "searchCriteria")
        {
            string xml = XmlSerializeHelper.Serialize(criteria);
            var searchCriteria = new SqlParameter
            {
                ParameterName = paramName,
                Value = xml
            };
            return DbContext.Database.SqlQuery<TEntity>(string.Format("exec {0} @{1}", spName, paramName), searchCriteria).ToList();
        }

        public virtual void ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            DbContext.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        public virtual List<TEntity> ExecuteStoredProcedure<TEntity>(string sqlCommand, params object[] parameters)
        {
            var cmd = "EXEC " + sqlCommand + " " + string.Join(",", parameters);

            return DbContext.Database.SqlQuery<TEntity>(cmd).ToList();
        }
    }
}