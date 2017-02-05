namespace HR_Management.Context.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Web.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<HR_Management.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HR_Management.Context.ApplicationDbContext context)
        {
            //Guid companyId = Guid.NewGuid();
            //var company = new List<Company>
            //            {
            //                new Company {   Id=companyId,  CompanyName = "ABC"},
            //            };
            //company.ForEach(s => context.Companyies.AddOrUpdate(s));
            //context.SaveChanges();

            var userManager =
                 new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            var roleManager =
                new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new ApplicationDbContext()));

            // Users Details
            string superAdminEmail = "superadmin@gmail.com";
            string AdminUserEmail = "admin@gmail.com";
            string managerAdminEmail = "manager@gmail.com";
            string password = "Password&*9";
            string SuperAdminfirstName = "SuperAdmin";
            string AdminfirstName = "Admin";
            string ManagerfirstName = "Manager";
            // Roles
            string roleSuperAdmin = "SuperAdmin";
            string roleAdmin = "Admin";
            string roleAdminsManager = "Manager";
            string roleClient = "Client";

            var role = roleManager.FindByName(roleSuperAdmin);
            var roleCheckAdmin = roleManager.FindByName(roleAdmin);
            var roleCheckAdminsManager = roleManager.FindByName(roleAdminsManager);
            var roleCheckClient = roleManager.FindByName(roleClient);

            // Creating Roles
            if (role == null)
            {
                role = new ApplicationRole(roleSuperAdmin);
                var roleResult = roleManager.Create(role);
            }
            if (roleCheckAdmin == null)
            {
                roleCheckAdmin = new ApplicationRole(roleAdmin);
                var roleResult = roleManager.Create(roleCheckAdmin);
            }
            if (roleCheckAdminsManager == null)
            {
                roleCheckAdminsManager = new ApplicationRole(roleAdminsManager);
                var roleResult = roleManager.Create(roleCheckAdminsManager);
            }
            if (roleCheckClient == null)
            {
                roleCheckClient = new ApplicationRole(roleClient);
                var roleResult = roleManager.Create(roleCheckClient);
            }

            //Creating Users
            var superAdminUser = userManager.FindByName(superAdminEmail);
            if (superAdminUser == null)
            {
                superAdminUser = new ApplicationUser { UserName = superAdminEmail, Email = superAdminEmail, FirstName = SuperAdminfirstName, EmailConfirmed = true};
                var result = userManager.Create(superAdminUser, password);
                result = userManager.SetLockoutEnabled(superAdminUser.Id, false);
            }

            var AdminUser = userManager.FindByName(AdminUserEmail);
            if (AdminUser == null)
            {
                AdminUser = new ApplicationUser { UserName = AdminUserEmail, Email = AdminUserEmail, FirstName = AdminfirstName, EmailConfirmed = true };
                var result = userManager.Create(AdminUser, password);
                result = userManager.SetLockoutEnabled(AdminUser.Id, false);
            }

            var managerUser = userManager.FindByName(managerAdminEmail);
            if (managerUser == null)
            {
                managerUser = new ApplicationUser { UserName = managerAdminEmail, Email = managerAdminEmail, FirstName = ManagerfirstName, EmailConfirmed = true};
                var result = userManager.Create(managerUser, password);
                result = userManager.SetLockoutEnabled(managerUser.Id, false);
            }

            ///Assigning User Roles
            var rolesForSuperUser = userManager.GetRoles(superAdminUser.Id);
            if (!rolesForSuperUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(superAdminUser.Id, role.Name);
            }

            var rolesForAdminUser = userManager.GetRoles(AdminUser.Id);
            if (!rolesForAdminUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(AdminUser.Id, roleCheckAdmin.Name);
            }

            var rolesForManagerUser = userManager.GetRoles(managerUser.Id);
            if (!rolesForManagerUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(managerUser.Id, roleCheckAdminsManager.Name);
            }

            //    /////////////////////////////////////////////////////////////////////
            //    var category = new List<AccountCategory>
            //                {
            //                    new AccountCategory { Id=1,  CategoryName = "Real",      ParentCategoryId = null },
            //                    new AccountCategory { Id=2,  CategoryName = "Persenal", ParentCategoryId = null },
            //                    new AccountCategory {Id=3,   CategoryName = "Nominal", ParentCategoryId = null },
            //                    new AccountCategory {Id=4,   CategoryName = "Expenses", ParentCategoryId = 3  },
            //                    new AccountCategory {Id=5,   CategoryName = "Cash In Hand", ParentCategoryId = 1   },
            //                    new AccountCategory {Id=6,   CategoryName = "Debitors",    ParentCategoryId = 2 },
            //                    new AccountCategory {Id=7,   CategoryName = "Customer",     ParentCategoryId = 6 },
            //                    new AccountCategory {Id=8,   CategoryName = "Bank",     ParentCategoryId = 2},
            //                    new AccountCategory {Id=9,   CategoryName = "Capital Acct",     ParentCategoryId = 2},
            //                    new AccountCategory {Id=10,   CategoryName = "Assets",     ParentCategoryId =1 },
            //                    new AccountCategory {Id=11,   CategoryName = "Direct Expenses",     ParentCategoryId = 4},
            //                    new AccountCategory {Id=12,   CategoryName = "Indirect Expenses",     ParentCategoryId = 4},
            //                    new AccountCategory {Id=13,   CategoryName = "Stock",     ParentCategoryId = 1},
            //                    new AccountCategory {Id=14,   CategoryName = "Income",     ParentCategoryId = 3},
            //                    new AccountCategory {Id=15,   CategoryName = "Gain",     ParentCategoryId = 3} ,
            //                    new AccountCategory {Id=16,   CategoryName = "Damage Goods",     ParentCategoryId = 12 },
            //                     new AccountCategory {Id=16,   CategoryName = "Sales Account",     ParentCategoryId = 14 },
            //                     new AccountCategory {Id=18,   CategoryName = "Purchase Account",     ParentCategoryId = 11} ,
            //                    new AccountCategory {Id=19,   CategoryName = "Liabilities",     ParentCategoryId = 3} ,
            //                      new AccountCategory {Id=20,   CategoryName = "Current Liabilities",     ParentCategoryId = 19} ,
            //                    new AccountCategory {Id=21,   CategoryName = "Creditors",     ParentCategoryId = 2 } ,
            //                    new AccountCategory {Id=22,   CategoryName = "Supplier",     ParentCategoryId = 21} ,
            //                     new AccountCategory {Id=23,   CategoryName = "Duties N Taxes",     ParentCategoryId = 20},
            //                };
            //    category.ForEach(s => context.AccountCategories.AddOrUpdate(s));
            //    context.SaveChanges();


            //    //var account = new List<Account>
            //    //            {
            //    //                new Account {   Id=Guid.NewGuid(),  AccountName = "Cash", AccountCategoryId=5},
            //    //                new Account {   Id=Guid.NewGuid(),  AccountName = "Sales", AccountCategoryId=17},
            //    //                 new Account {   Id=Guid.NewGuid(),  AccountName = "Discount", AccountCategoryId=12},
            //    //                  new Account {   Id=Guid.NewGuid(),  AccountName = "Packaging", AccountCategoryId=12},
            //    //            };
            //    //account.ForEach(s => context.Accounts.AddOrUpdate(s));
            //    //context.SaveChanges();

            //    ////////////////////////////////////////////////////////////////

            //var voucherTypes = new List<VoucherTypes>
            //                {
            //                    new VoucherTypes {   Id=Guid.NewGuid(),  VoucherName = "Sale"},
            //                    new VoucherTypes {   Id=Guid.NewGuid(),  VoucherName = "Purchase"},
            //                     new VoucherTypes {   Id=Guid.NewGuid(),  VoucherName = "Recipt"},
            //                      new VoucherTypes {   Id=Guid.NewGuid(),  VoucherName = "Payment"},
            //                };
            //voucherTypes.ForEach(s => context.VoucherTypes.AddOrUpdate(s));
            //context.SaveChanges();

            //var unit = new List<Unit>
            //            {
            //                new Unit {   Id=1,  Name = "Kg"},
            //                new Unit {   Id=2,  Name = "Lt"},
            //                new Unit {   Id=3,  Name = "Pcs"},
            //                new Unit {   Id=4,  Name = "gm"},
            //                new Unit {   Id=5,  Name = "ml"},
            //            };
            //unit.ForEach(s => context.Units.AddOrUpdate(s));
            //context.SaveChanges();

            //var warehouse = new List<WareHouse>
            //            {
            //                new WareHouse {   Id=1,  Name = "Warehouse 1"},
            //                new WareHouse {   Id=2,  Name = "Warehouse 2"},

            //            };
            //warehouse.ForEach(s => context.WareHouses.AddOrUpdate(s));
            //context.SaveChanges();
        }
    }
}
