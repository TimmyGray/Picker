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
using Picker.Classes;
using Pickerlib.Models;
using Pickerlib.Contexts;
using Pickerlib.Models.Classes;
using System.Reflection;

namespace Picker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClockContext<ITimeItem> data;

        

        private string SetFieldValue(bool? check, TextBox element)
        {
            
            if (check==true)
            {

                return element.Text;
               
                        
            }
            else
            {
                return null;
            }
        }

        private  ITimeItem SetFieldValue(bool? check, ComboBox element)
        {

            if (check == true)
            {

                return (ITimeItem)element.SelectedItem;


            }
            else
            {
                return null;
            }
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

            TestConnBut.Source = ElementsAction.SetSource(SaveDataContext.CheckConn());
            
         
        }

        private void SaveBut_Click(object sender, RoutedEventArgs e)
        {
            string question = SetFieldValue(QCheck.IsChecked,QuestionBox);
            string answer1 = SetFieldValue(A1Check.IsChecked,Ans1Box);
            string answer2 = SetFieldValue(A2Check.IsChecked,Ans2Box);
            string phone1 = SetFieldValue(P1Check.IsChecked,Phone1Box);
            string phone2 = SetFieldValue(P2Check.IsChecked,Phone2Box);

            if (DstartCheck.IsChecked == false      //question
                && DstopCheck.IsChecked == false
                && QCheck.IsChecked == true
                && A1Check.IsChecked == false
                && A2Check.IsChecked == false
                && P1Check.IsChecked == false
                && P2Check.IsChecked == false)
            {

                string result = SaveDataContext.SaveData(question);
                MessageBox.Show(result);
                return;

            }

            

            if (DstartCheck.IsChecked == false //question+ans1+ans2
                && DstopCheck.IsChecked == false
                && QCheck.IsChecked == true
                && A1Check.IsChecked == true
                && A2Check.IsChecked == true
                && P1Check.IsChecked == false
                && P2Check.IsChecked == false)
            {
                string result = SaveDataContext.SaveData(question, answer1, answer2);
                MessageBox.Show(result);
            }

            if (QCheck.IsChecked == true        //save dates
                && A1Check.IsChecked == false
                && A2Check.IsChecked == false
                && P1Check.IsChecked == false
                && P2Check.IsChecked == false)
            {
                if (DstartCheck.IsChecked==true&&DstopCheck.IsChecked==false) //start date
                {

                }

                if (DstartCheck.IsChecked == false && DstopCheck.IsChecked == true) //stop date
                {

                }

                if (DstartCheck.IsChecked == true && DstopCheck.IsChecked == true) // two dates
                {

                }

            }

            if (DstartCheck.IsChecked==true //two dates
                &&DstopCheck.IsChecked==true
                &&QCheck.IsChecked==false
                &&A1Check.IsChecked==false
                &&A2Check.IsChecked==false
                &&P1Check.IsChecked==false
                &&P2Check.IsChecked==false)
            {
                SaveDates();

            }

            if (DstartCheck.IsChecked == false // q+2a+2ph
                && DstopCheck.IsChecked == false
                && QCheck.IsChecked == true
                && A1Check.IsChecked == true
                && A2Check.IsChecked == true
                && P1Check.IsChecked == true
                && P2Check.IsChecked == true)
            {

            }

            if (DstartCheck.IsChecked == true //full
                && DstopCheck.IsChecked == true
                && QCheck.IsChecked == true
                && A1Check.IsChecked == true
                && A2Check.IsChecked == true
                && P1Check.IsChecked == true
                && P2Check.IsChecked == true)
            {

            }

            


        }

        private void SaveDates()
        {
            try
            {
                ITimeItem hour1 = (ITimeItem)HourPickBox1.SelectedItem;
                ITimeItem min1 = (ITimeItem)MinPickBox1.SelectedItem;
                ITimeItem sec1 = (ITimeItem)SecPickBox1.SelectedItem;
                ITimeItem hour2 = (ITimeItem)HourPick2Box.SelectedItem;
                ITimeItem min2 = (ITimeItem)MinPickBox2.SelectedItem;
                ITimeItem sec2 = (ITimeItem)SecPickBox2.SelectedItem;

                DateTime date_start = VoteSet.MakeDate(hour1.Clockvalue, min1.Clockvalue, sec1.Clockvalue);
                DateTime date_stop = VoteSet.MakeDate(hour2.Clockvalue, min2.Clockvalue, sec2.Clockvalue);
                Vote newvote = VoteSet.SetValue(date_start, date_stop, new Vote());
                using (VoteContext db = new VoteContext())
                {
                    db.phone_vote_question.Add(newvote);
                    db.SaveChanges();

                }

                MessageBox.Show("Успешно!");



            }
            catch (Exception error)
            {

                MessageBox.Show($"{error.Message}/n{error.InnerException.Message}");


            }
        }

        private void TestConnBut_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool state = SaveDataContext.CheckConn();
            if (state)
            {
                TestConnBut.Source = ElementsAction.SetSource(state);

                MessageBox.Show("Подключение есть");

            }
            else
            {
                TestConnBut.Source = ElementsAction.SetSource(state);

                MessageBox.Show("Подключения нет");

            }
        }

        private void AddField(object sender, RoutedEventArgs e)
        {
            ElementsAction.CheckedAction(true, sender,MainGrid);

        }

        private void RemoveField(object sender,RoutedEventArgs e)
        {
            ElementsAction.CheckedAction(false, sender, MainGrid);

        }


    }
}
