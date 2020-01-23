using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishMobile
{
    class DbNames
    {
        public static string dbName;
        public DbNames(object cbName)
        {
            dbName = cbName as String;
        }
    }
}
