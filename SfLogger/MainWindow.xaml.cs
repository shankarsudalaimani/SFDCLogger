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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SfLogger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<string> Logs { get; set; }
        public bool onTop { get; set; }
        private SFConnection sfConnection;
        
        
        public MainWindow(SFConnection connection)
        {
            InitializeComponent();
            this.DataContext = this;
            Logs = new ObservableCollection<string>();
            sfConnection = connection;
            sfConnection.Connected += delegate {
                ResetLoading();
            };
            sfConnection.Initialize();
        }


        private async void fetchBtn_Click(object sender, RoutedEventArgs e)
        {
            Loading();
            Logs.Clear();
            foreach (var log in await sfConnection.QueryLogs())
            {
                Logs.Add(log);
            }
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            Loading();
            sfConnection.RegisterUser();
        }

        private void Loading()
        {
            ellipse.Visibility = System.Windows.Visibility.Visible;
            fetchBtn.IsEnabled = false;
            registerBtn.IsEnabled = false;
        }

        private void ResetLoading()
        {
            ellipse.Visibility = System.Windows.Visibility.Hidden;
            fetchBtn.IsEnabled = true;
            registerBtn.IsEnabled = true;
        }

        private void onTopBox_Checked(object sender, RoutedEventArgs e)
        {
            Window window = (Window)this;
            window.Topmost = onTop;
        }




    }
}
