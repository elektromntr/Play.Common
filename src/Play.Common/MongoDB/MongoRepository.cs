using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Play.Common.Entities;

namespace Play.Common.MongoDB
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> _collection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public MongoRepository(
            IMongoDatabase database,
            string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public IMongoQueryable<T> Query() =>
            _collection.AsQueryable();

        public async Task<T> GetOne(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetOne(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> FindOneAndUpdate(
                Expression<Func<T, bool>> expression,
                UpdateDefinition<T> update,
                FindOneAndUpdateOptions<T> option) =>
                    await _collection.FindOneAndUpdateAsync(expression, update, option);

        public async Task UpdateOne(
            Expression<Func<T, bool>> expression,
            T update) =>
                await _collection.ReplaceOneAsync(expression, update);

        public async Task DeleteOne(
            Expression<Func<T, bool>> expression) =>
                await _collection.DeleteOneAsync(expression);

        public async Task InsertMany(
            IEnumerable<T> items) =>
                await _collection.InsertManyAsync(items);

        public async Task InsertOne(
            T item) =>
                await _collection.InsertOneAsync(item);

    }
}