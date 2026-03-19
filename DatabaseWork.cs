using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows;

namespace Warehouse
{
    public class DatabaseWork
    {
        static internal List<MainTable> Data = new List<MainTable>();
        public static string currentDirectory = Directory.GetCurrentDirectory();
        public static string changedDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(currentDirectory, @"../../../"));
        public static string databaseFile = @"Data Source=" + changedDirectory + "Database.db";
        SQLiteConnection connection = new SQLiteConnection(databaseFile);
        SQLiteCommand command = new SQLiteCommand();
        public DatabaseWork()
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = @"CREATE TABLE Wares (id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Type TEXT, Price REAL, PurchasePrice REAL, Amount INTEGER, TotalPrice REAL);";
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void NewDatabaseTable(string Name)
        {
            connection.Open();
            command.CommandText = $@"CREATE TABLE {Name} (id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Type TEXT, Price REAL, PurchasePrice REAL, Amount INTEGER, TotalPrice REAL);";
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DataInsertion(string Name, string Type, double Price, double PurchasePrice, int Amount, double TotalPrice)
        {
            connection.Open();
                command.CommandText = $"INSERT INTO Wares (Name, Type, Price, PurchasePrice, Amount, TotalPrice) VALUES ('{Name}', '{Type}', '{Price}', '{PurchasePrice}', '{Amount}', '{TotalPrice}');";
                command.ExecuteNonQuery();
            MessageBox.Show("lll");
            connection.Close();
        }
        public void TempDataInsertion()
        {
            connection.Open();
            for (int i = 0; i < 100; i++)
            {
                command.CommandText = "INSERT INTO Wares (Name, Type, Price, PurchasePrice, Amount, TotalPrice) VALUES ('nnn', 'vvv', '123.5', '100.5', '100', '247');";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public void AllDataExtraction()
        {
            connection.Open();
            command.CommandText = @"SELECT * FROM Wares";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Data.Add(new MainTable(Convert.ToInt32(reader.GetInt64(0)), reader.GetString(1), reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), Convert.ToInt32(reader.GetInt64(5)), reader.GetDouble(6)));
            }
            connection.Close();
        }
        public void AllProductsDeletion()
        {
            connection.Open();
            command.CommandText = @"DELETE FROM Wares";
            command.ExecuteNonQuery();
            Data.Clear();
            connection.Close();
        }
    }
}