
using Helpers.connection;

namespace OpenEcommerceDLL.user
{
    public class User
    {
        public static string table = "AcessUser";
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
        [Save]
        public DateTime? LastLogin { get; set; }
        [Save]
        public string UserRole { get; set; }

        public bool? IsAuthenticate { get; set; }
    }


}
