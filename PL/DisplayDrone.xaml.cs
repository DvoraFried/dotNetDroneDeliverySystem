using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for DisplayDrone.xaml
    /// </summary>
    public partial class DisplayDrone : Window
    {
        IBL.IBL Bl;

        int maxWeight = 1;
        public DisplayDrone(IBL.IBL bl,DroneBL drone)
        {
            Bl = bl;
            InitializeComponent();
        }
        public DisplayDrone(IBL.IBL bl)
        {
            Bl = bl;
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (0==0)
            {
                NumberTextBox.Foreground = Brushes.Red;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NumberTextBox.Text) || string.IsNullOrWhiteSpace(ModelTextBox.Text) || string.IsNullOrWhiteSpace(StationIdTextBox.Text)){
                MessageBox.Show("one of the fields is empty");
            }
            else
            {
                Bl.AddDrone(Int32.Parse(NumberTextBox.Text), ModelTextBox.Text, (EnumBL.WeightCategoriesBL)maxWeight, Int32.Parse(StationIdTextBox.Text));
            }
        }
 
        private void light_Checked(object sender, RoutedEventArgs e)
        {
            maxWeight = 0;
        }

        private void medium_Checked(object sender, RoutedEventArgs e)
        {
            maxWeight = 1;
        }

        private void heavy_Checked(object sender, RoutedEventArgs e)
        {
            maxWeight = 2;
        }
    }
}
