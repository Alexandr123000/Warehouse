using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
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
            connection.Close();
        }
        public void SetObject(MainWindow changeGrid) //+++ =================================================================
        {
            ChangeGrid = changeGrid;
        }
        public void SetAllSoldProductsObject(SoldProducts SoldProducts) //+++ =================================================================
        {
            AllSoldProducts = SoldProducts;
        }
        public void SetCertaingTypeProductsObject(CertainTypeProducts CertainTypeProductsObject) //+++ =================================================================
        {
            this.CertainTypeProductsObject = CertainTypeProductsObject;
        }
        public void TypeDataInsertion(string Name = "") //+++ =================================================================
        {
            //--------------
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            //--------------
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
        public void DataInsertion(NewProductAddition TempObject, string Name = "", string Type = "", double Price = 0, double PurchasePrice = 0, int Amount = 0, double TotalPrice = 0) // =================================================================
        {
            if (Amount == 0 || Amount < 0)
            {
                InformationWindow DatabaseInformationWindow = new InformationWindow();
                DatabaseInformationWindow.InformationLabel.Content = "The number is incorrect";
                DatabaseInformationWindow.Show();
            }
            else
            {
                TypeData.Clear();
                TypeDataInsertion(Type);
                //--------------
                SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
                SQLiteCommand Command = new SQLiteCommand();
                Command.Connection = TempConnection;
                //--------------
                TempConnection.Open();
                Command.CommandText = $"INSERT INTO Wares (Name, Type, Price, PurchasePrice, Amount, TotalPrice) VALUES ('{Name}', '{Type}', '{Price}', '{PurchasePrice}', '{Amount}', '{TotalPrice}');";
                Command.ExecuteNonQuery();
                TempConnection.Close();
            }
        }
        public void CertainTypeProductsDataExtraction() //+++ =================================================================
        {
            CertainTypeProductsData.Clear();
            //-----------
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            //-----------
            TempConnection.Open();
            Command.CommandText = @$"SELECT * FROM Wares WHERE Type = '{CertainTypeProductsObject.ProductTypeTextBox.Text}';";
            var CertainTypeProductsReader = Command.ExecuteReader();
            while (CertainTypeProductsReader.Read())
            {
                CertainTypeProductsData.Add(new MainTable(Convert.ToInt32(CertainTypeProductsReader.GetInt64(0)), CertainTypeProductsReader.GetString(1), CertainTypeProductsReader.GetString(2), CertainTypeProductsReader.GetDouble(3), CertainTypeProductsReader.GetDouble(4), Convert.ToInt32(CertainTypeProductsReader.GetInt64(5)), CertainTypeProductsReader.GetDouble(6)));
            }
            TempConnection.Close();
        }
        public void AllDataExtraction() //+++ =================================================================
        {
            Data.Clear();
            //-----------
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            //-----------
            TempConnection.Open();
            Command.CommandText = @"SELECT * FROM Wares";
            var Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Data.Add(new MainTable(Convert.ToInt32(Reader.GetInt64(0)), Reader.GetString(1), Reader.GetString(2), Reader.GetDouble(3), Reader.GetDouble(4), Convert.ToInt32(Reader.GetInt64(5)), Reader.GetDouble(6)));
            }
            TempConnection.Close();
        }
        public void SoldProductsDataExtraction() //+++ =================================================================
        {
            SoldProductsData.Clear();
            //-----------
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            //-----------
            TempConnection.Open();
            Command.CommandText = @"SELECT * FROM SoldWares";
            var SoldProductsReader = Command.ExecuteReader();
            while (SoldProductsReader.Read())
            {
                SoldProductsData.Add(new SoldProductsTable(Convert.ToInt32(SoldProductsReader.GetInt64(0)), SoldProductsReader.GetString(1), SoldProductsReader.GetString(2), SoldProductsReader.GetDouble(3), Convert.ToInt32(SoldProductsReader.GetInt64(4)), SoldProductsReader.GetDouble(5)));
            }
            TempConnection.Close();
        }
        public void AllTypeDataExtraction() //+++ =================================================================
        {
            //-----------
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            //-----------
            TempConnection.Open();
            Command.CommandText = @"SELECT * FROM WareTypes";
            var TypeReader = Command.ExecuteReader();
            while (TypeReader.Read())
            {
                TypeData.Add(new TypeTable(Convert.ToInt32(TypeReader.GetInt64(0)), TypeReader.GetString(1)));
            }
            TempConnection.Close();
        }
        public void AllProductsDeletion() //+++
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
        public void ProductDeletion(ProductsSelling productsSelling, MainWindow TempObject) //+++ =================================================================
        {
            if (Convert.ToInt32(productsSelling.ProductQuantityTextBox.Text) == 0 || Convert.ToInt32(productsSelling.ProductQuantityTextBox.Text) < 0)
            {
                InformationWindow DatabaseInformationWindow = new InformationWindow();
                DatabaseInformationWindow.InformationLabel.Content = "The number is incorrect";
                DatabaseInformationWindow.Show();
            }
            else
            {
                double TotalPrice;
                //--------------
                SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
                SQLiteCommand Command = new SQLiteCommand();
                TotalPrice = Convert.ToDouble(productsSelling.PriceSellingProductTextBox.Text) * Convert.ToInt32(productsSelling.ProductQuantityTextBox.Text);
                Command.Connection = TempConnection;
                //--------------
                TempConnection.Open();
                Command.CommandText = @$"UPDATE Wares SET Amount = (SELECT Amount FROM Wares WHERE Name = '{productsSelling.NameOfSellingProductTextBox.Text}' AND Type = '{productsSelling.TypeOfSellingProductTextBox.Text}') - {productsSelling.ProductQuantityTextBox.Text} WHERE Name = '{productsSelling.NameOfSellingProductTextBox.Text}' AND Type = '{productsSelling.TypeOfSellingProductTextBox.Text}';";
                Command.ExecuteNonQuery();
                Command.CommandText = @"DELETE FROM Wares WHERE Amount < 1;";
                Command.ExecuteNonQuery();
                Command.CommandText = $"INSERT INTO SoldWares (Name, Type, Price, Amount, TotalPrice) VALUES ('{productsSelling.NameOfSellingProductTextBox.Text}', '{productsSelling.TypeOfSellingProductTextBox.Text}', '{productsSelling.PriceSellingProductTextBox.Text}', '{productsSelling.ProductQuantityTextBox.Text}', '{TotalPrice}');";
                Command.ExecuteNonQuery();
                TempConnection.Close();
                AllDataExtraction();
                SoldProductsDataExtraction();
                TempObject.databaseMainGrid.ItemsSource = null;
                AllSoldProducts.SoldProductsGrid.ItemsSource = null;
                TempObject.databaseMainGrid.ItemsSource = Data;
                AllSoldProducts.SoldProductsGrid.ItemsSource = SoldProductsData;
                InformationWindow DatabaseInformationWindow = new InformationWindow();
                DatabaseInformationWindow.InformationLabel.Content = "Product sold";
                DatabaseInformationWindow.Show();
            }
        }
    }
}