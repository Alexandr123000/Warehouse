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
            NewProductAddition NewProduct = new NewProductAddition(this);
            ProductsSelling SomeProductSelling = new ProductsSelling(this);
            SoldProducts AllSoldProducts = new SoldProducts(this);
            this.DataContext = new MainWindowViewModel();
            MainDatabase.SetObject(this);
            MainDatabase.AllDataExtraction();
            MainDatabase.AllTypeDataExtraction();
            databaseMainGrid.ItemsSource = DatabaseWork.Data;
            productTypesGrid.ItemsSource = DatabaseWork.TypeData;
            AddProductButton.Click += AddProductButton_Click;
            SellProductButton.Click += SellProductButton_Click;
            ShowProductOfCertainTypeButton.Click += ShowProductOfCertainTypeButton_Click;
            ShowSoldProductsButton.Click += ShowSoldProductsButton_Click;
            DeleteAllProductsButton.Click += DeleteAllProductsButton_Click;
        }
        private void DeleteAllProductsButton_Click(object sender, RoutedEventArgs e)
        {
            MainDatabase.AllProductsDeletion();
            InformationWindow DatabaseInformationWindow = new InformationWindow();
            DatabaseInformationWindow.InformationLabel.Content = "All products have been removed";
            DatabaseInformationWindow.Show();
        }
        private void ShowSoldProductsButton_Click(object sender, RoutedEventArgs e)
        {
            SoldProducts AllSoldProducts = new SoldProducts(this);
            MainDatabase.SoldProductsDataExtraction();
            AllSoldProducts.SoldProductsGrid.ItemsSource = null;
            AllSoldProducts.SoldProductsGrid.ItemsSource = DatabaseWork.SoldProductsData;
            AllSoldProducts.Show();
        }
        private void ShowProductOfCertainTypeButton_Click(object sender, RoutedEventArgs e)
        {
            CertainTypeProducts ProductsOfCertainType = new CertainTypeProducts();
            ProductsOfCertainType.Show();
        }
        private void SellProductButton_Click(object sender, RoutedEventArgs e)
        {
            ProductsSelling SomeProductSelling = new ProductsSelling(this);
            SomeProductSelling.Show();
        }
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            NewProductAddition NewProduct = new NewProductAddition(this);
            NewProduct.Show();
        }
    }
}