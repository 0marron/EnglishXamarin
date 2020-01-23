using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Speech.Tts;
using Android.Content.Res;
using System.IO;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace EnglishMobile.Droid
{
 
    [Activity(Label = "EnglishMobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            string content;
            AssetManager assets = this.Assets;
            FileStream writeStream;
            var docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            
            var dbFile = Path.Combine(docFolder, "10000words.db"); // FILE NAME TO USE WHEN COPIED
            if (!File.Exists(dbFile))
            {
                writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);
                assets.Open("10000words.db").CopyTo(writeStream);
                writeStream.Close();
            }

            dbFile = Path.Combine(docFolder, "RandomWords.txt"); // FILE NAME TO USE WHEN COPIED
            if (!File.Exists(dbFile))
            {
                writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);
                assets.Open("RandomWords.txt").CopyTo(writeStream); 
                writeStream.Close();
            }
            
            //var docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //var dbFile = Path.Combine(docFolder, "10000words.db"); // FILE NAME TO USE WHEN COPIED

            //FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);
            //assets.Open("10000words.db").CopyTo(writeStream);



            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}