using MongoDB.Driver;

namespace ProAdvCore.Repository.Context
{
    public class ContextMongo
    {
        public static IMongoDatabase Database { get; set; }
    }
}
