using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Helpers.connection;
using Helpers.Criptografia;

namespace OpenEcommerceDLL.user
{
    public class User
    {
        public string table = "Usuario";
        [Key]
        public string? UserId { get; set; }
        [Save]
        public string? Name { get; set; }
        [Save]
        public string? LastName { get; set; }
        [Save]
        public string? Password { get; set; }
        [Save]
        public string? Email { get; set; }
        [Save]
        public DateTime? RegistrationDate { get; set; }
        [Save]
        public string? Phone { get; set; }

        public string? Token { get; set; }

        public DateTime? LastLogin { get; set; }
    }


}
