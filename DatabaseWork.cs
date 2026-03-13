using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace Warehouse
{
    public class DatabaseWork
    {
        public static string currentDirectory = Directory.GetCurrentDirectory();
        public static string changedDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(currentDirectory, @"../../../"));
        public static string databaseFile = @"Data Source=" + changedDirectory + "Database.db";
        SQLiteConnection connection = new SQLiteConnection(databaseFile);
        public SQLiteCommand command = new SQLiteCommand();
        public DatabaseWork()
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = @"CREATE TABLE sells (id INTEGER PRIMARY KEY AUTOINCREMENT, amount INTEGER, txt TEXT);";
            command.ExecuteNonQuery();
            for (int i = 0; i < 10; i++)
            {
                command.CommandText = "INSERT INTO sells (amount, txt) VALUES ('" + Convert.ToString(i) + "', 'LLL');";
                command.ExecuteNonQuery();
            }
            command.CommandText = @"SELECT * FROM sells";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                MessageBox.Show(Convert.ToString(reader.GetInt64(0)) + Convert.ToString(reader.GetInt64(1)) + reader.GetString(2));
            }
            connection.Close();
        }
    }
}