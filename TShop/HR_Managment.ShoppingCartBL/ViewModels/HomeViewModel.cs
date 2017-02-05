using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.ShoppingCartBL.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<ProductViewModel> NewArrivalList { get; set; }
    }
}
