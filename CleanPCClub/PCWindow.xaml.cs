using System;
using System.Collections.Generic;
using System.Data;
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

namespace CleanPCClub
{
    /// <summary>
    /// Логика взаимодействия для PCWindow.xaml
    /// </summary>
    public partial class PCWindow : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer;
        public PCWindow()
        {
            InitializeComponent();
            DataGrid dataGrid = new DataGrid();
            PC.SearchAll(dataGrid);
            DataView data = (DataView)dataGrid.ItemsSource;
            for (int i = 0; i < data.Table.Rows.Count; i++)
            {
                PCControl pC = new PCControl(data[i]);
                pC.Margin = new Thickness(5);
                stackPanelPC.Children.Add(pC);
            }
            TimerStart();
        }

        public void TimerStart()
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            while (stackPanelPC.Children.Count > 0)
                stackPanelPC.Children.RemoveAt(0);
            DataGrid dataGrid = new DataGrid();
            PC.SearchAll(dataGrid);
            DataView data = (DataView)dataGrid.ItemsSource;
            for (int i = 0; i < data.Table.Rows.Count; i++)
            {
                PCControl pC = new PCControl(data[i]);
                pC.Margin = new Thickness(5);
                stackPanelPC.Children.Add(pC);
            }
        }

        private void PCControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
