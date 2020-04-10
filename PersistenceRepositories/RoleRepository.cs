using Entities.User;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceRepositories
{
    public class RoleRepository : Repository
    {
        public RoleRepository(DataAccess dataAccess) : base(dataAccess)
        {
            base.TABLE_CREATION_COMMAND ="CREATE TABLE IF NOT EXISTS roles" +
                                        "(" +
                                        "role_id INTEGER PRIMARY KEY, " +
                                        "name NVARCHAR(20) NOT NULL" +
                                        ")";
        }

        public Role Save(Role role)
        {
            SqliteCommand insertCommand = new SqliteCommand();
            insertCommand.CommandText = "INSERT INTO roles (name) VALUES (@name);";
            insertCommand.Parameters.AddWithValue("@name", role.Name);
            SqliteDataReader query = dataAccess.ExecuteSQLCommand(insertCommand);
            return role;
        }
    }
}
