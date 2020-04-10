using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceRepositories
{
    public abstract class Repository
    {
        protected string TABLE_CREATION_COMMAND;
        protected readonly DataAccess dataAccess;

        public Repository(DataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public void CreateTable()
        {
            dataAccess.ExecuteSQLCommand(TABLE_CREATION_COMMAND);
        }
    }
}
