using HR_Management.Context;
using HR_Management.ShoppingCartBL.ViewModels;
using HR_Management.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.ShoppingCartBL
{
    public class UserRepo
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public UserRepo() : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public UserRepo(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public bool MakeUserActive(string userEmail)
        {
            //var userManager =
            //   new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            bool flag = false;

            ApplicationUser user = UserManager.FindByEmail(userEmail);
            if (user != null)
            {
                user.IsActive = true;
                UserManager.Update(user);
                flag = true;
            }

            return flag;
        }
        public ApplicationUser GetUserInfo(string userEmail)
        {
            ApplicationUser user = UserManager.FindByEmail(userEmail);
            return user;
        }

        public int UpdateUserInfo(ApplicationUserViewModel model)
        {
            int value = 0;

            ApplicationUser user = UserManager.FindByEmail(model.Email);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                value = 1;
            }

            return value;
        }

        public IList<ApplicationUser> GetAdminUsers()
        {
            //List<ApplicationUser> modellst = new List<ApplicationUser>();
            var users = UserManager.Users.ToList();
            return users;
        }

        public int UpdateUser(ApplicationUser model)
        {
            int value = 0;

            ApplicationUser user = UserManager.FindByEmail(model.Email);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                value = 1;
            }
            return value;
        }

        public int DeleteUser(string Id)
        {
            int value = 0;
            using (var db = new ApplicationDbContext())
            {
                ApplicationUser user = UserManager.FindById(Id);
                if (user != null)
                {
                    UserManager.Delete(user);
                    value = 1;
                }
            }
            return value;
        }

        public ApplicationUser GetUserById(string id)
        {
            ApplicationUser user = UserManager.FindById(id);
            return user;
        }

        public bool IsUserAdmin(string email)
        {
            bool flag = false;
            var userRoles = UserManager.GetRoles(email);
            bool isThisUserAnEcommerceAdmin = userRoles.Contains("Admin");
           
            if (isThisUserAnEcommerceAdmin ==true)
            {
                flag = true;
            }
            return flag;
        }


    }
}
