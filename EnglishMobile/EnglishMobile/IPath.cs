using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishMobile
{
    public interface IPath
    {
        string GetDatabasePath(string filename);
    }
}
