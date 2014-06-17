using System;
using System.IO;
using System.Windows;
using System.Security.Cryptography;

namespace HashCheck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            chkMD5.IsChecked = CurrentConfig.UseMD5;
            chkSHA1.IsChecked = CurrentConfig.UseSHA1;
            chkSHA256.IsChecked = CurrentConfig.UseSHA256;
            chkSHA384.IsChecked = CurrentConfig.UseSHA384;
            chkSHA512.IsChecked = CurrentConfig.UseSHA512;

            Loaded += MainContainerLoaded;
        }

        void MainContainerLoaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["Argument1"] != null)
            {
                txbFilePath.Text = Application.Current.Properties["Argument1"].ToString();
                UpdateHashes();
            }
        }

        private void BtnCalcClick(object sender, RoutedEventArgs e)
        {
            UpdateHashes();
        }

        private void UpdateHashes()
        {
            FileStream fp;
            try
            {
                fp = new FileStream(txbFilePath.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch(Exception ex)
            {
                MessageBox.Show(string.Format("Could not open file\n{0}\n{1}",txbFilePath.Text,ex.Message), "Error when opening file");
                return;
            }
            //Load whole file into buffer
            var buffer = new byte[fp.Length];
            fp.Read(buffer, 0, buffer.Length);
            fp.Dispose();

            //Calc checked hashes
            txbMD5.Text = chkMD5.IsChecked == true ? BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(buffer)).Replace("-", "").ToLower() : string.Empty;
            txbSHA1.Text = chkSHA1.IsChecked == true ? BitConverter.ToString(new SHA1CryptoServiceProvider().ComputeHash(buffer)).Replace("-", "").ToLower() : string.Empty;
            txbSHA256.Text = chkSHA256.IsChecked == true ? BitConverter.ToString(new SHA256CryptoServiceProvider().ComputeHash(buffer)).Replace("-", "").ToLower() : string.Empty;
            txbSHA384.Text = chkSHA384.IsChecked == true ? BitConverter.ToString(new SHA384CryptoServiceProvider().ComputeHash(buffer)).Replace("-", "").ToLower() : string.Empty;
            txbSHA512.Text = chkSHA512.IsChecked == true ? BitConverter.ToString(new SHA512CryptoServiceProvider().ComputeHash(buffer)).Replace("-", "").ToLower() : string.Empty;
            
            //new SHA256CryptoServiceProvider().TransformBlock()
        }

        private void BtnBrowseClick(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            var dlg = new Microsoft.Win32.OpenFileDialog {DefaultExt = ".*", Filter = "Any file (.*)|*.*"};

            // Display OpenFileDialog by calling ShowDialog method
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                txbFilePath.Text = filename;
                UpdateHashes();
            }
        }

        private void ChkMD5Changed(object sender, RoutedEventArgs e)
        {
            if (chkMD5.IsChecked != null) CurrentConfig.UseMD5 = (bool) chkMD5.IsChecked;
        }

        private void ChkSHA1Changed(object sender, RoutedEventArgs e)
        {
            if (chkSHA1.IsChecked != null) CurrentConfig.UseSHA1 = (bool) chkSHA1.IsChecked;
        }

        private void ChkSHA256Changed(object sender, RoutedEventArgs e)
        {
            if (chkSHA256.IsChecked != null) CurrentConfig.UseSHA256 = (bool)chkSHA256.IsChecked;
        }

        private void ChkSHA384Changed(object sender, RoutedEventArgs e)
        {
            if (chkSHA384.IsChecked != null) CurrentConfig.UseSHA384 = (bool)chkSHA384.IsChecked;
        }

        private void ChkSHA512Changed(object sender, RoutedEventArgs e)
        {
            if (chkSHA512.IsChecked != null) CurrentConfig.UseSHA512 = (bool)chkSHA512.IsChecked;
        }
    }
}
