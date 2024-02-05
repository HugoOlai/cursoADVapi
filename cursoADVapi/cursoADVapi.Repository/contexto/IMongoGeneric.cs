using MongoDB.Driver;
using ProAdvCore.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProAdvCore.Repository.Context
{
    public interface IMongoGeneric<T> where T : IMongoModel
    {
        void InsertOne(T model);

        void InsertMany(List<T> lista);

        List<T> FindAll();

        List<T> FindBuilders(FilterDefinition<T> filter);

        List<T> FindExpression(Expression<Func<T, bool>> filter);

        T FindOneExpression(Expression<Func<T, bool>> filter);

        List<T> FindExpressionAggregate(List<Expression<Func<T, bool>>> filter);

        List<T> FindExpressionQueryable(List<Expression<Func<T, bool>>> filter);

        DeleteResult DeleteOne(FilterDefinition<T> filter);

        UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update);
    }
}
