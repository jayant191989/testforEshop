using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.ShoppingCartBL.ViewModels
{
    public class UserAddressViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
      
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string Company { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Mobile { get; set; }
        public string NearestLandMark { get; set; }
        public string AddressTitle { get; set; }
        public string AddressBlock
        {
            get
            {
                string addressBlock = string.Format("{0}<br/>{1}, {2} {3}", AddressLine1, City, State, ZipCode).Trim();
                return addressBlock == "<br/>," ? string.Empty : addressBlock;
            }
        }
        public string ApplicationUserId { get; set; }
        
    }

    public class UserAddressesList
    {
        public IEnumerable<UserAddressViewModel> UserAddressViewModelList { get; set; }

    }
}
