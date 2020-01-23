using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishMobile
{
    class ViewModels
    {
        public IList<string> ComboBoxItems
        {
            get
            {
                return new List<string>() { "10000words.db", "511Verbs.db" };
            }
        }

        public string Foo
        {
            get
            {
                return "10000words.db";
            }
        }
    }
}
