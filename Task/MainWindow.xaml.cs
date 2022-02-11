global using Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataRequestedClient client;
        public MainWindow()
        {
            InitializeComponent();
            client = new DataRequestedClient("127.0.0.1", 8000, showCars);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            client.Stop();
        }

        private void Button_Click_Request_Id(object sender, RoutedEventArgs e)
        {
            try 
            {
                int id = int.Parse(inputId.Text);
                if (id >= 0) 
                {
                    client.RequestFromId(id);
                }
                
            }
            catch { }
            
        }

        private void Button_Click_Request_All(object sender, RoutedEventArgs e)
        {
            client.RequestAll();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            client.SaveData();
        }
    }
}
