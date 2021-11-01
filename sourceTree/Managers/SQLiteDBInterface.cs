using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace sourceTree
{
    public interface SQLiteDBInterface
    {
        SQLiteAsyncConnection createSQLiteDB();
    }
}
