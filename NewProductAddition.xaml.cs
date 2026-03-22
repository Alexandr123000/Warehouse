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
    public partial class NewProductAddition : Window
    {
        public MainWindow MainWidowObject;
        public NewProductAddition(MainWindow MainWidowObject)
        {
            InitializeComponent();
            this.MainWidowObject = MainWidowObject;
            NewProductAdditionButton.Click += NewProductAdditionButton_Click;
            CancelNewProductAdditionButton.Click += CancelNewProductAdditionButton_Click;
        }
        private void CancelNewProductAdditionButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void NewProductAdditionButton_Click(object sender, RoutedEventArgs e)
        {
            double TempTotalPrice = Convert.ToDouble(PriceOfNewProductTextBox.Text) * Convert.ToInt32(AmountOfNewProductTextBox.Text);
            MainWidowObject.databaseMainGrid.ItemsSource = null;
            DatabaseWork.Data.Clear();
            MainWindow.MainDatabase.DataInsertion(this, NameOfNewProductTextBox.Text, TypeOfNewProductTextBox.Text, Convert.ToDouble(PriceOfNewProductTextBox.Text), Convert.ToDouble(PurchasePriceOfNewProductTextBox.Text), Convert.ToInt32(AmountOfNewProductTextBox.Text), TempTotalPrice);
            MainWindow.MainDatabase.AllDataExtraction();
            MainWidowObject.databaseMainGrid.ItemsSource = DatabaseWork.Data;
            this.Close();
        }
    }
}