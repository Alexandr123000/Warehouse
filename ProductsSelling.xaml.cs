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
    public partial class ProductsSelling : Window
    {
        public ProductsSelling()
        {
            InitializeComponent();
            ProductSellingButton.Click += ProductSellingButton_Click;
            CancelProductSellingButton.Click += CancelProductSellingButton_Click;
        }

        private void CancelProductSellingButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ProductSellingButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseWork TempObject = new DatabaseWork();
            TempObject.ProductDeletion(this);
        }
    }
}
