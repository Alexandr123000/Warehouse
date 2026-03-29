using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace Warehouse
{
    public class DatabaseWork
    {
        static internal List<MainTable> Data = new List<MainTable>();
        static internal List<TypeTable> TypeData = new List<TypeTable>();
        static internal List<SoldProductsTable> SoldProductsData = new List<SoldProductsTable>();
        static internal List<MainTable> CertainTypeProductsData = new List<MainTable>();
        MainWindow ChangeGrid;
        static SoldProducts AllSoldProducts;
        CertainTypeProducts CertainTypeProductsObject;
        public static string currentDirectory = Directory.GetCurrentDirectory();
        public static string changedDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(currentDirectory, @"../../../"));
        public static string databaseFile = @"Data Source=" + changedDirectory + "Database.db";
        public static string File = changedDirectory + "WarehouseImage.bmp";
        SQLiteConnection connection = new SQLiteConnection(databaseFile);
        SQLiteCommand command = new SQLiteCommand();
        public DatabaseWork()
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = @"CREATE TABLE IF NOT EXISTS WareTypes (id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT UNIQUE NOT NULL, UNIQUE ('Name') ON CONFLICT IGNORE);";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE IF NOT EXISTS Wares (id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Type TEXT, Price REAL, PurchasePrice REAL, Amount INTEGER, TotalPrice REAL, FOREIGN KEY (Type) REFERENCES WareTypes(Name));";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE IF NOT EXISTS SoldWares (id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Type TEXT, Price REAL, Amount INTEGER, TotalPrice REAL, FOREIGN KEY (Type) REFERENCES WareTypes(Name));";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE IF NOT EXISTS CurrentBalanceInformation (id INTEGER PRIMARY KEY AUTOINCREMENT, CurrentBalance REAL DEFAULT 10000);";
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void CurrentBalanceDataUpdating(double CurrentBalance)
        {
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            TempConnection.Open();
            Command.CommandText = @$"UPDATE CurrentBalanceInformation SET CurrentBalance = '{CurrentBalance}' WHERE id = 2;";
            Command.ExecuteNonQuery();
            MainWindow.CurrentBalance = CurrentBalance;
            TempConnection.Close();
        }
        public void CurrentBalanceDataInsertion()
        {
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            TempConnection.Open();
            Command.CommandText = $"INSERT INTO CurrentBalanceInformation (CurrentBalance) VALUES ('30000');";
            Command.ExecuteNonQuery();
            Command.CommandText = $"INSERT INTO CurrentBalanceInformation (CurrentBalance) VALUES ('30000');";
            Command.ExecuteNonQuery();
            TempConnection.Close();
        }
        public bool CurrentBalanceInformationExisting()
        {
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            TempConnection.Open();
            Command.CommandText = @"SELECT * FROM CurrentBalanceInformation";
            var Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                if (Reader.GetValue(1).ToString() == "30000")
                {
                    TempConnection.Close();
                    return true;
                }
            }
            TempConnection.Close();
            return false;
        }
        public void CurrentBalanceDataExtraction()
        {
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            TempConnection.Open();
            Command.CommandText = @"SELECT * FROM CurrentBalanceInformation";
            var Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                MainWindow.CurrentBalance = Reader.GetDouble(1);
            }
            TempConnection.Close();
        }
        public void SortProductsByName()
        {
            Data.Clear();
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            TempConnection.Open();
            Command.CommandText = @"SELECT * FROM Wares ORDER BY Name";
            var Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Data.Add(new MainTable(Convert.ToInt32(Reader.GetInt64(0)), Reader.GetString(1), Reader.GetString(2), Reader.GetDouble(3), Reader.GetDouble(4), Convert.ToInt32(Reader.GetInt64(5)), Reader.GetDouble(6)));
            }
            TempConnection.Close();
        }
        public void SortProductsByPrice()
        {
            Data.Clear();
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            TempConnection.Open();
            Command.CommandText = @"SELECT * FROM Wares ORDER BY Price DESC";
            var Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Data.Add(new MainTable(Convert.ToInt32(Reader.GetInt64(0)), Reader.GetString(1), Reader.GetString(2), Reader.GetDouble(3), Reader.GetDouble(4), Convert.ToInt32(Reader.GetInt64(5)), Reader.GetDouble(6)));
            }
            TempConnection.Close();
        }
        public void GroupProductsByType()
        {
            Data.Clear();
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            TempConnection.Open();
            Command.CommandText = @"SELECT * FROM Wares ORDER BY Type";
            var Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Data.Add(new MainTable(Convert.ToInt32(Reader.GetInt64(0)), Reader.GetString(1), Reader.GetString(2), Reader.GetDouble(3), Reader.GetDouble(4), Convert.ToInt32(Reader.GetInt64(5)), Reader.GetDouble(6)));
            }
            TempConnection.Close();
        }
        public void SetObject(MainWindow changeGrid)
        {
            ChangeGrid = changeGrid;
        }
        public void SetAllSoldProductsObject(SoldProducts SoldProducts)
        {
            AllSoldProducts = SoldProducts;
        }
        public void SetCertaingTypeProductsObject(CertainTypeProducts CertainTypeProductsObject)
        {
            this.CertainTypeProductsObject = CertainTypeProductsObject;
        }
        public void TypeDataInsertion(string Name = "")
        {
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            TempConnection.Open();
            Command.CommandText = $"INSERT INTO WareTypes (Name) VALUES ('{Name}');";
            Command.ExecuteNonQuery();
            AllTypeDataExtraction();
            ChangeGrid.productTypesGrid.ItemsSource = null;
            ChangeGrid.productTypesGrid.ItemsSource = TypeData;
            TempConnection.Close();
            InformationWindow DatabaseInformationWindow = new InformationWindow();
            DatabaseInformationWindow.InformationLabel.Content = "Product added";
            DatabaseInformationWindow.Show();
        }
        public void DataInsertion(NewProductAddition TempObject, string Name = "", string Type = "", double Price = 0, double PurchasePrice = 0, int Amount = 0, double TotalPrice = 0)
        {

            if (Amount == 0 || Amount < 0)
            {
                InformationWindow DatabaseInformationWindow = new InformationWindow();
                DatabaseInformationWindow.InformationLabel.Content = "The number is incorrect";
                DatabaseInformationWindow.Show();
            }
            else
            {
                MainWindow.CurrentBalance += Amount * Price;
                TypeData.Clear();
                TypeDataInsertion(Type);
                SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
                SQLiteCommand Command = new SQLiteCommand();
                Command.Connection = TempConnection;
                TempConnection.Open();
                Command.CommandText = $"INSERT INTO Wares (Name, Type, Price, PurchasePrice, Amount, TotalPrice) VALUES ('{Name}', '{Type}', '{Price}', '{PurchasePrice}', '{Amount}', '{TotalPrice}');";
                Command.ExecuteNonQuery();
                TempConnection.Close();
                TempConnection.Open();
                Command.CommandText = @$"UPDATE CurrentBalanceInformation SET CurrentBalance = (SELECT CurrentBalance FROM CurrentBalanceInformation WHERE id = '2') - {Price * Amount} WHERE id = '2';";
                Command.ExecuteNonQuery();
                TempConnection.Close();
                CurrentBalanceDataExtraction();
                ChangeGrid.CurrentBalanceLabel.Content = MainWindow.CurrentBalance;
            }
        }
        public void CertainTypeProductsDataExtraction()
        {
            CertainTypeProductsData.Clear();
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            TempConnection.Open();
            Command.CommandText = @$"SELECT * FROM Wares WHERE Type = '{CertainTypeProductsObject.ProductTypeTextBox.Text}';";
            var CertainTypeProductsReader = Command.ExecuteReader();
            while (CertainTypeProductsReader.Read())
            {
                CertainTypeProductsData.Add(new MainTable(Convert.ToInt32(CertainTypeProductsReader.GetInt64(0)), CertainTypeProductsReader.GetString(1), CertainTypeProductsReader.GetString(2), CertainTypeProductsReader.GetDouble(3), CertainTypeProductsReader.GetDouble(4), Convert.ToInt32(CertainTypeProductsReader.GetInt64(5)), CertainTypeProductsReader.GetDouble(6)));
            }
            TempConnection.Close();
        }
        public void AllDataExtraction()
        {
            Data.Clear();
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            TempConnection.Open();
            Command.CommandText = @"SELECT * FROM Wares";
            var Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Data.Add(new MainTable(Convert.ToInt32(Reader.GetInt64(0)), Reader.GetString(1), Reader.GetString(2), Reader.GetDouble(3), Reader.GetDouble(4), Convert.ToInt32(Reader.GetInt64(5)), Reader.GetDouble(6)));
            }
            TempConnection.Close();
        }
        public void SoldProductsDataExtraction()
        {
            SoldProductsData.Clear();
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            TempConnection.Open();
            Command.CommandText = @"SELECT * FROM SoldWares";
            var SoldProductsReader = Command.ExecuteReader();
            while (SoldProductsReader.Read())
            {
                SoldProductsData.Add(new SoldProductsTable(Convert.ToInt32(SoldProductsReader.GetInt64(0)), SoldProductsReader.GetString(1), SoldProductsReader.GetString(2), SoldProductsReader.GetDouble(3), Convert.ToInt32(SoldProductsReader.GetInt64(4)), SoldProductsReader.GetDouble(5)));
            }
            TempConnection.Close();
        }
        public void AllTypeDataExtraction()
        {
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            TempConnection.Open();
            Command.CommandText = @"SELECT * FROM WareTypes";
            var TypeReader = Command.ExecuteReader();
            while (TypeReader.Read())
            {
                TypeData.Add(new TypeTable(Convert.ToInt32(TypeReader.GetInt64(0)), TypeReader.GetString(1)));
            }
            TempConnection.Close();
        }
        public void AllProductsDeletion()
        {
            connection.Open();
            command.CommandText = @"DELETE FROM Wares";
            command.ExecuteNonQuery();
            command.CommandText = @"DELETE FROM WareTypes";
            command.ExecuteNonQuery();
            Data.Clear();
            TypeData.Clear();
            ChangeGrid.databaseMainGrid.ItemsSource = null;
            ChangeGrid.productTypesGrid.ItemsSource = null;
            ChangeGrid.databaseMainGrid.ItemsSource = Data;
            ChangeGrid.productTypesGrid.ItemsSource = TypeData;
            connection.Close();
        }
        public void ProductDeletion(ProductsSelling productsSelling, MainWindow TempObject)
        {
            double TotalPrice;

            if (Convert.ToInt32(productsSelling.ProductQuantityTextBox.Text) == 0 || Convert.ToInt32(productsSelling.ProductQuantityTextBox.Text) < 0)
            {
                InformationWindow DatabaseProductInformationWindow = new InformationWindow();
                DatabaseProductInformationWindow.InformationLabel.Content = "The number is incorrect";
                DatabaseProductInformationWindow.Show();
            }
            else
            {
                SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
                SQLiteCommand Command = new SQLiteCommand();
                TotalPrice = Convert.ToDouble(productsSelling.PriceSellingProductTextBox.Text) * Convert.ToInt32(productsSelling.ProductQuantityTextBox.Text);
                Command.Connection = TempConnection;
                TempConnection.Open();
                Command.CommandText = @$"UPDATE Wares SET Amount = (SELECT Amount FROM Wares WHERE Name = '{productsSelling.NameOfSellingProductTextBox.Text}' AND Type = '{productsSelling.TypeOfSellingProductTextBox.Text}') - {productsSelling.ProductQuantityTextBox.Text} WHERE Name = '{productsSelling.NameOfSellingProductTextBox.Text}' AND Type = '{productsSelling.TypeOfSellingProductTextBox.Text}';";
                Command.ExecuteNonQuery();
                Command.CommandText = @"DELETE FROM Wares WHERE Amount < 1;";
                Command.ExecuteNonQuery();
                Command.CommandText = $"INSERT INTO SoldWares (Name, Type, Price, Amount, TotalPrice) VALUES ('{productsSelling.NameOfSellingProductTextBox.Text}', '{productsSelling.TypeOfSellingProductTextBox.Text}', '{productsSelling.PriceSellingProductTextBox.Text}', '{productsSelling.ProductQuantityTextBox.Text}', '{TotalPrice}');";
                Command.ExecuteNonQuery();
                Command.CommandText = @$"UPDATE CurrentBalanceInformation SET CurrentBalance = (SELECT CurrentBalance FROM CurrentBalanceInformation WHERE id = '2') + {Convert.ToDouble(productsSelling.PriceSellingProductTextBox.Text) * Convert.ToInt64(productsSelling.ProductQuantityTextBox.Text)} WHERE id = '2';";
                Command.ExecuteNonQuery();
                TempConnection.Close();
                CurrentBalanceDataExtraction();
                TempObject.CurrentBalanceLabel.Content = MainWindow.CurrentBalance;
                AllDataExtraction();
                SoldProductsDataExtraction();
                TempObject.databaseMainGrid.ItemsSource = null;
                AllSoldProducts.SoldProductsGrid.ItemsSource = null;
                TempObject.databaseMainGrid.ItemsSource = Data;
                AllSoldProducts.SoldProductsGrid.ItemsSource = SoldProductsData;
                InformationWindow DatabaseProductSoldInformationWindow = new InformationWindow();
                DatabaseProductSoldInformationWindow.InformationLabel.Content = "Product sold";
                DatabaseProductSoldInformationWindow.Show();
                productsSelling.Close();
            }
        }
    }
}