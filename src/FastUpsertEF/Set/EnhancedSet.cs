﻿using FastUpsertEF.EnhancedDb;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FastUpsertEF.Set
{
    public class EnhancedSet<TEntity> : IEnhancedSet<TEntity>, IOperationSet where TEntity : class
    {
        private readonly EnhanceDb enhanceDb;
        public List<KeyValuePair<Type, DataTable>> UpsertOperations { get; private set; }
        private readonly DbSet<TEntity> normalSet;

        public EnhancedSet(EnhanceDb enhanceDb, DbSet<TEntity> contextSet)
        {
            this.enhanceDb = enhanceDb;
            normalSet = contextSet;
            UpsertOperations = new List<KeyValuePair<Type, DataTable>>();
        }

        public void Upsert(IEnumerable<TEntity> entities)
        {
            //TODO: Modify normal set
            //TODO: Mount upsertOperations list acording to normal set
            UpsertOperations.Add(new KeyValuePair<Type, DataTable>(typeof(TEntity), EntitiesToDataTable(entities)));
        }

        public DbSet<TEntity> NormalSet() => normalSet;

        private bool HasKeyAttribute(PropertyInfo p)
        {
            return Attribute.IsDefined(p, typeof(KeyAttribute));
        }

        private DataTable EntitiesToDataTable(IEnumerable<TEntity> entities)
        {
            return new DataTable();
        }
    }
}
