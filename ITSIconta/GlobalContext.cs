using PersistenceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSIconta
{
    static class GlobalContext
    {
        public const string DBNAME = "ITSIConta";
        public static DataAccess connection;
    }
}
