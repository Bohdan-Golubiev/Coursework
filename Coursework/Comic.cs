using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework
{
    public class Comic:Item
    {
        public string Theme { get; set; }
        public string OldRange { get; set; }
        public Comic()
        {
        }
        public Comic(string name, string author, int amount, string theme, string oldRange)
        {
            TypeItem = "Comic";
            Name = name;
            Author = author;
            Amount = amount;
            Theme = theme;
            OldRange = oldRange;   
        }
        public override void Information()
        {
            MessageBox.Show($"This comic with the title {Name}, invented by {Author},\n" +
                $"was published for an audience of {OldRange} year olds,\n" +
                $"themed around {Theme}, with a print run of {Amount} copies."
                , "Information");
        }
    }
}
