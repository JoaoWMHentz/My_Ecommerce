 
using Helpers.connection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEcommerceDLL.user
{
    public class UserDAO
    {
        public UserDAO(Connection con)
        {
            this.con = con;
        }

        private Connection con { get; set; }
        public User save(User user)
        {
            try
            {
                DbHelper.saveObjectDeleteInsert(con, user, User.table);
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public User getUser(string id)
        {
            User user = new User();
            user.UserId = id;
            try
            {
                return GetUsers(user).FirstOrDefault();
            }
            catch (Exception)
            {
                return user;
            }
        }

        public List<User> GetUsers(User user)
        {
            List<User> exit = new List<User>();
            foreach (User us in DbHelper.getObject(con, user, User.table))
            {
                exit.Add(us);
            }
            return exit;
        }
    }
}
