using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework
{
    public class Article:Item
    {
        public string Theme { get; set; }
        public Article()
        {
        }
        public Article(string name, string author, int amount, string theme)
        {
            TypeItem = "Article";
            Name = name;
            Author = author;
            Amount = amount;
            Theme = theme;
        }
        public override void Information()
        {
            MessageBox.Show($"This article written by - {Author} has the titles {Name}\n" +
                $"whose subject is {Theme}\n" +
                $"has been published in {Amount} units."
                , "Information");
        }
    }
}
