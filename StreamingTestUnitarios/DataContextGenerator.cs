using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.DependencyInjection;
using Streaming.Infraestructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreamingTestUnitarios
{
    public static class DataContextGenerator
    {
        public static MediaContext Generate()
        {
            var options = CreateNewContextOptions();
            var context = new MediaContext(options);

            CreateTestData(ref context);
            context.SaveChanges();

            return context;
        }

        private static DbContextOptions<MediaContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<MediaContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        private static void CreateTestData(ref MediaContext context)
        {
            context.ResetValueGenerators();
            context.Database.EnsureDeleted();
            //context.Medias.Add(new MediaEntity { });
        }
    }

    public static class DbContextExtensions
    {
        public static void ResetValueGenerators(this DbContext context)
        {
            var cache = context.GetService<IValueGeneratorCache>();

            foreach (var keyProperty in context.Model.GetEntityTypes()
                .Select(e => e.FindPrimaryKey().Properties[0])
                .Where(p => p.ClrType == typeof(int)
                            && p.ValueGenerated == ValueGenerated.OnAdd))
            {
                var generator = (ResettableValueGenerator)cache.GetOrAdd(
                    keyProperty,
                    keyProperty.DeclaringEntityType,
                    (p, e) => new ResettableValueGenerator());

                generator.Reset();
            }
        }
    }

    public class ResettableValueGenerator : ValueGenerator<int>
    {
        private int _current;

        public override bool GeneratesTemporaryValues => false;

        public override int Next(EntityEntry entry)
            => System.Threading.Interlocked.Increment(ref _current);

        public void Reset() => _current = 0;
    }
}
