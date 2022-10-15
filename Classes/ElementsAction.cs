using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;
using Pickerlib.Models;

namespace Picker.Classes
{
    static internal class ElementsAction
    {

        #region set source
        public static BitmapImage SetSource(bool connection)
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

               
            }

            bitmap.EndInit();
            return bitmap;
        }

        #endregion

        #region Set save model variant
        private static UIElement? GetElement(Grid maingrid, int row, int column)    //search grid elements by cordinates
        {
            foreach (UIElement item in maingrid.Children)
            {
                if (Grid.GetRow(item) == row && Grid.GetColumn(item) == column)
                {
                    return item;
                }
            }
            return null;
        }

        public static void CheckedAction(bool param, object sender, Grid maingrid)  //enable or disable elements
        {
            try
            {
                int rownumber = Grid.GetRow((CheckBox)sender);
                int columcount = maingrid.ColumnDefinitions.Count;
                int beginindex = 1 + rownumber * columcount;
                int endindex = columcount - 2;
                for (int columnumber = 1; columnumber <= endindex; columnumber++)
                {

                    UIElement element = GetElement(maingrid, rownumber, columnumber);
                    if (element != null)
                    {
                        element.IsEnabled = param;
                    }
                    else
                    {
                        return;
                    }
                }

            }
            catch (Exception)
            {

                return;
            }
        }

        #endregion

        #region Set value or default
        public static string SetFieldValue(bool? check, TextBox element)
        {

            if (check == true)
            {

                return element.Text;


            }
            else
            {
                return null;
            }
        }

        public static ITimeItem SetFieldValue(bool? check, ComboBox element)
        {

            if (check == true)
            {
                if (element.SelectedItem==null)
                {
                    return null;
                }
                return (ITimeItem)element.SelectedItem;


            }
            else
            {
                return null;
            }
        }

        #endregion

        #region set right time's region
        public static bool MakeRegion(ComboBox hourbox1, ComboBox minbox1, ComboBox secbox1, ComboBox hourbox2, ComboBox minbox2, ComboBox secbox2)
        {

            if (hourbox1.SelectedItem != null 
                && hourbox2.SelectedItem != null
                && minbox1.SelectedItem != null
                && minbox2.SelectedItem != null
                && secbox1.SelectedItem != null
                && secbox2.SelectedItem !=null)
            {
                ITimeItem hour1 = (ITimeItem)hourbox1.SelectedItem;
                ITimeItem min1 = (ITimeItem)minbox1.SelectedItem;
                ITimeItem sec1 = (ITimeItem)secbox1.SelectedItem;
                ITimeItem hour2 = (ITimeItem)hourbox2.SelectedItem;
                ITimeItem min2 = (ITimeItem)minbox2.SelectedItem;
                ITimeItem sec2 = (ITimeItem)secbox2.SelectedItem;



                if (hour1.Clockvalue==hour2.Clockvalue)
                {
                    if (min1.Clockvalue == min2.Clockvalue)
                    {
                        if (sec1.Clockvalue >= sec2.Clockvalue)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                    else if (min1.Clockvalue > min2.Clockvalue)
                    {
                        return false;
                    }
                    else 
                    {
                        return true;
                    } 

                }
                else if(hour1.Clockvalue > hour2.Clockvalue)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            return true;

        }
        #endregion

    }
}
