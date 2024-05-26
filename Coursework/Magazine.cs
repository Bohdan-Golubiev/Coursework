using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework
{
    public class Magazine:Item
    {
        public string Theme { get; set; }
        public string Date { get; set; }
        public Magazine() 
        { 
        }
        public Magazine(string name, string author, int amount, string theme, string date)
        {
            TypeItem = "Magazine";
            Name = name;
            Author = author;
            Amount = amount;
            Theme = theme;
            Date = date;
        }
        public override void Information()
        {
            MessageBox.Show($"This magazine has the name {Name} published by {Author},\n" +
                $"the theme of which is {Theme} has a circulation of {Amount} units\n" +
                $"the last edition of which took place {Date}."
                , "Information");
        }
    }
}
