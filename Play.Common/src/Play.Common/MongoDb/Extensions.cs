using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Play.Common.Repositories;
using Play.Common.Service.Entities;
using Play.Common.Service.Settings;

namespace Play.Common.MongoDb
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            

            services.AddSingleton(serviceProvider => {
                var configuration = serviceProvider.GetService<IConfiguration>();
                ServiceSettings serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDbSettings = configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            return services;
        }


        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T: IEntity
        {
            services.AddSingleton<IRepository<T>>(services => {
                var database = services.GetRequiredService<IMongoDatabase>();
                return new MongoRepository<T>(database, collectionName);
            });

            return services;
        } 
      
    }
}
