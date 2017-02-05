using HR_Management.Context;
using HR_Management.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            HomeViewModel hvm = new HomeViewModel();
            List<CustomerDueFeesViewModel> customerDueFeesList = new List<CustomerDueFeesViewModel>();
            var enrolledCustomers = _dbContext.EnrollCustomers.ToList().OrderBy(c => c.FullName);
            foreach (var enrolledCustomer in enrolledCustomers)
            {
                CustomerDueFeesViewModel dueFeesViewModel = new CustomerDueFeesViewModel();
                dueFeesViewModel.CustomerName = enrolledCustomer.FullName;
                customerDueFeesList.Add(dueFeesViewModel);
                var customersFees = _dbContext.CustomerFees.Where(c => c.EnrollCustomerId == enrolledCustomer.Id);
                var membership = _dbContext.Memberships.Where(c => c.Id == enrolledCustomer.MembershipId).OrderBy(m => m.MembershipName).FirstOrDefault();
                ////due time               
                int enrolledCustomerStartMonth = enrolledCustomer.DateOfJoining.Value.Month;
                int enrolledCustomerEndMonth = enrolledCustomer.MembershipEndTime.Value.Month;
                if (enrolledCustomerEndMonth > enrolledCustomerStartMonth)
                {
                    int monthsSpend = DateTime.Now.Month - enrolledCustomerStartMonth;
                    int membershipMonths = enrolledCustomerEndMonth - enrolledCustomerStartMonth;
                    int dueTime = membershipMonths - monthsSpend;
                    dueFeesViewModel.DueTime = dueTime;
                    if (dueFeesViewModel.DueTime == 2)
                    {
                        dueFeesViewModel.Status = "Remind Him";
                    }
                    if (dueFeesViewModel.DueTime == 1)
                    {
                        dueFeesViewModel.Status = "Urgent";
                    }
                }
                else
                {
                    int monthsSpend = DateTime.Now.Month - enrolledCustomerStartMonth;
                    int monthsLeft = 12 - enrolledCustomerStartMonth;
                    int membershipMonths = monthsLeft + enrolledCustomerEndMonth;
                    dueFeesViewModel.DueTime = membershipMonths - monthsSpend;
                }

                if (customersFees.Count() == 0)
                {
                    dueFeesViewModel.DueFees = membership.Fees;
                    dueFeesViewModel.Status = "Full Fees Due";
                }
                foreach (var customerFee in customersFees)
                {
                    if (customerFee.DueFees == 0)
                    {
                        dueFeesViewModel.Status = "Paid";
                    }
                    dueFeesViewModel.DueFees = customerFee.DueFees;
                }

                dueFeesViewModel.MembershipName = membership.MembershipName;
                dueFeesViewModel.MembershipFees = membership.Fees;
                dueFeesViewModel.MembershipJoinDate = enrolledCustomer.DateOfJoining;
                IEnumerable<CustomerDueFeesViewModel> en = customerDueFeesList;
                hvm.CustomerDueFeesViewModel = en;
            }

            var customers = _dbContext.Contacts.Where(c => c.Status == true);
            var premiumnCustomers = _dbContext.Contacts.Where(c => c.Status == true && c.CustomerType=="2");
            hvm.NumberOfPremiumnCustomers = premiumnCustomers.Count();
            hvm.NumberOfCustomers = customers.Count();
            return View(hvm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}