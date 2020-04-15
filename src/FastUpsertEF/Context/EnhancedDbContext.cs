using FastUpsertEF.EnhancedDb;
using FastUpsertEF.Set;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUpsertEF.Context
{
    public class EnhancedDbContext : DbContext, IEnhancedContext
    {
        private readonly IDictionary<Type, object> enhancedSets;
        private readonly EnhanceDb enhanceDb;

        public EnhancedDbContext(string connection) : base(connection)
        {
            enhanceDb = new EnhanceDb(this);
            enhancedSets = new Dictionary<Type, object>();
        }

        public EnhancedSet<TEntity> EnhancedSet<TEntity>() where TEntity : class
        {
            if (enhancedSets.ContainsKey(typeof(TEntity)))
                return enhancedSets[typeof(TEntity)] as EnhancedSet<TEntity>;

            var newSet = new EnhancedSet<TEntity>(enhanceDb, Set<TEntity>());
            enhancedSets.Add(typeof(TEntity), newSet);

            return newSet;
        }
    }
}
