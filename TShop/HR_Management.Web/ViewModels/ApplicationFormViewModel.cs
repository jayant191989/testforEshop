using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class ApplicationFormViewModel
    {
        public ApplicationFormViewModel()
        {
            applicationFormStatus = ApplicationFormStatus.Creating;
        }
        public Guid Id { get; set; }

        [Display(Name = "Date")]
        public DateTime CurrentDate { get; set; }
        public string Status
        {
            get { return applicationFormStatus.ToString(); }
        }
        public string Type { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? TillDate { get; set; }

        [Display(Name = "Employee Name")]
        public int? EmployeeId { get; set; }

        [Display(Name = "Status")]
        public ApplicationFormStatus applicationFormStatus { get; set; }
        public PromotionResult ClaimWorkListItem(string userId)
        {
            PromotionResult promotionResult = new PromotionResult { Success = true };

            if (!promotionResult.Success)
            {             
                return promotionResult;
            }

            switch (applicationFormStatus)
            {
                case ApplicationFormStatus.Rejected:
                    promotionResult = PromoteToProcessing();
                    break;

                case ApplicationFormStatus.Created:
                    promotionResult = PromoteToProcessing();
                    break;

                case ApplicationFormStatus.Processed:
                    promotionResult = PromoteToApproving();
                    break;            
            }

          
            return promotionResult;
        }
        public PromotionResult PromoteWorkListItem(string command)
        {
            PromotionResult promotionResult = new PromotionResult();

            switch (command)
            {
                case "PromoteToCreated":
                    promotionResult = PromoteToCreated();
                    break;

                case "PromoteToProcessed":
                    promotionResult = PromoteToProcessed();
                    break;                                 

                case "PromoteToApproved":
                    promotionResult = PromoteToApproved();
                    break;

                case "DemoteToCreated":
                    promotionResult = DemoteToCreated();
                    break;

                case "DemoteToRejected":
                    promotionResult = DemoteToRejected();
                    break;

                case "DemoteToCanceled":
                    promotionResult = DemoteToCanceled();
                    break;
            }           

            return promotionResult;
        }
        private PromotionResult PromoteToCreated()
        {
            PromotionResult promotionResult = new PromotionResult();
            promotionResult.Success = true;

            if (applicationFormStatus != ApplicationFormStatus.Creating)
            {
                promotionResult.Success = false;
                promotionResult.Message = "Failed to promote the work order to Created status because its current status prevented it.";
            }
         
            if (promotionResult.Success)
            {
                applicationFormStatus = ApplicationFormStatus.Created;
                promotionResult.Message = String.Format("Work order {0} successfully promoted to status {1}.", Id, applicationFormStatus);
            }

            return promotionResult;
        }
        private PromotionResult PromoteToProcessing()
        {
            if (applicationFormStatus == ApplicationFormStatus.Created || applicationFormStatus == ApplicationFormStatus.Rejected)
            {
                applicationFormStatus = ApplicationFormStatus.Processing;
            }

            PromotionResult promotionResult = new PromotionResult();
            promotionResult.Success = applicationFormStatus == ApplicationFormStatus.Processing;

            if (promotionResult.Success)
                promotionResult.Message = String.Format("Application form {0} successfully Promoted by {1} and promoted to status {2}.",Id,  HttpContext.Current.User.Identity.Name,applicationFormStatus);
            else
                promotionResult.Message = "Failed to promote the work order to Processing status because its current status prevented it.";

            return promotionResult;
        }
        private PromotionResult PromoteToProcessed()
        {
            PromotionResult promotionResult = new PromotionResult();
            promotionResult.Success = true;

            if (applicationFormStatus != ApplicationFormStatus.Processing)
            {
                promotionResult.Success = false;
                promotionResult.Message = "Failed to promote the work order to Processed status because its current status prevented it.";
            }
           

            if (promotionResult.Success)
            {
                applicationFormStatus = ApplicationFormStatus.Processed;
                promotionResult.Message = String.Format("Work order {0} successfully promoted to status {1}.", Id, applicationFormStatus);
            }

            return promotionResult;
        }
        private PromotionResult PromoteToApproving()
        {
            if (applicationFormStatus == ApplicationFormStatus.Processed)
            {
                applicationFormStatus = ApplicationFormStatus.Approving;
            }

            PromotionResult promotionResult = new PromotionResult();
            promotionResult.Success = applicationFormStatus == ApplicationFormStatus.Approving;

            if (promotionResult.Success)
                promotionResult.Message = String.Format("Work order {0} successfully claimed by {1} and promoted to status {2}.",
                    Id,
                    HttpContext.Current.User.Identity.Name,
                    applicationFormStatus);
            else
                promotionResult.Message = "Failed to promote the work order to Approving status because its current status prevented it.";

            return promotionResult;
        }
        private PromotionResult PromoteToApproved()
        {
            PromotionResult promotionResult = new PromotionResult();
            promotionResult.Success = true;

            if (applicationFormStatus != ApplicationFormStatus.Approving && applicationFormStatus != ApplicationFormStatus.Processed)
            {
                promotionResult.Success = false;
                promotionResult.Message = "Failed to promote the work order to Approved status because its current status prevented it.";
            }

            if (promotionResult.Success)
            {
                applicationFormStatus = ApplicationFormStatus.Approved;
                promotionResult.Message = String.Format("Work order {0} successfully promoted to status {1}.", Id, applicationFormStatus);
            }

            return promotionResult;
        }
        private PromotionResult DemoteToCreated()
        {
            PromotionResult promotionResult = new PromotionResult();
            promotionResult.Success = true;

            if (applicationFormStatus != ApplicationFormStatus.Approving)
            {
                promotionResult.Success = false;
                promotionResult.Message = "Failed to demote the work order to Created status because its current status prevented it.";
            }

            //if (String.IsNullOrWhiteSpace(ReworkNotes))
            //{
            //    promotionResult.Success = false;
            //    promotionResult.Message = "Failed to demote the work order to Created status because Rework Notes must be present.";
            //}

            if (promotionResult.Success)
            {
                applicationFormStatus = ApplicationFormStatus.Created;
                promotionResult.Message = String.Format("Work order {0} successfully demoted to status {1}.", Id, applicationFormStatus);
            }

            return promotionResult;
        }
        private PromotionResult DemoteToRejected()
        {
            PromotionResult promotionResult = new PromotionResult();
            promotionResult.Success = true;

            if (applicationFormStatus != ApplicationFormStatus.Approving)
            {
                promotionResult.Success = false;
                promotionResult.Message = "Failed to demote the work order to Rejected status because its current status prevented it.";
            }

            if (promotionResult.Success)
            {
                applicationFormStatus = ApplicationFormStatus.Rejected;
                promotionResult.Message = String.Format("Work order {0} successfully demoted to status {1}.", Id, applicationFormStatus);
            }

            return promotionResult;
        }
        private PromotionResult DemoteToCanceled()
        {
            PromotionResult promotionResult = new PromotionResult();
            promotionResult.Success = true;

            if (applicationFormStatus != ApplicationFormStatus.Approving)
            {
                promotionResult.Success = false;
                promotionResult.Message = "Failed to demote the work order to Canceled status because its current status prevented it.";
            }

            if (promotionResult.Success)
            {
                applicationFormStatus = ApplicationFormStatus.Canceled;
                promotionResult.Message = String.Format("Work order {0} successfully demoted to status {1}.", Id, applicationFormStatus);
            }

            return promotionResult;
        }
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