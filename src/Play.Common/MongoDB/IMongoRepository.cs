using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Play.Common.MongoDB
{
    public interface IMongoRepository<T> where T : Play.Common.Entities.IEntity
    {
        IMongoQueryable<T> Query();
        Task<T> GetOne(Guid id);
        Task<T> GetOne(Expression<Func<T, bool>> filter);
        Task<T> FindOneAndUpdate(Expression<Func<T, bool>> expression, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> option);
        Task UpdateOne(Expression<Func<T, bool>> expression, T update);
        Task DeleteOne(Expression<Func<T, bool>> expression);
        Task InsertMany(IEnumerable<T> items);
        Task InsertOne(T item);
    }
}