using HR_Management.Context;
using HR_Management.ShoppingCartBL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.ShoppingCartBL
{
    public class CheckOutBL
    {
        public List<UserAddressViewModel> GetAddressList(string userEmail)
        {
            List<UserAddressViewModel> addressesList = new List<UserAddressViewModel>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var addresses = db.UserAddresses.Where(a => a.ApplicationUserEmail == userEmail);
                foreach (var address in addresses)
                {
                    UserAddressViewModel viewModel = new UserAddressViewModel();
                    viewModel.Id = address.Id;
                    viewModel.FirstName = address.FirstName;
                    viewModel.LastName = address.LastName;
                    viewModel.AddressLine1 = address.AddressLine1;
                    viewModel.AddressLine2 = address.AddressLine2;
                    viewModel.NearestLandMark = address.NearestLandMark;
                    viewModel.Mobile = address.Mobile;
                    viewModel.City = address.City;
                    viewModel.State = address.State;
                    viewModel.Country = address.Country;
                    viewModel.ZipCode = address.ZipCode;
                    viewModel.AddressTitle = address.AddressTitle;
                    viewModel.ApplicationUserId = userEmail;
                    addressesList.Add(viewModel);
                }
            }
            return addressesList;
        }
    }
}
