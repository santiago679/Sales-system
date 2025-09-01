using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SalesSystem.DAL
{
    public abstract class BaseDAL
    {
        protected readonly string connectionString;

        // No recibe parámetros, lee directo desde App.config
        public BaseDAL()
        {
            connectionString = ConfigurationManager
                .ConnectionStrings["SalesSystemDB"]
                .ConnectionString;
        }
    }
}
