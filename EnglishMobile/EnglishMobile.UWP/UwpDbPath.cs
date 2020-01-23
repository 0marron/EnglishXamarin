 using Xamarin.Forms;
using System.IO;
using Windows.Storage;
using EnglishMobile.UWP;

[assembly: Dependency(typeof(UwpDbPath))]
namespace EnglishMobile.UWP
{
    public class UwpDbPath : IPath
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
        }
    }
}
