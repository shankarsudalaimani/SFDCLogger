﻿using System;
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
using System.Windows.Threading;

namespace SfLogger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableLimitedQueue<string> Logs { get; set; }
        public bool onTop { get; set; }
        
        private SFConnection sfConnection;
        private DispatcherTimer timer = new DispatcherTimer();
        private int startCounter;

        public MainWindow(SFConnection connection)
        {
            InitializeComponent();
            this.DataContext = this;
            
            Logs = new ObservableLimitedQueue<string>(50);
            sfConnection = connection;
            sfConnection.Connected += ResetLoading;
            sfConnection.Initialize();
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += new EventHandler(timer_Tick);
            
        }

        public void AddLogs(IEnumerable<string> logs)
        {
            foreach (var log in logs)
            {
                Logs.Enqueue(log);
            }
        }

        private async void FetchLogs()
        {
            var logs = await sfConnection.QueryLogs();
            if (logs == null)
                return;

            AddLogs(logs);
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            if (startCounter++ % 2 == 0)
            {
                Loading();
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
            

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            FetchLogs();
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            Loading();
            sfConnection.RegisterUser();
        }

        private void Loading()
        {
            ellipse.Visibility = System.Windows.Visibility.Visible;
            startBtn.IsEnabled = false;
            registerBtn.IsEnabled = false;
        }

        private void ResetLoading()
        {
            ellipse.Visibility = System.Windows.Visibility.Hidden;
            startBtn.IsEnabled = true;
            registerBtn.IsEnabled = true;
        }

        private void onTopBox_Checked(object sender, RoutedEventArgs e)
        {
            Window window = (Window)this;
            window.Topmost = onTop;
        }

 






    }
}
