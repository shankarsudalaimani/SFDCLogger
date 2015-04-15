using System;
using System.Collections.Generic;
using System.IO;
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

namespace SfLogger
{
    /// <summary>
    /// Interaction logic for loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {



        public loginWindow()
        {
            InitializeComponent();
            readCachedCredentials();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(loginBox.Text)
                || string.IsNullOrWhiteSpace(passwordBox.Password)
                || string.IsNullOrWhiteSpace(urlBox.Text)
                || string.IsNullOrWhiteSpace(keyBox.Text)
                || string.IsNullOrWhiteSpace(secretBox.Text)
                || string.IsNullOrWhiteSpace(apiBox.Text))
                return;
            
            var sfConnection = new SFConnection(
                loginBox.Text, passwordBox.Password, urlBox.Text, keyBox.Text, secretBox.Text, apiBox.Text, sandboxBox.IsChecked.Value);

            MainWindow mainWindow = new MainWindow(sfConnection);
            mainWindow.Show();

            cacheCredentials();
            this.Close();
        }

        private void cacheCredentials()
        {
            File.WriteAllBytes("cache.dat", 
                EncryptionHelper.Encrypt(
                    loginBox.Text + '\n' +
                    passwordBox.Password + '\n' +
                    urlBox.Text + '\n' +
                    keyBox.Text + '\n' +
                    secretBox.Text + '\n' +
                    apiBox.Text + '\n' +
                    sandboxBox.IsChecked.ToString()));

        }

        private void readCachedCredentials()
        {
            if (! File.Exists("cache.dat"))
                return;

            var credentialsBytes = File.ReadAllBytes("cache.dat");
            var credentials = EncryptionHelper.Decrypt(credentialsBytes).Split('\n');;
            


            //var credentials = File.ReadAllLines("cache.dat");
            try
            {
                loginBox.Text = credentials[0];
                passwordBox.Password = credentials[1];
                urlBox.Text = credentials[2];
                keyBox.Text = credentials[3];
                secretBox.Text = credentials[4];
                apiBox.Text = credentials[5];
                sandboxBox.IsChecked = bool.Parse(credentials[6]);
            }
            catch(InvalidDataException ex)
            {
                MessageBox.Show("invalid cached data");
            }
            
        }
    }
}
