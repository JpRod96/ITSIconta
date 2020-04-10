using System;
using Microsoft.Data.Sqlite;
using Windows.Storage;
using System.IO;
using System.Collections.Generic;

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
            CreateTables();
            Seed();
        }

        private void CreateTables()
        {
            List<Repository> repositories = InitializedRepositories();
            foreach(Repository repository in repositories)
            {
                repository.CreateTable();
            }
        }

        private void Seed()
        {
            Seeder seeder = new Seeder()
            {
                roleRepository = new RoleRepository(this),
                userRepository = new UserRepository(this)
            };
            seeder.Seed();
        }

        private List<Repository> InitializedRepositories()
        {
            List<Repository> repositories = new List<Repository>();
            repositories.Add(new RoleRepository(this));
            repositories.Add(new UserRepository(this));
            return repositories;
        }

        public void UpdateDatabase()
        {
            DeleteDBIfExists();
            InitializeDatabase();
        }

        public SqliteDataReader ExecuteSQLCommand(string SQLCommand)
        {
            SqliteDataReader output;
            DB.Open();
            SqliteCommand command = new SqliteCommand(SQLCommand, DB);
            output = command.ExecuteReader();
            DB.Close();
            return output;
        }

        public SqliteDataReader ExecuteSQLCommand(SqliteCommand SQLCommand)
        {
            SqliteDataReader output;
            DB.Open();
            SQLCommand.Connection = DB;
            output = SQLCommand.ExecuteReader();
            DB.Close();
            return output;
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
