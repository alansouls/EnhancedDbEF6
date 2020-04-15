using FastUpsertEF.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FastUpsertEF.EnhancedDb
{
    public class EnhanceDb
    {
        private readonly DbContext context;

        public EnhanceDb(DbContext context)
        {
            this.context = context;
        }

        public void ExecuteUpsertProcedure<TEntity>(DataTable typeTable) where TEntity : class
        {
            context.Database.ExecuteSqlCommand(typeof(TEntity).GetDbProcedureName(), new SqlParameter(GetTypeParameterName<TEntity>(),
                typeTable));
        }

        private static string GetTypeParameterName<TEntity>() where TEntity : class
        {
            return string.Format("@{0}", typeof(TEntity).GetDbTypeName().ToLower());
        }

        public void CreateStoreProcedureIfDontExist(Type type)
        {
            CreateTypeInDataBaseIfDontExist(type);
            var procedureExists = context.Database.SqlQuery<int>(string.Format("SELECT COUNT(*) FROM sys.objects WHERE type = 'P' AND name = \'{0}\'", type.GetDbProcedureName())).Count() > 0;
            if (procedureExists)
                return;

            context.Database.ExecuteSqlCommand(GetDbProcedureSqlCreation(type));
        }

        public void CreateTypeInDataBaseIfDontExist(Type type)
        {
            var typeExists = context.Database.SqlQuery<int>(string.Format("SELECT COUNT(*) FROM sys.objects WHERE type = 'TT' AND name = \'{0}\'", 
                GetDbTypeInSqlQuery(type.GetDbTypeName()))).Count() > 0;
            if (typeExists)
                return;

            context.Database.ExecuteSqlCommand(GetDbTypeSqlCreation(type));
        }

        public string GetDbTypeSqlCreation(Type type)
        {
            return "";
        }

        public string GetDbProcedureSqlCreation(Type type)
        {
            return "";
        }

        private bool HasKeyAttribute(PropertyInfo p)
        {
            return Attribute.IsDefined(p, typeof(KeyAttribute));
        }

        private string GetDbTypeInSqlQuery(string dbTypeName)
        {
            return string.Format("TT\\_{0}\\_________", dbTypeName);
        }
    }
}
