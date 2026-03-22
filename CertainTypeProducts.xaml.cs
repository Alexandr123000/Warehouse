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
    public partial class CertainTypeProducts : Window
    {
        public CertainTypeProducts()
        {
            InitializeComponent();
            MainWindow.MainDatabase.SetCertaingTypeProductsObject(this);
            ShowCertainTypeProductsButton.Click += ShowCertainTypeProductsButton_Click;
            CloseCertainTypeProductsButton.Click += CloseCertainTypeProductsButton_Click;
        }
        private void ShowCertainTypeProductsButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseWork.CertainTypeProductsData.Clear();
            MainWindow.MainDatabase.CertainTypeProductsDataExtraction();
            CertainTypeProductsGrid.ItemsSource = null;
            CertainTypeProductsGrid.ItemsSource = DatabaseWork.CertainTypeProductsData;
        }
        private void CloseCertainTypeProductsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}