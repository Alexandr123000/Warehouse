using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Warehouse
{
    public partial class InformationWindow : Window
    {
        public InformationWindow()
        {
            InitializeComponent();
            CloseInformationWindowButton.Click += CloseInformationWindowButton_Click;
            this.Icon = new BitmapImage(new Uri(DatabaseWork.File, UriKind.RelativeOrAbsolute));
        }
        private void CloseInformationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}