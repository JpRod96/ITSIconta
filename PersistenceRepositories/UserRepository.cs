using Entities.User;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace PersistenceRepositories
{
    public class UserRepository : Repository
    {
        public UserRepository(DataAccess dataAccess) : base(dataAccess)
        {
            base.TABLE_CREATION_COMMAND = "CREATE TABLE IF NOT EXISTS users" +
                                        "(" +
                                        "user_id INTEGER PRIMARY KEY, " +
                                        "userName NVARCHAR(20) NOT NULL," +
                                        "password NVARCHAR(20) NOT NULL," +
                                        "email NVARCHAR(40) NOT NULL," +
                                        "role_Id INTEGER NOT NULL," +
                                        "FOREIGN KEY (role_id) " +
                                        "REFERENCES roles(role_id)" +
                                        ")";
        }

        public User Save(User user)
        {
            return user;
        }
    }
}
