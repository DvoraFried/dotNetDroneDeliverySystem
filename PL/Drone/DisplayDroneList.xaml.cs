using BO;
using PO;
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
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneList : Window
    {
        BlApi.IBL Bl;   
        int droneStatus = -1;
        int droneMaxWeight = -1;
        /// <summary>
        /// the constructor render cmobo box and listview
        /// </summary>
        /// <param name="bl"></param>
        public DroneList(BlApi.IBL bl)
        {
            InitializeComponent();
            Bl = bl;
            Bl.ActionUpdateList += AddPlDrone;
            dronesDisplay.ItemsSource = Bl.GetDronesByStatusAndMaxW(droneStatus, droneMaxWeight);
            List<ComboBoxItem> itemList = new List<ComboBoxItem>(); 
            
            for (int i = 0; i < 3; i++) {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = (BO.Enum.DroneStatusesBL)(i);
                itemList.Add(newItem);
            }
            OrderByStatus.ItemsSource = itemList;
            itemList = new List<ComboBoxItem>();

            for (int i = 0; i < 3; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = (BO.Enum.WeightCategoriesBL)(i);
                itemList.Add(newItem);
            }
            OrderByMaxWeight.ItemsSource = itemList;
            
        }
        /// <summary>
        /// the function update the drone list after update and add drone
        /// </summary>
        /// <param name="arg2"></param>
        private void AddPlDrone( bool arg2)
        {
            dronesDisplay.ItemsSource = Bl.GetDronesByStatusAndMaxW(droneStatus, droneMaxWeight);
        }
        /// <summary>
        /// function returns and refresh the drone list according to th econdition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxOByStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox senderCB = sender as ComboBox;
            droneStatus = senderCB.SelectedIndex;
            dronesDisplay.ItemsSource = Bl.GetDronesByStatusAndMaxW(droneStatus, droneMaxWeight);

        }
        /// <summary>
        /// kana"l
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxOByMaxW_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox senderCB = sender as ComboBox;
            droneMaxWeight = senderCB.SelectedIndex;
            dronesDisplay.ItemsSource = Bl.GetDronesByStatusAndMaxW(droneStatus, droneMaxWeight);
        }
        /// <summary>
        /// function clear the conditions to display drone list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearFilter_Click(object sender, RoutedEventArgs e)
        {
            OrderByStatus.Text = string.Empty;
            OrderByMaxWeight.Text = string.Empty;
            droneStatus = -1; droneMaxWeight = -1;
            dronesDisplay.ItemsSource = Bl.GetDronesByStatusAndMaxW(droneStatus, droneMaxWeight);
        }
        /// <summary>
        /// function send the customer to a drone womdow after double clicking on dron
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Drone drone = (sender as ListView).SelectedValue as Drone;
            if (drone != null)
            {
                new DisplayDrone(Bl, drone).Show();
            }
        }
        /// <summary>
        /// function send customer to add drone window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddDrone_Click(object sender, RoutedEventArgs e)
        {
            DisplayDrone addDrone = new DisplayDrone(Bl);
            addDrone.Show();
        }
        /// <summary>
        /// function update the drone list to be sorted by drone status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStatusSortFilter_Click(object sender, RoutedEventArgs e)
        {
            dronesDisplay.ItemsSource = Bl.GetDronesSortrdByStatusOrder();
        }

    }
}




















































































/*            switch (senderCB.SelectedIndex)
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
        }*/