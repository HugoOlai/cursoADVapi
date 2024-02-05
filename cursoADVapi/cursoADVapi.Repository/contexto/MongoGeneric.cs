using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ProAdvCore.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProAdvCore.Repository.Context
{
    public class MongoGeneric<T> where T : IMongoModel, new()
    {
        public string CollectionName = new T().CollectionName();

        public IMongoCollection<T> Collection => ContextMongo.Database.GetCollection<T>(CollectionName);

        public void InsertOne(T model) => Collection.InsertOne(model);

        public void InsertMany(List<T> lista) => Collection.InsertMany(lista);

        public List<T> FindAll() => Collection.Find("{}").ToList();

        public List<T> FindBuilders(FilterDefinition<T> filter) => Collection.Find(filter).ToList();

        public List<T> FindExpression(Expression<Func<T, bool>> filter) => Collection.Find(filter).ToList();

        public T FindOneExpression(Expression<Func<T, bool>> filter) => Collection.Find(filter).FirstOrDefault();

        public List<T> FindExpressionAggregate(List<Expression<Func<T, bool>>> filter)
        {
            IAggregateFluent<T> query = Collection.Aggregate();

            foreach (var expression in filter)
                query = query.Match(expression);

            return query.ToList();
        }

        public List<T> FindExpressionQueryable(List<Expression<Func<T, bool>>> filter)
        {
            IMongoQueryable<T> _expressions = Collection.AsQueryable();

            foreach (var expression in filter)
                _expressions = _expressions.Where(expression);

            return _expressions.ToList();
        }

        public DeleteResult DeleteOne(FilterDefinition<T> filter) => Collection.DeleteOne(filter);

        public UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update) => Collection.UpdateOne(filter, update);
    }
}
