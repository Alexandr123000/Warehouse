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
        public NewProductAddition()
        {
            InitializeComponent();
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
            int LastId, CurrentId;
            LastId = DatabaseWork.Data[DatabaseWork.Data.Count - 1].ID;
            CurrentId = LastId += 2;
            DatabaseWork.Data.Add(new MainTable(CurrentId, NameOfNewProductTextBox.Text, TypeOfNewProductTextBox.Text, Convert.ToDouble(PriceOfNewProductTextBox.Text), Convert.ToDouble(PurchasePriceOfNewProductTextBox.Text), Convert.ToInt32(AmountOfNewProductTextBox.Text), TempTotalPrice));
            MainWindow.MainDatabase.DataInsertion(NameOfNewProductTextBox.Text, TypeOfNewProductTextBox.Text, Convert.ToDouble(PriceOfNewProductTextBox.Text), Convert.ToDouble(PurchasePriceOfNewProductTextBox.Text), Convert.ToInt32(AmountOfNewProductTextBox.Text), TempTotalPrice);
        }
    }
}