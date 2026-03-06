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

namespace Warehouse
{
    

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static string currentDirectory = Directory.GetCurrentDirectory();
        public static string changedDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(currentDirectory, @"../../../"));
        public static string databaseFile = @"Data Source=" + changedDirectory + "---";
        SQLiteConnection connection = new SQLiteConnection(databaseFile);
        public SQLiteCommand command = new SQLiteCommand();
        
        connection.Open();

        command.Connection = connection;
        command.CommandText = @"CREATE TABLE sells (id INTEGER PRIMARY KEY AUTOINCREMENT, amount INT)";
        command.ExecuteNonQuery();
        for (int = 1; i < 20; i++)
        {
            command.CommandText = "INSERT INTO sells (amount) VALUES (" + Convert.ToString(i+3) + ")";
            command.ExecuteNonQuery();
        }
        /*
        command.CommandText = "SELECT * FROM sells"
        var reader = command.Execute.Reader();
        while (reader.Read())
        {
            MainLabel.Content = reader.GetString(1);
        }*/



    }
}