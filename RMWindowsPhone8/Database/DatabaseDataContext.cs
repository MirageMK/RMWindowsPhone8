using RMWindowsPhone8.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMWindowsPhone8.Database
{
    class DatabaseDataContext:DataContext
    {
        public static string DBConnectionString = "Data Source=isostore:/localDatabase.sdf";
        public DatabaseDataContext(string connectionString) : base(connectionString) { }

        public Table<GroupViewModel> groups;
    }
}
