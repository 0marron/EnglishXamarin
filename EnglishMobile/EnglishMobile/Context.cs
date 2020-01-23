using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishMobile
{
    class RandomWordsContext : DbContext
    {
        public DbSet<Word> Words { get; set; }

        private string _databasePath { get; set; }
        public RandomWordsContext(string databasePath)
        {
            _databasePath = databasePath;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Filename={ _databasePath }");

    }

    public class Word
    {
        public int WordId { get; set; }
        public string EnglishWord { get; set; }
        public string RussianhWord { get; set; }
        public int Try { get; set; }



    }
}
