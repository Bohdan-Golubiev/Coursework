using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework
{
    public class Book : Item
    {
        public string Genre { get; set; }
        public Book()
        {
        }
        public Book(string name, string author, int amount, string genre)
        {
            TypeItem = "Book";
            Name = name;
            Author = author;
            Amount = amount;
            Genre = genre;
        }
        public override void Information()
        {
            MessageBox.Show($"This book has the title {Name} written by {Author}.\n" +
                $"It is available in a quantity of {Amount} copies.\n" +
                $"The genre is - {Genre}", "Information");
        }
    }
}
