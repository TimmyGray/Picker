using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;

namespace Picker.Classes
{
    static internal class ElementsAction
    {


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

                MessageBox.Show("Подключения нет");
            }

            bitmap.EndInit();
            return bitmap;
        }

        private static UIElement? GetElement(Grid maingrid, int row, int column)
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

        public static void CheckedAction(bool param, object sender, Grid maingrid)
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



    }
}
