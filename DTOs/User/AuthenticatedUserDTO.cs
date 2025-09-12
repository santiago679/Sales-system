using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesSystem.DTOs.User
{
    public class AuthenticatedUserDTO
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
