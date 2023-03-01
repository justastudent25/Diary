using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        JsonList json = new JsonList();
        Dictionary<string, Note> Notes = new Dictionary<string, Note>();
        Dictionary<string, List<string>> NamesOnDates = new Dictionary<string, List<string>>();
        
        private void CreateNote(string Name, string Description, string Date)
        {
            Note NewNote = new Note();
            NewNote.Name = Name;
            NewNote.Description = Description;
            NewNote.Date = Date;
            Notes[Name] = NewNote;
            NamesOnDates[Date].Add(Name);
            NoteList.Items.Refresh();
        }

        private void DeleteNote(string Name, string Date)
        {
            Notes.Remove(Name);
            NamesOnDates[Date].Remove(Name);
            NoteList.Items.Refresh();
        }

        private void ChangeNote(string Name, string Description)
        {
            Notes[Name].Description = Description;
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if ((DatePick.Text == "") || (TextBoxName.Text == "") || (TextBoxName.Text == ""))
            {
                LabelName.Content = "Одна из строк пуста, пожалуйста заполните её!";
            }
            else if (Notes.ContainsKey(TextBoxName.Text))
            {
                LabelName.Content = "Ошибка! Такое название уже существует.";
            }
            else
            {
                LabelName.Content = "Название:";
                CreateNote(TextBoxName.Text, TextBoxDescription.Text, DatePick.Text);
            } 
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteNote(NoteList.SelectedValue.ToString(), DatePick.Text);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if ((DatePick.Text == "") || (TextBoxName.Text == "") || (TextBoxName.Text == ""))
            {
                LabelName.Content = "Одна из строк пуста, пожалуйста заполните её!";
            }
            else
            {
                LabelName.Content = "Название:";
                ChangeNote(NoteList.SelectedValue.ToString(), TextBoxDescription.Text);
            } 
        }

        private void DatePick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NamesOnDates.ContainsKey(DatePick.Text) == false)
            {
                NamesOnDates[DatePick.Text] = new List<string>();
            }
            NoteList.ItemsSource = NamesOnDates[DatePick.Text];
            LabelName.Content = "Название:";
            TextBoxName.IsEnabled = true;
            TextBoxName.Text = "";
            TextBoxDescription.Text = "";
            ButtonSave.IsEnabled = false;
            ButtonDelete.IsEnabled = false;
        }

        private void NoteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonSave.IsEnabled = true;
            ButtonDelete.IsEnabled = true;
            TextBoxName.IsEnabled = false;
            LabelName.Content = "Название:";
            if (NoteList.SelectedValue == null)
            {
                TextBoxName.Text = "";
                TextBoxDescription.Text = "";
            }
            else
            {
                TextBoxName.Text = NoteList.SelectedValue.ToString();
                TextBoxDescription.Text = Notes[NoteList.SelectedValue.ToString()].Description;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            json.IntoJson(Notes);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Notes = json.Outtojson();
            foreach (string name in Notes.Keys)
            {
                if (NamesOnDates.ContainsKey(Notes[name].Date))
                {
                    NamesOnDates[Notes[name].Date].Add(name);
                }
                else
                {
                    NamesOnDates[Notes[name].Date] = new List<string>() {name};
                }
            }
        }
    }
}
