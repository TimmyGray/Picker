using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Picker.Classes;
using Pickerlib.Models;
using Pickerlib.Contexts;


namespace Picker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClockContext<ITimeItem> data;
        bool rightregion = false;

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
            string question = ElementsAction.SetFieldValue(QCheck.IsChecked,QuestionBox);
            string answer1 = ElementsAction.SetFieldValue(A1Check.IsChecked,Ans1Box);
            string answer2 = ElementsAction.SetFieldValue(A2Check.IsChecked,Ans2Box);
            string phone1 = ElementsAction.SetFieldValue(P1Check.IsChecked,Phone1Box);
            string phone2 = ElementsAction.SetFieldValue(P2Check.IsChecked,Phone2Box);
            
            ITimeItem hour1 = ElementsAction.SetFieldValue(DstartCheck.IsChecked,HourPickBox1);
            ITimeItem min1 = ElementsAction.SetFieldValue(DstartCheck.IsChecked,MinPickBox1);
            ITimeItem sec1 = ElementsAction.SetFieldValue(DstartCheck.IsChecked,SecPickBox1);
            ITimeItem hour2 = ElementsAction.SetFieldValue(DstopCheck.IsChecked,HourPick2Box);
            ITimeItem min2 = ElementsAction.SetFieldValue(DstopCheck.IsChecked, MinPickBox2);
            ITimeItem sec2 = ElementsAction.SetFieldValue(DstopCheck.IsChecked,SecPickBox2);
            
            string result = null;
            
            if (DstartCheck.IsChecked == false      //question
                && DstopCheck.IsChecked == false
                && QCheck.IsChecked == true
                && A1Check.IsChecked == false
                && A2Check.IsChecked == false
                && P1Check.IsChecked == false
                && P2Check.IsChecked == false)
            {

                MessageBox.Show("question");
                result = SaveDataContext.SaveData(question);
               
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
                result = SaveDataContext.SaveData(question, answer1, answer2);
                
                MessageBox.Show(result);

                return;
            }

            if (QCheck.IsChecked == false       //save dates
                && A1Check.IsChecked == false
                && A2Check.IsChecked == false
                && P1Check.IsChecked == false
                && P2Check.IsChecked == false)
            {


                if (DstartCheck.IsChecked==true&&DstopCheck.IsChecked==false) //start date (true for date_start)
                {
                   result = SaveDataContext.SaveData(hour1, min1, sec1, true);
                }

                if (DstartCheck.IsChecked == false && DstopCheck.IsChecked == true) //stop date (false for date_stop)
                {
                   result = SaveDataContext.SaveData(hour2, min2, sec2, false);
                }

                if (DstartCheck.IsChecked == true && DstopCheck.IsChecked == true) // two dates
                {
                    
                    result = SaveDataContext.SaveData(hour1, min1, sec1, hour2, min2, sec2);
                }

                MessageBox.Show(result);

                return;

            }


            if (DstartCheck.IsChecked == false // q+2a+2ph
                && DstopCheck.IsChecked == false
                && QCheck.IsChecked == true
                && A1Check.IsChecked == true
                && A2Check.IsChecked == true
                && P1Check.IsChecked == true
                && P2Check.IsChecked == true)
            {
                result = SaveDataContext.SaveData(question, answer1, answer2, phone1, phone2);
                
                MessageBox.Show(result);

                return;

            }

            if (DstartCheck.IsChecked == true //full
                && DstopCheck.IsChecked == true
                && QCheck.IsChecked == true
                && A1Check.IsChecked == true
                && A2Check.IsChecked == true
                && P1Check.IsChecked == true
                && P2Check.IsChecked == true)
            {
               
                result = SaveDataContext.SaveData(question, answer1, answer2, hour1, min1, sec1, hour2, min2, sec2, phone1, phone2);

                MessageBox.Show(result);

                return;

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

        private void TimeChanged(object sender, SelectionChangedEventArgs e)
        {
            rightregion = ElementsAction.MakeRegion(HourPickBox1,MinPickBox1,SecPickBox1,HourPick2Box,MinPickBox2,SecPickBox2);
            if (!rightregion)
            {
                MessageBox.Show("Неправильный интервал времени");
            }
        }
    }
}
