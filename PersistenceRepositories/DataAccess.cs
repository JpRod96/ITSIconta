using System;
using Microsoft.Data.Sqlite;
using Windows.Storage;
using System.IO;

namespace PersistenceRepositories
{
    public class DataAccess
    {
        private const string DB_EXTENSION = ".db";

        private readonly string DBName;
        private SqliteConnection DB;

        public DataAccess(string DBName)
        {
            this.DBName = DBName + DB_EXTENSION;
            GetConnection();
        }

        private void GetConnection()
        {
            string DBPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DBName);
            DB = new SqliteConnection($"Filename={DBPath}");
        }

        public async void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(DBName, CreationCollisionOption.OpenIfExists);
            GetConnection();
        }

        public void UpdateDatabase()
        {
            DeleteDBIfExists();
            InitializeDatabase();
        }

        public void ExecuteSQLCommand(string SQLCommand)
        {
            DB.Open();
            SqliteCommand command = new SqliteCommand(SQLCommand, DB);
            command.ExecuteReader();
            DB.Close();
        }

        public void ExecuteSQLCommand(SqliteCommand SQLCommand)
        {
            DB.Open();
            SqliteCommand command = new SqliteCommand(SQLCommand, DB);
            command.ExecuteReader();
            DB.Close();
        }

        private async void DeleteDBIfExists()
        {
            try
            {
                StorageFile DBFile = await ApplicationData.Current.LocalFolder.GetFileAsync(DBName);
                await DBFile.DeleteAsync();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Database does not exist");
            }
        }
    }
}
