using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FastUpsertEF.Extentions
{
    public static class TypeExtensions
    {
        public static string GetDbTypeName(this Type type)
        {
            return type.Name + "EnhanceType";
        }

        public static string GetDbProcedureName(this Type type)
        {
            return "EnhanceUpsert" + type.Name;
        }
    }
}
