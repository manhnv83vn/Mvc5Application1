using System.Collections.Generic;
using System.Linq;

namespace Mvc5Application1.Data.Model
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        List<T> ExecuteSearch(string spName, object criteria, string paramName = "searchCriteria");

        List<TEntity> ExecuteSearch<TEntity>(string spName, object criteria, string paramName = "searchCriteria");

        void ExecuteCommand(string sqlCommand, params object[] parameters);

        List<TEntity> ExecuteStoredProcedure<TEntity>(string sqlCommand, params object[] parameters);
    }
}