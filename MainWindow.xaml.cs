using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.IO;
using Base.ViewModels;

namespace Warehouse
{
    public partial class MainWindow : Window
    {
        public static DatabaseWork MainDatabase = new DatabaseWork();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            MainDatabase.TempDataInsertion();
            MainDatabase.AllDataExtraction();
            databaseMainGrid.ItemsSource = DatabaseWork.Data;
            AddProductButton.Click += AddProductButton_Click;
            SellProductButton.Click += SellProductButton_Click;
            ShowProductOfCertainTypeButton.Click += ShowProductOfCertainTypeButton_Click;
            ShowSoldProductsButton.Click += ShowSoldProductsButton_Click;
            DeleteAllProductsButton.Click += DeleteAllProductsButton_Click;
        }
        private void DeleteAllProductsButton_Click(object sender, RoutedEventArgs e)
        {
            Deletion DeleteAllProducts = new Deletion();
            DeleteAllProducts.Show();
            MainDatabase.AllProductsDeletion();
        }
        private void ShowSoldProductsButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ShowProductOfCertainTypeButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SellProductButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            NewProductAddition NewProduct = new NewProductAddition();
            NewProduct.Show();
        }
    }
}