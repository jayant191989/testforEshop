using HR_Management.Context.Configuration;
using HR_Management.Model.Common;
using HR_Management.Model.Models;
using HR_Management.Models;
using HR_Management.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR_Management.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //: base("DefaultConnection", throwIfV1Schema: false)
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Company> Companyies { get; set; }
        public DbSet<Attendence> Attendences { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<EmployeeAttendence> EmployeeAttendences { get; set; }
        public DbSet<ApplicationForm> ApplicationForms { get; set; }
        public DbSet<EmployeeSalaryDetail> EmployeeSalaryDetails { get; set; }
        public DbSet<Salary> Salary { get; set; }
     //   public DbSet<Customer> Customers { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<EnrollCustomer> EnrollCustomers { get; set; }
        public DbSet<CustomerFees> CustomerFees { get; set; }
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductsAttribute> ProductsAttributes { get; set; }
        public DbSet<ProductAttributeOptions> ProductAttributeOptions { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<SyncLog> SyncLogs { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Batch> Batch { get; set; }
        public DbSet<Daily> Daily { get; set; }
        public DbSet<Particular> Particular { get; set; }
        public DbSet<DailyItem> DailyItem { get; set; }
        public DbSet<UserCart> UserCarts { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserOrder> UserOrders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new DepartmentConfiguration());
            modelBuilder.Configurations.Add(new ContactConfiguration());
            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new AttendenceConfiguration());
            modelBuilder.Configurations.Add(new LogEntryConfiguration());
            modelBuilder.Configurations.Add(new BankAccountConfiguration());
            modelBuilder.Configurations.Add(new BranchConfiguration());
            modelBuilder.Configurations.Add(new EmployeeAttendenceConfiguration());
            modelBuilder.Configurations.Add(new ApplicationFormConfiguration());
            modelBuilder.Configurations.Add(new EmployeeSalaryDetailConfiguration());
            modelBuilder.Configurations.Add(new SalaryConfiguration());
            modelBuilder.Configurations.Add(new MembershipConfiguration());
            //   modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new CustomerFeesConfiguration());
            modelBuilder.Configurations.Add(new EnrollCustomerConfiguration());
            modelBuilder.Configurations.Add(new EmployeeSalaryConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ProductAttributeConfiguration());
            modelBuilder.Configurations.Add(new ProductAttributeOptionsConfiguration());
            modelBuilder.Configurations.Add(new ProductVariantConfiguration());
            modelBuilder.Configurations.Add(new ProductCategoryConfiguration());
            modelBuilder.Configurations.Add(new SyncLogConfiguration());
            modelBuilder.Configurations.Add(new StoreConfiguration());
            modelBuilder.Configurations.Add(new StoreProductConfiguration());
            modelBuilder.Configurations.Add(new ProductImageConfiguration());
            modelBuilder.Configurations.Add(new BatchConfiguration());
            modelBuilder.Configurations.Add(new DailyConfiguration());
            modelBuilder.Configurations.Add(new ParticularConfiguration());
            modelBuilder.Configurations.Add(new DailyItemConfiguration());
            modelBuilder.Configurations.Add(new UserCartConfiguration());
            modelBuilder.Configurations.Add(new UserAddressConfiguration());
            modelBuilder.Configurations.Add(new UserOrderConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailConfiguration());
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new StateConfiguration());
            modelBuilder.Configurations.Add(new CityCounfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    string identityName = Thread.CurrentPrincipal.Identity.Name;
                    DateTime now = DateTime.Now;

                    if (entry.State == System.Data.Entity.EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }

            return base.SaveChanges();
        }

        //public System.Data.Entity.DbSet<HR_Management.Web.Models.ApplicationUser> ApplicationUsers { get; set; }
        //public System.Data.Entity.DbSet<HR_Management.Models.ApplicationRole> IdentityRoles { get; set; }
    }
}
