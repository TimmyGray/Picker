using Picker.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Picker.Contexts
{
    internal class ItemContext
    {
        public ObservableCollection<Houritem> Houritems;
        public ObservableCollection<Minitem> Minitems;
        public ObservableCollection<Secitem> Secitems;

        public ItemContext(ComboBox _HourPick, ComboBox _MinPick, ComboBox _SecPick)
        {
            Houritems = new ObservableCollection<Houritem>();
            Minitems = new ObservableCollection<Minitem>();
            Secitems = new ObservableCollection<Secitem>();
            for (int i = 0; i < 60; i++)
            {
                while (i<24)
                {
                    Houritem houritem = new Houritem(i);
                    Houritems.Add(houritem);
                    break;
                }
                Minitem minitem = new Minitem(i);
                Secitem secitem = new Secitem(i);

                Minitems.Add(minitem);
                Secitems.Add(secitem);

            }

            FillHourBox(_HourPick);
            FillMinBox(_MinPick);
            FillSecBox(_SecPick);
        }

        private void FillHourBox(ComboBox HourPick)
        {
            for (int i = 0; i < Houritems.Count; i++)
            {
                TextBlock hour = new TextBlock();
                hour.FontSize = 48;
                if (i < 10)
                {
                    hour.Text = "0";
                }
                hour.Text += Houritems[i].TimeValue.ToString();
                HourPick.Items.Add(hour);
            }
        }

        private void FillMinBox(ComboBox MinPick)
        {
            for (int i = 0; i < Minitems.Count; i++)
            {
                TextBlock min = new TextBlock();
                min.FontSize = 48;
                if (i < 10)
                {
                    min.Text = "0";
                }
                min.Text += Minitems[i].TimeValue.ToString();
                MinPick.Items.Add(min);
            }
        }

        private void FillSecBox(ComboBox SecPick)
        {
            for (int i = 0; i < Secitems.Count; i++)
            {
                TextBlock sec = new TextBlock();
                sec.FontSize = 48;
                if (i < 10)
                {
                    sec.Text = "0";
                }
                sec.Text += Secitems[i].TimeValue.ToString();
                SecPick.Items.Add(sec);
            }
        }


        public Date GetDate(string hour,string min,string sec)
        {
            try
            {
                string parsedate = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day} {hour}:{min}:{sec}";
                DateTime datetime = DateTime.ParseExact(parsedate, "yyyy-M-d HH:mm:ss", CultureInfo.InvariantCulture);
                Date date = new Date();
                date.DateValue = datetime;
                return date;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            
        }
    }
}
