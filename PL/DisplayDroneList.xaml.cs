using IBL.BO;
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
/*<ComboBox SelectionChanged="comboBoxOBS_SelectionChanged" x:Name="OrderByMaxWeight" HorizontalAlignment="Left" Height="30" Margin="20,30,0,0" VerticalAlignment="Top" Width="120"/>
<ComboBox SelectionChanged="comboBoxOBME_SelectionChanged" x:Name="OrderByStatus" HorizontalAlignment="Left" Height="30" Margin="160,30,0,0" VerticalAlignment="Top" Width="120"/>*/
namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneList : Window
    {
        public DroneList()
        {
            InitializeComponent();

            List<ComboBoxItem> itemList = new List<ComboBoxItem>(); 
            
                for (int i = 0; i < 3; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = (EnumBL.DroneStatusesBL)(i);
                itemList.Add(newItem);
            }
            OrderByStatus.ItemsSource = itemList;
            itemList = new List<ComboBoxItem>();
            for (int i = 0; i < 3; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = (EnumBL.WeightCategoriesBL)(i);
                itemList.Add(newItem);
            }
            OrderByMaxWeight.ItemsSource = itemList;
        }
        public IBL.IBL MyBl { get; set; }
        private void comboBoxOBS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox senderCB = sender as ComboBox;
            MessageBox.Show($"{senderCB.SelectedIndex} Selected");
        }
        private void comboBoxOBME_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox senderCB = sender as ComboBox;
            MessageBox.Show($"{senderCB.SelectedIndex} Selected");
            switch (senderCB.SelectedIndex)
            {
                case 0:
                    showEmptyDrones();
                    break;
                case 1:
                    showMaintenaceDrones();
                    break;
                case 2:
                    showShippingDrones();
                    break;
            }
        }
        public void showEmptyDrones()
        {
            MessageBox.Show("empty drone list");
        }
        public void showMaintenaceDrones()
        {
            MessageBox.Show("mainstain drone list");
        }
        public void showShippingDrones()
        {
            MessageBox.Show("shipping drone list");
        }
    }
}
