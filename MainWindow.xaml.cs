using System;
using System.Collections.Generic;
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
using Microsoft.EntityFrameworkCore;
using Picker.Classes;
using Picker.Contexts;

namespace Picker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ItemContext itemcontext;
        ConfigurationDb configuration;
        public MainWindow()
        {
            InitializeComponent();
            itemcontext = new ItemContext(HourPick,MinPick,SecPick);
            configuration = new ConfigurationDb();
        }

        private void SaveBut_Click(object sender, RoutedEventArgs e)
        {
            TextBlock hour = (TextBlock)HourPick.SelectedItem;
            TextBlock min = (TextBlock)MinPick.SelectedItem;
            TextBlock sec = (TextBlock)SecPick.SelectedItem;
            if (hour!=null&&min!=null&&sec!=null)
            {
                try
                {
                    Date newdate = itemcontext.GetDate(hour.Text, min.Text, sec.Text);
                    using (DateContext db = new DateContext(configuration.options))
                    {
                        db.Dates.Add(newdate);
                        db.SaveChanges();
                    }

                    using (DateContext db = new DateContext(configuration.options))
                    {
                        var date = db.Dates.First();
                        MessageBox.Show(date.DateValue.ToString());
                    }

                }
                catch (Exception error)
                {

                    MessageBox.Show(error.Message);

                }

            }




        }
    }
}
