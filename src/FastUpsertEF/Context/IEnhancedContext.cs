using FastUpsertEF.Set;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUpsertEF.Context
{
    interface IEnhancedContext : IDisposable, IObjectContextAdapter
    {
        EnhancedSet<TEntity> EnhancedSet<TEntity>() where TEntity : class;
    }
}
