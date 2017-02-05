using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class ApplicationForm : AuditableEntity<Guid>
    {
        public DateTime CurrentDate { get; set; }
        public ApplicationFormStatus applicationFormStatus { get; set; }
        public string Type { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? TillDate { get; set; }  
        public int? EmployeeId { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public IEnumerable<string> RolesWhichCanClaim
        {
            get
            {
                List<string> rolesWhichCanClaim = new List<string>();

                switch (applicationFormStatus)
                {
                    case ApplicationFormStatus.Created:
                        rolesWhichCanClaim.Add("Clerk");
                        rolesWhichCanClaim.Add("Manager");
                        break;

                    case ApplicationFormStatus.Processed:
                        rolesWhichCanClaim.Add("Manager");
                        rolesWhichCanClaim.Add("EcommerceAdmin");
                        break;


                    case ApplicationFormStatus.Rejected:
                        rolesWhichCanClaim.Add("Manager");
                        rolesWhichCanClaim.Add("EcommerceAdmin");
                        break;
                }

                return rolesWhichCanClaim;
            }
        }
        public enum ApplicationFormStatus
        {
            Creating = 5,
            Created = 10,
            Processing = 15,
            Processed = 20,
            Certifying = 25,
            Certified = 30,
            Approving = 35,
            Approved = 40,
            Rejected = -10,
            Canceled = -20
        }
    }
}
