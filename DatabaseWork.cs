using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace Warehouse
{
    public class DatabaseWork
    {
        static internal List<MainTable> Data = new List<MainTable>();
        static internal List<TypeTable> TypeData = new List<TypeTable>();
        MainWindow ChangeGrid;

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
            command.CommandText = @"CREATE TABLE IF NOT EXISTS SoldWares (id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Type TEXT, Price REAL, PurchasePrice REAL, Amount INTEGER, TotalPrice REAL, FOREIGN KEY (Type) REFERENCES WareTypes(Name));";
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void SetObject(MainWindow changeGrid)
        {
            ChangeGrid = changeGrid;
        }
        public void TypeDataInsertion(string Name = "")
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
        }
        public void DataInsertion(string Name = "", string Type = "", double Price = 0, double PurchasePrice = 0, int Amount = 0, double TotalPrice = 0)
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

        public void AllDataExtraction()
        {
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

        public void AllTypeDataExtraction()
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

        public void ProductDeletion(ProductsSelling productsSelling) //===============================================================
        {
            //--------------
            SQLiteConnection TempConnection = new SQLiteConnection(databaseFile);
            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = TempConnection;
            //--------------
            TempConnection.Open();
            if (Convert.ToInt32(productsSelling.ProductQuantityTextBox.Text) == 1)
            {
                Command.CommandText = @$"DELETE FROM Wares WHERE Name = '{productsSelling.NameOfSellingProductTextBox.Text}' AND Type = '{productsSelling.TypeOfSellingProductTextBox.Text}';";
                Command.ExecuteNonQuery();
                /*ChangeGrid.databaseMainGrid.ItemsSource = null;
                AllDataExtraction();
                ChangeGrid.databaseMainGrid.ItemsSource = Data;*/
            
            }
            else
            {
                Command.CommandText = @$"UPDATE Wares SET Amount = (SELECT Amount FROM Wares WHERE Name = '{productsSelling.NameOfSellingProductTextBox.Text}' AND Type = '{productsSelling.TypeOfSellingProductTextBox.Text}');";
                Command.ExecuteNonQuery();
                MessageBox.Show("JJJ");
            }
            /*Command.CommandText = $"INSERT INTO SoldWares (Name, Type, Price, PurchasePrice, Amount, TotalPrice) VALUES (SELECT Name, Type, Price, PurchasePrice, Amount, TotalPrice FROM Wares WHERE Name = '{productsSelling.NameOfSellingProductTextBox.Text}' AND Type = '{productsSelling.TypeOfSellingProductTextBox.Text}');";
            Command.ExecuteNonQuery();*/
            TempConnection.Close();
            
        }

    }
}