using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace EnglishMobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public static List<Tuple<string, bool>> GlobalTupleList;
        public static string currentWord;
        public static string[] randomWord;
        public string dbFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public string fileName = "";
        public MainPage()
        {
 
            BindingContext = new ViewModels();
            InitializeComponent();
            string dbname = (this.FindByName("pickerdb") as Picker).SelectedItem.ToString();
            DbNames namesDb = new DbNames(dbname);
            fileName = DbNames.dbName;
            randomWord = File.ReadAllLines(Path.Combine(dbFolder, "RandomWords.txt"));
            SetWords();
        }

         
        private List<Tuple<string, bool>> GetWords()
        {
            var db = new RandomWordsContext(Path.Combine(dbFolder, fileName));
            int wordNumber = 0;
            var qwerty =   db.Words;
            foreach (var item in qwerty)
            {

            }
            int counts = db.Words.Count();
            wordNumber = new Random().Next(0, counts - 1);

            int orderNumber = new Random().Next(0, 3);


            var word = db.Words.Find(wordNumber);
            currentWord = word.EnglishWord;//set true english word
                                           //   this.label1.Text = word.RussianhWord;// set label russian word

            MainThread.BeginInvokeOnMainThread(() =>
            {
                (this.FindByName("russianWordLabel") as Label).Text = word.RussianhWord;
            });
             
         //   attempts_label.Text = word.Try + "/5"; // counter attempts

            List<Tuple<string, bool>> List = new List<Tuple<string, bool>>();
            for (int i = 0; i <= 3; i++)
            {
                if (i == orderNumber)
                {
                    List.Add(new Tuple<string, bool>(word.EnglishWord, true));
                }
                else
                {
                    List.Add(new Tuple<string, bool>(GetFakeWords(), false));
                }
            }
            return List;
        }

        private string GetFakeWords()
        {
            return randomWord[new Random(Guid.NewGuid().GetHashCode()).Next(0, 9999)];
        }
 
        private void SetWords()
        {
            Random RND = new Random();

            GlobalTupleList = GetWords();
            GlobalTupleList = GlobalTupleList.OrderBy(a => Guid.NewGuid()).ToList();

            this.btn1.Text = GlobalTupleList[0].Item1;
            this.btn2.Text = GlobalTupleList[1].Item1;
            this.btn3.Text = GlobalTupleList[2].Item1;
            this.btn4.Text = GlobalTupleList[3].Item1;
            btn1.IsEnabled = true;
            btn2.IsEnabled = true;
            btn3.IsEnabled = true;
            btn4.IsEnabled = true;
        }
        private void CheckWord(Button checkButton)
        {
            var settings = new SpeechOptions(){Volume = 1.0f, Pitch = 1.0f};
            TextToSpeech.SpeakAsync(currentWord, settings);

            if (checkButton.Text == currentWord)
            {
 
                using (var db = new RandomWordsContext(Path.Combine(dbFolder, fileName)) )
                {
                    var tableToChange = db.Words.SingleOrDefault(b => b.EnglishWord == currentWord);    // add try +1
                    int numberOfTry = tableToChange.Try;

                    if (numberOfTry == 5)
                    {
                        db.Remove(tableToChange);
                    }
                    else
                    {
                        tableToChange.Try++;
                    }
                    db.SaveChanges();
                }
            }
            else
            {
                using (var db = new RandomWordsContext(Path.Combine(dbFolder, fileName)))
                {
                    var tableToChange = db.Words.SingleOrDefault(b => b.EnglishWord == currentWord); // refresh try 0
                    tableToChange.Try = 0;
                    db.SaveChanges();
                }

                Thread.Sleep(500);
                try
                {
                     TextToSpeech.SpeakAsync(currentWord, settings);
                }
                catch {
                
                } 
            }


            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (btn1.Text == currentWord) { btn1.BackgroundColor = Color.Green; } //else { button2.BackColor = Color.Red; }
                if (btn2.Text == currentWord) { btn2.BackgroundColor = Color.Green; } //else { button3.BackColor = Color.Red; }
                if (btn3.Text == currentWord) { btn3.BackgroundColor = Color.Green; } //else { button4.BackColor = Color.Red; }
                if (btn4.Text == currentWord) { btn4.BackgroundColor = Color.Green; } //else { button5.BackColor = Color.Red; }
                btn1.IsEnabled = false;
                btn2.IsEnabled = false;
                btn3.IsEnabled = false;
                btn4.IsEnabled = false;

            });
     

            Thread.Sleep(3000);
             TextToSpeech.SpeakAsync(currentWord, settings); 
            Thread.Sleep(2000);
            MainThread.BeginInvokeOnMainThread(() =>
            {
            btn1.BackgroundColor = Color.DarkGray;
            btn2.BackgroundColor = Color.DarkGray;
            btn3.BackgroundColor = Color.DarkGray;
            btn4.BackgroundColor = Color.DarkGray;
                SetWords();
            });

      

        }

        private   void btn1_Click(object sender, EventArgs args)
        {

            var task = Task.Factory.StartNew(() =>
            {
                CheckWord(this.btn1);
            });

        }
        private void btn2_Click(object sender, EventArgs args)
        {
            var task = Task.Factory.StartNew(() =>
            {
                CheckWord(this.btn2);
            });
        }
        private void btn3_Click(object sender, EventArgs args)
        {
            var task = Task.Factory.StartNew(() =>
            {
                CheckWord(this.btn3);
            });
        }
        private void btn4_Click(object sender, EventArgs args)
        {
            
            var task = Task.Factory.StartNew(() =>
            {
                CheckWord(this.btn4);
            });
        }

        private void OnPickerChange(object sender, EventArgs args)
        {
            string dbname = (this.FindByName("pickerdb") as Picker).SelectedItem.ToString();
            DbNames namesDb = new DbNames(dbname);
            fileName = DbNames.dbName;
            SetWords();
        }
    }


    
}
