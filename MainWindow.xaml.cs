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
        }
    }
}