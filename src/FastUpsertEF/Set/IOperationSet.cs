using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUpsertEF.Set
{
    public interface IOperationSet
    {
        List<KeyValuePair<Type, DataTable>> UpsertOperations { get;}
    }
}
