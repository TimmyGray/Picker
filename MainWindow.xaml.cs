using System;
using System.Collections.Generic;
using System.IO;
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
using Pickerlib;

using Pickerlib.Models;
using Pickerlib.Contexts;
using Pickerlib.Models.Classes;

namespace Picker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClockContext<ITimeItem> data;

        private BitmapImage SetSource(bool connection)
        {

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
           

            if (connection)
            {
                string uri = System.IO.Path.Combine(
                                            Directory.GetCurrentDirectory(),
                                            "Resources/button green.png");
                bitmap.UriSource = new Uri(uri);

            }
            else
            {
                string uri = System.IO.Path.Combine(
                                            Directory.GetCurrentDirectory(),
                                            "Resources/button red.png");
                bitmap.UriSource = new Uri(uri);

                MessageBox.Show("Подключения нет");
            }

            bitmap.EndInit();
            return bitmap;
        }
        
        public MainWindow()
        {

            InitializeComponent();

            data = new ClockContext<ITimeItem>();
            HourPickBox1.ItemsSource = data.Houritems;
            MinPickBox1.ItemsSource = data.Minitems;
            SecPickBox1.ItemsSource = data.Secitems;
            HourPick2Box.ItemsSource = data.Houritems;
            MinPickBox2.ItemsSource = data.Minitems;
            SecPickBox2.ItemsSource = data.Secitems;


            using (VoteContext db = new VoteContext())
            {
                bool conn = db.CheckConn();

                TestConnBut.Source = SetSource(conn);
            }
         
        }

        private void SaveBut_Click(object sender, RoutedEventArgs e)
        {
            string question = QuestionBox.Text;
            string answer1 = Ans1Box.Text;
            string answer2 = Ans2Box.Text;
            string phone1 = Phone1Box.Text;
            string phone2 = Phone2Box.Text;
            ITimeItem hour1 = (ITimeItem)HourPickBox1.SelectedItem;
            ITimeItem min1 = (ITimeItem)MinPickBox1.SelectedItem;
            ITimeItem sec1 = (ITimeItem)SecPickBox1.SelectedItem;
            ITimeItem hour2 = (ITimeItem)HourPick2Box.SelectedItem;
            ITimeItem min2 = (ITimeItem)MinPickBox2.SelectedItem;
            ITimeItem sec2 = (ITimeItem)SecPickBox2.SelectedItem;

            if (hour1!=null&&min1!=null&&sec1!=null&& hour2 != null && min2 != null && sec2 != null)
            {
                try
                {
                    DateTime date_start = VoteSet<int>.MakeDate(hour1.Clockvalue, min1.Clockvalue, sec1.Clockvalue);
                    DateTime date_stop = VoteSet<int>.MakeDate(hour2.Clockvalue, min2.Clockvalue, sec2.Clockvalue);
                    Vote newvote = VoteSet<int>.SetValue(date_start, date_stop, new Vote());
                    using (VoteContext db = new VoteContext())
                    {
                        db.phone_vote_question.Add(newvote);
                        db.SaveChanges();

                    }

                    MessageBox.Show("Успешно!");

                    

                }
                catch (Exception error)
                {

                    MessageBox.Show($"{ error.Message}/n{error.InnerException.Message}");
                    

                }

            }




        }

        private void TestConnBut_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            using (VoteContext db = new VoteContext())
            {
                bool con = db.CheckConn();

                TestConnBut.Source = SetSource(con);


            }
        }


        private void QCheck_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void A1Check_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void A2Check_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DstartCheck_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DstopCheck_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void P1Check_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void P2Check_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
