using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Coursework
{
    public class Newspaper:Item
    {
        public string Theme { get; set; }
        public string Date { get; set; }
        public Newspaper()
        {
        }
        public Newspaper(string name, string author, int amount, string theme, string date)
        {
            TypeItem = "Newspaper";
            Name = name;
            Author = author;
            Amount = amount;
            Theme = theme;
            Date = date;
        }
        public override void Information()
        {
            MessageBox.Show($"This newspaper created by {Author} Publishing has the name {Name}.\n" +
                $" It has the theme of {Theme}, made in {Amount} copies\n" +
                $"and published on {Date}."
                , "Information");
        }
    }
}
