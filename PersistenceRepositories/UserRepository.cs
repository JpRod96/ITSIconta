using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceRepositories
{
    class UserRepository
    {
        private const string TABLE_CREATION_COMMAND = "CREATE TABLE IF NOT EXISTS users" +
                                                    "(" +
                                                    "user_id INTEGER PRIMARY KEY, " +
                                                    "userName NVARCHAR(2048) NOT NULL," +
                                                    "password NVARCHAR(2048) NOT NULL," +
                                                    "email NVARCHAR(2048) NOT NULL," +
                                                    "role_Id INTEGER NOT NULL," +
                                                    "FOREIGN KEY (role_id) " +
                                                    "REFERENCES roles(role_id)" +
                                                    ")";
        private readonly DataAccess dataAccess;

        public UserRepository(DataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public void CreateTable()
        {
            dataAccess.ExecuteSQLCommand(TABLE_CREATION_COMMAND);
        }
    }
}
