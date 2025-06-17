using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Repositories.Interfaces.User
{
    public class UserUpdate
    {
        public SqlString? ProfileName { get; set; }
        public SqlString? UserPassword { get; set; }
    }
}
