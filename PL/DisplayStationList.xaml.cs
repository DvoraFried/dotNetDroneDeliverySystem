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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayStationList.xaml
    /// </summary>
    public partial class DisplayStationList : Window
    {
        IBL.IBL Bl;
        public DisplayStationList(IBL.IBL bl)
        {
            InitializeComponent();
            Bl = bl;
            stationDisplay.ItemsSource = Bl.ReturnStationList();
        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IBL.BO.StationToList station = (sender as ListView).SelectedValue as IBL.BO.StationToList;
            this.Close();
            new DisplayStation(Bl, Bl.convertStationToListToStationBl(station)).ShowDialog();
        }

        private void ButtonAddStation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new DisplayStation(Bl).ShowDialog();
        }
    }
}
