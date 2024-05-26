using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Coursework
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private ObservableCollection<Item> data;
        public AddWindow(ObservableCollection<Item> data)
        {
            InitializeComponent();
            this.data = data;
        }
        private void Got_Focus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "...")
            {
                textBox.Text = string.Empty;
            }
        }
        private void Lost_Focus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "...";
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = TypeComboBox.Text;

            if(CorrectData())
            {
                switch (selectedItem)
                {
                    case "Book":
                        Book book = new Book(Name.Text, Author.Text, Convert.ToInt32(Amount.Text), Genre.Text);
                        data.Add(book);
                        break;
                    case "Magazine":
                        Magazine magazine = new Magazine(Name.Text, Author.Text, Convert.ToInt32(Amount.Text), Theme.Text, Date.Text);
                        data.Add(magazine);
                        break;
                    case "Article":
                        Article article = new Article(Name.Text, Author.Text, Convert.ToInt32(Amount.Text), Theme.Text);
                        data.Add(article);
                        break;
                    case "Newspaper":
                        Newspaper newspaper = new Newspaper(Name.Text, Author.Text, Convert.ToInt32(Amount.Text), Theme.Text, Date.Text);
                        data.Add(newspaper);
                        break;
                    case "Comic":
                        Comic comic = new Comic(Name.Text, Author.Text, Convert.ToInt32(Amount.Text), Theme.Text, OldRange.Text);
                        data.Add(comic);
                        break;
                    default:
                        break;
                }
                this.Close();
            } 
        }
        private bool CorrectData()
        {
            Item item = new Item();//плохой вариант но без дублирования кода в Add_Click
            bool correctData = true;
            if (Name.Text == "...")
            {
                Name.ToolTip = "Wrong value";
                Name.Background = Brushes.Red;
                correctData = false;
            }//Name
            else
            {
                Name.ToolTip = null;
                Name.Background = Brushes.Transparent;
            }
            if (Author.Text == "...")
            {
                Author.ToolTip = "Wrong value";
                Author.Background = Brushes.Red;
                correctData = false;
            }//Author
            else
            {
                Author.ToolTip = null;
                Author.Background = Brushes.Transparent;
            }
            try
            {
                int amount = Convert.ToInt32(Amount.Text);
                if (amount >= 0)
                {
                    Amount.ToolTip = null;
                    Amount.Background = Brushes.Transparent;
                }
                else
                {
                    Amount.ToolTip = "Negative value";
                    Amount.Background = Brushes.Red;
                    correctData = false;
                }
            }
            catch
            {
                Amount.ToolTip = "Wrong value";
                Amount.Background = Brushes.Red;
                correctData = false;
            }//Amount
            if (Genre.Text == "..." && Genre.Visibility == Visibility.Visible)
            {
                Genre.ToolTip = "Wrong value";
                Genre.Background = Brushes.Red;
                correctData = false;
            }//Genre
            else
            {
                Genre.ToolTip = null;
                Genre.Background = Brushes.Transparent;
            }
            if (Theme.Text == "..." && Theme.Visibility == Visibility.Visible)
            {
                Theme.ToolTip = "Wrong value";
                Theme.Background = Brushes.Red;
                correctData = false;
            }//Theme
            else
            {
                Theme.ToolTip = null;
                Theme.Background = Brushes.Transparent;
            }
            if ((Date.Text == "..."||!item.CorrectDate(Date.Text))&&Date.Visibility == Visibility.Visible)
            {
                Date.ToolTip = "Incorrect date values!\n" +
                    "Year.Month.Day, delimiters - \".\"";
                Date.Background = Brushes.Red;
                correctData = false;
            }//Date
            else
            {
                if(Date.Visibility == Visibility.Visible)
                {
                    GoodDateView(Date.Text);
                }
                Date.ToolTip = null;
                Date.Background = Brushes.Transparent;
            }
            if ((OldRange.Text == "..."|| !item.CorrectRange(OldRange.Text)) && OldRange.Visibility == Visibility.Visible)
            {
                OldRange.ToolTip = "Incorrect range values!\n" +
                    "num;num1, delimiters - \"-\"";
                OldRange.Background = Brushes.Red;
                correctData = false;
            }//oldrange
            else
            {
                OldRange.ToolTip = null;
                OldRange.Background = Brushes.Transparent;
            }
            return correctData;
        }
        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)  
        {
            string selectedItem = TypeComboBox.SelectedIndex.ToString();

            if(Genre!=null&&Theme!=null&&Date!=null&&OldRange!=null)
            {
                switch (selectedItem)
                {
                    case "0":
                        Genre.Visibility = Visibility.Visible;
                        GenreLable.Visibility = Visibility.Visible;

                        Theme.Visibility = Visibility.Collapsed;
                        ThemeLabel.Visibility = Visibility.Collapsed;

                        Date.Visibility = Visibility.Collapsed;
                        DateLable.Visibility = Visibility.Collapsed;

                        OldRange.Visibility = Visibility.Collapsed;
                        OldRangeLabel.Visibility = Visibility.Collapsed;
                        break;//book
                    case "1":
                        Genre.Visibility = Visibility.Collapsed;
                        GenreLable.Visibility = Visibility.Collapsed;

                        Theme.Visibility = Visibility.Visible;
                        ThemeLabel.Visibility = Visibility.Visible;

                        Date.Visibility = Visibility.Visible;
                        DateLable.Visibility = Visibility.Visible;

                        OldRange.Visibility = Visibility.Collapsed;
                        OldRangeLabel.Visibility = Visibility.Collapsed;
                        break;//magazine
                    case "2":
                        Genre.Visibility = Visibility.Collapsed;
                        GenreLable.Visibility = Visibility.Collapsed;

                        Theme.Visibility = Visibility.Visible;
                        ThemeLabel.Visibility = Visibility.Visible;

                        Date.Visibility = Visibility.Collapsed;
                        DateLable.Visibility = Visibility.Collapsed;

                        OldRange.Visibility = Visibility.Collapsed;
                        OldRangeLabel.Visibility = Visibility.Collapsed;
                        break;//newspaper
                    case "3":
                        Genre.Visibility = Visibility.Collapsed;
                        GenreLable.Visibility = Visibility.Collapsed;

                        Theme.Visibility = Visibility.Visible;
                        ThemeLabel.Visibility = Visibility.Visible;

                        Date.Visibility = Visibility.Visible;
                        DateLable.Visibility = Visibility.Visible;

                        OldRange.Visibility = Visibility.Collapsed;
                        OldRangeLabel.Visibility = Visibility.Collapsed;
                        break;//article
                    case "4":
                        Genre.Visibility = Visibility.Collapsed;
                        GenreLable.Visibility = Visibility.Collapsed;

                        Theme.Visibility = Visibility.Visible;
                        ThemeLabel.Visibility = Visibility.Visible;

                        Date.Visibility = Visibility.Collapsed;
                        DateLable.Visibility = Visibility.Collapsed;

                        OldRange.Visibility = Visibility.Visible;
                        OldRangeLabel.Visibility = Visibility.Visible;
                        break;//comic
                    default:
                        break;
                }
            }
        }
        private void GoodDateView(string line)
        {
            string[] date = line.Trim().Split('.');
            int year = int.Parse(date[0]);
            if (date[1].Length==1)
            {
                date[1] = "0" + date[1];
            }
            if (date[2].Length == 1)
            {
                date[2] = "0" + date[2];
            }
            line = year.ToString() + "." + date[1] + "." + date[2];
            Date.Text = line;
        }
    }
}
