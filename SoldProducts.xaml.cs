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
    public partial class SoldProducts : Window
    {
        MainWindow TempObject;
        public SoldProducts(MainWindow TempObject)
        {
            InitializeComponent();
            
            this.TempObject = TempObject;
            CloseSoldProductsButton.Click += CloseSoldProductsButton_Click;
            DatabaseWork SoldProductsObject = new DatabaseWork();
            SoldProductsObject.SetAllSoldProductsObject(this);
            SoldProductsObject.SoldProductsDataExtraction();
            SoldProductsGrid.ItemsSource = DatabaseWork.SoldProductsData;
        }
        private void CloseSoldProductsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}