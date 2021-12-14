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

namespace PL
{
    /*        <Grid Name="PageGrod" HorizontalAlignment="Stretch" Height="auto" VerticalAlignment="Stretch" Width="auto" Background="#FF91B4AB">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*"/>
                <ColumnDefinition Width="2*"/>
                <ColumnDefinition Width="*"/>
            </Grid.ColumnDefinitions >
            <Grid x:Name="GridForm" Grid.Column="1" Background="#FF6D978C">
                <Grid ShowGridLines="True">
                    <Grid.RowDefinitions>
                        <RowDefinition Height="*" />
                        <RowDefinition Height="3*" />
                        <RowDefinition Height="*" />
                    </Grid.RowDefinitions>
                </Grid>
            </Grid>
        </Grid>*/

    /// <summary>
    /// Interaction logic for DisplayDrone.xaml
    /// </summary>
    public partial class DisplayDrone : Window
    {
        public DisplayDrone(DroneBL drone)
        {
            InitializeComponent();
        }
        public DisplayDrone()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
