using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite1
{
    class DataAccess
    {
        public static void InitializeDatabase()
        {

            using (SqliteConnection db =
               new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                    "first_Name CHARACTER NULL," +
                    "last_Name CHARACTER NULL," +
                    "email NVACHAR(40) NULL)";



                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
        public static void AddData(string firstName, string lastName, string email)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO MyTable VALUES (NULL, @first_Name,@last_Name,@email);";
                insertCommand.Parameters.AddWithValue("@first_Name", firstName);
                insertCommand.Parameters.AddWithValue("@last_Name", lastName);
                insertCommand.Parameters.AddWithValue("@email", email);


                insertCommand.ExecuteReader();

                db.Close();

                
            }

        }
        public static List<String> GetData()
        {
            List<String> entries = new List<string>();
            using (SqliteConnection db = new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand selectcommand = new SqliteCommand(" SELECT first_Name,Last_Name,email from MyTable", db);
                SqliteDataReader query = selectcommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add($"{query.GetString(0)} {query.GetString(1)} {query.GetString(2)}");
                }
                db.Close();
            }
            return entries;
        }
    }
}
