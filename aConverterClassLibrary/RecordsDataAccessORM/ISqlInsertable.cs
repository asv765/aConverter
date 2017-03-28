using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public interface ISQLInsertable
    {
        string InsertSQL { get; }
    }
}
