using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Xml;
using System.ComponentModel;
using System.Globalization;
using System.Collections;
using System.Reflection;

namespace Coursework
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Item> data;
        public ObservableCollection<Item> Data
        {
            get { return data; }
            set { data = value; }
        }
        public MainWindow()
        {
            InitializeComponent();
            data = new ObservableCollection<Item>();
            DataList.ItemsSource = data;
            data.Add(new Magazine("Magazine1", "Author2", 5, "Science", "2024.05.03"));
            data.Add(new Book("Book1", "Author1", 10, "Fantasy"));
            data.Add(new Book("Book2", "Author3", 15, "Thriller"));
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Search_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox != null && searchBox.Text == "Search...")
            {
                searchBox.FontStyle = FontStyles.Normal;
                searchBox.Text = string.Empty;
            }
            searchBox.PreviewKeyDown += SearchBox_EnterKeyDown;
        }
        private void SearchBox_EnterKeyDown(object sender, KeyEventArgs e)
        {
            ObservableCollection<Item> savedData = data;
            TextBox searchBox = sender as TextBox;
            if (searchBox != null && e.Key == Key.Enter)
            {
                string searchText = searchBox.Text.ToLower();
                string selectedColumn = searchFilter.Text;

                var filteredData = data.Where(item =>
                {
                    switch (selectedColumn)
                    {
                        case "Name":
                            return item.Name.ToLower().Contains(searchText);
                        case "Author":
                            return item.Author.ToLower().Contains(searchText);
                        case "Amount":
                            return item.Amount.ToString().ToLower().Contains(searchText);
                        case "Genre":
                            if (item is Book)
                            {
                                return ((Book)item).Genre.ToLower().Contains(searchText);
                            }
                            return false;
                        case "Theme":
                            if (item is Magazine)
                            {
                                return ((Magazine)item).Theme.ToLower().Contains(searchText);
                            }
                            else if (item is Newspaper)
                            {
                                return ((Newspaper)item).Theme.ToLower().Contains(searchText);
                            }
                            else if (item is Article)
                            {
                                return ((Article)item).Theme.ToLower().Contains(searchText);
                            }
                            else if (item is Comic)
                            {
                                return ((Comic)item).Theme.ToLower().Contains(searchText);
                            }
                            else
                            {
                                return false;
                            }
                        case "Date":
                            if (item is Magazine)
                            {
                                return ((Magazine)item).Date.ToLower().Contains(searchText);
                            }
                            else if(item is Newspaper)
                            {
                                return ((Newspaper)item).Date.ToLower().Contains(searchText);
                            }
                            return false;
                        case "Old range":
                            if (item is Comic)
                            {
                                return ((Comic)item).OldRange.ToLower().Contains(searchText);
                            }
                            return false;
                        default:
                            return false;
                    }
                });
                DataList.ItemsSource = filteredData.ToList();
            }
            if (searchBox.Text == "" && e.Key == Key.Enter)
            {
                DataList.ItemsSource = savedData;
            }
        }
        private void Search_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox != null && string.IsNullOrWhiteSpace(searchBox.Text))
            {
                searchBox.FontStyle = FontStyles.Italic;
                searchBox.Text = "Search...";
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Item selectedItem = DataList.SelectedItem as Item;
            if (selectedItem != null)
            {
                data.Remove(selectedItem);
            }
        }
        private void DataList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                Item selectedItem = DataList.SelectedItem as Item;

                if (selectedItem != null)
                {
                    data.Remove(selectedItem);
                }
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Item>));

                using (XmlWriter writer = XmlWriter.Create(saveFileDialog.FileName))
                {
                    serializer.Serialize(writer, data);
                }

                MessageBox.Show($"Data saved to {saveFileDialog.FileName}");
            }
        }
        private void Import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                data.Clear();

                string filePath = openFileDialog.FileName;

                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Item>));

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    try
                    {
                        ObservableCollection<Item> importedData = (ObservableCollection<Item>)serializer.Deserialize(fileStream);

                        foreach (var item in importedData)
                        {
                            if(item.Amount>=0)
                            {
                                data.Add(item);
                                if (data[data.Count - 1] is Newspaper || data[data.Count - 1] is Magazine)//оба класса имеют одинаковое поле нуждающиеся в проверке, смысла разделять как такового нет
                                {
                                    Magazine n = (Magazine)data[data.Count - 1];
                                    if (!n.CorrectDate(n.Date))
                                    {
                                        data.RemoveAt(data.Count - 1);
                                    }
                                }
                            }
                        }

                        DataList.ItemsSource = data;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Data import error: {ex.Message}");
                    }
                }
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
                AddWindow addWindow = new AddWindow(data);
                addWindow.ShowDialog();
        }
        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            string sortBy = column.Tag.ToString();

            if (!string.IsNullOrEmpty(sortBy))
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(DataList.ItemsSource);
                if (view != null)
                {
                    List<Item> items = new List<Item>(data);

                    List<Item> itemsWithNull = new List<Item>();
                    List<Item> itemsWithoutNull = new List<Item>();

                    foreach (var item in items)
                    {
                        object value = item.GetType().GetProperty(sortBy)?.GetValue(item);
                        if (value == null)
                        {
                            itemsWithNull.Add(item);
                        }
                        else
                        {
                            itemsWithoutNull.Add(item);
                        }
                    }

                    itemsWithoutNull.Sort((x, y) =>
                    {
                        object valueX = x.GetType().GetProperty(sortBy)?.GetValue(x);
                        object valueY = y.GetType().GetProperty(sortBy)?.GetValue(y);

                        return Comparer.Default.Compare(valueX, valueY);
                    });

                    data.Clear();
                    foreach (var item in itemsWithoutNull)
                    {
                        data.Add(item);
                    }
                    foreach (var item in itemsWithNull)
                    {
                        data.Add(item);
                    }
                    view.Refresh();
                }
            }
        }
        private void DataList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var clickedItem = (sender as ListView).SelectedItem as Item;

            if (clickedItem != null)
            {
                clickedItem.Information();
            }
        }
    }
}
