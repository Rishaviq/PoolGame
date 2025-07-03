using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PoolGame.Services.Helpers
{
    public class Validation
    {
        public bool IsValid { get; set; } = true;
        public string? ErrorMessage { get; set; }

        public Validation(string message)
        {
            IsValid = false;
            ErrorMessage = message;
        }
        public Validation()
        {
            IsValid = true;
            ErrorMessage = null;
        }
    }
}
