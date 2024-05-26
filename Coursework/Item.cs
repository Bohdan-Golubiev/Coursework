using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Coursework
{
    [XmlInclude(typeof(Book))]            // явное указание типов, которые могут быть сериализованы. 
    [XmlInclude(typeof(Magazine))]
    [XmlInclude(typeof(Article))]
    [XmlInclude(typeof(Newspaper))]
    [XmlInclude(typeof(Comic))]
    public class Item
    {
        public string TypeItem { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Amount { get; set; }
        public virtual void Information()
        {
            MessageBox.Show($"Information about this Item\nName - {Name}\nAuthor - {Author}\nAmount - {Amount}");
        }
        public bool CorrectDate(string line)
        {
            try
            {
                int[] dateUint = new int[line.Trim().Split('.').Length];

                for (int i = 0; i < 3; i++)
                {
                    dateUint[i] = Convert.ToInt16(line.Trim().Split('.')[i]);
                }//удобный формат?

                if (dateUint[0] < 1000 || dateUint[0] > 9999 || dateUint[1] < 1 || dateUint[1] > 12)
                {
                    return false;
                }
                int[] daysInMonth = { 31, 28 + (DateTime.IsLeapYear(dateUint[0]) ? 1 : 0), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                if (dateUint[2] < 1 && dateUint[2] > daysInMonth[dateUint[1] - 1])
                {
                    return false;
                }//существующая дата?

                DateTime current = DateTime.Now;
                DateTime time = new DateTime(dateUint[0], dateUint[1], dateUint[2]);

                if (current < time)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool CorrectRange(string line)
        {
            try
            {
                uint[] range = new uint[line.Trim().Split(',', ';', '-').Length];
                for (int i = 0; i < 2; i++)
                {
                    range[i] = Convert.ToUInt16(line.Trim().Split(',', ';', '-')[i]);
                }
                if (range[0] >= range[1])
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
