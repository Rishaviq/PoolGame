using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Repositories.Interfaces.User
{
    public class UserFilter
    {
        public SqlInt32? UserId { get; set; }
        public SqlString? Username { get; set; }
        public SqlString? ProfileName { get; set; }


    }
}
