using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUpsertEF.Set
{
    public interface IEnhancedSet<TEntity> where TEntity : class
    {
        DbSet<TEntity> NormalSet();
        void Upsert(IEnumerable<TEntity> entities);
    }
}
