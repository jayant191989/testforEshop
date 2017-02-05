using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HR_Management.Context;
using HR_Management.Models;
using HR_Management.Web.ViewModels;
using HR_Management.Web.Helpers;
using System.IO;
using Newtonsoft.Json;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class DailyController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.Particular = new SelectList(_dbContext.Particular, "Id", "Name");
            //ViewBag.Products = new SelectList(_dbContext.Products, "Id", "Name");
            //ViewBag.Contacts = new SelectList(_dbContext.Contacts.OrderBy(c => c.FirstName), "Id", "FullName");
            DailyViewModel viewModelforHeadList = new DailyViewModel();
            ViewBag.HeadList = viewModelforHeadList.getList();
            return View();
        }

        public ActionResult GetDailyForDatatable()
        {
            using (var context = new ApplicationDbContext())
            {
                var dailyies = context.Daily.OrderByDescending(d => d.Date).ToList();
                List<DailyViewModel> dailyViewModelList = new List<DailyViewModel>();
                IEnumerable<DailyViewModel> dailyViewModelEnu;
                foreach (var daily in dailyies)
                {
                    DailyViewModel viewModel = new DailyViewModel()
                    {
                        Id = daily.Id,
                        Invoice = daily.Invoice,
                        Ledger = daily.Contact.FullName,
                        Particular = daily.Particular.Name,
                        Date = daily.Date,
                        Head = daily.HeadName,
                        Credit = daily.Credit,
                        Debit = daily.Debit,
                        Net = daily.Net
                    };
                    List<DailyItemViewModel> dailyItemViewModelList = new List<DailyItemViewModel>();
                    foreach (var item in daily.DailyItems)
                    {
                        DailyItemViewModel dailyItemViewModel = new DailyItemViewModel();
                        dailyItemViewModel.Id = item.Id;
                        dailyItemViewModel.Name = item.StoreProduct.Product.Name;
                        dailyItemViewModel.Quantity = item.Quantity;
                        dailyItemViewModel.MRPPerUnit = item.MRPPerUnit;
                        dailyItemViewModel.ItemAmount = item.ItemAmount;
                        dailyItemViewModelList.Add(dailyItemViewModel);
                    }
                    viewModel.DailyItemViewModels = dailyItemViewModelList;
                    dailyViewModelList.Add(viewModel);
                }
                dailyViewModelEnu = dailyViewModelList;
                var result = new
                {
                    iTotalRecords = dailyies.Count,
                    iTotalDisplayRecords = dailyies.Count,
                    aaData = dailyViewModelList
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DisplaySearchResults(string headName, Guid? particulartId, Guid? contactId, Guid? productId, DateTime? todate, DateTime? fromdate)
        {
            if (headName != null && headName != "--Select Head--")
            {
                var dailyByHeadName = _dbContext.Daily.Where(s => s.HeadName == headName).OrderBy(d => d.CreatedDate).ToList();
                List<DailyViewModel> dailyViewModelList = new List<DailyViewModel>();
                IEnumerable<DailyViewModel> dailyViewModelEnu;
                if (particulartId != null)
                {
                    var dailyByParticular = dailyByHeadName.Where(v => v.ParticularId == particulartId).OrderBy(d => d.CreatedDate).ToList();
                    if (todate != null && fromdate != null)
                    {
                        var tofromList = dailyByParticular.Where(d1 => d1.CreatedDate >= todate && d1.CreatedDate <= fromdate).OrderBy(d => d.CreatedDate).ToList();
                        foreach (var daily in tofromList)
                        {
                            DailyViewModel viewModel = new DailyViewModel()
                            {
                                Id = daily.Id,
                                Invoice = daily.Invoice,
                                Ledger = daily.Contact.FullName,
                                Particular = daily.Particular.Name,
                                Date = daily.Date,
                                Head = daily.HeadName,
                                Credit = daily.Credit,
                                Debit = daily.Debit,
                                Net = daily.Net
                            };
                            dailyViewModelList.Add(viewModel);
                        }
                        dailyViewModelEnu = dailyViewModelList;
                       // return PartialView("_FilterDaily", dailyViewModelEnu);
                        string modelString = RenderRazorViewToString("_FilterDaily", dailyViewModelEnu);
                        return Json(new { success = true, modelString }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        foreach (var daily in dailyByParticular)
                        {
                            DailyViewModel viewModel = new DailyViewModel()
                            {
                                Id = daily.Id,
                                Invoice = daily.Invoice,
                                Ledger = daily.Contact.FullName,
                                Particular = daily.Particular.Name,
                                Date = daily.Date,
                                Head = daily.HeadName,
                                Credit = daily.Credit,
                                Debit = daily.Debit,
                                Net = daily.Net
                            };
                            dailyViewModelList.Add(viewModel);
                        }
                        dailyViewModelEnu = dailyViewModelList;
                       // return PartialView("_FilterDaily", dailyViewModelEnu);
                        string modelString = RenderRazorViewToString("_FilterDaily", dailyViewModelEnu);
                        return Json(new { success = true, modelString }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (todate != null && fromdate != null)
                    {
                        var tofromList = dailyByHeadName.Where(d1 => d1.CreatedDate >= todate && d1.CreatedDate <= fromdate).OrderBy(d => d.CreatedDate).ToList();
                        foreach (var daily in tofromList)
                        {
                            DailyViewModel viewModel = new DailyViewModel()
                            {
                                Id = daily.Id,
                                Invoice = daily.Invoice,
                                Ledger = daily.Contact.FullName,
                                Particular = daily.Particular.Name,
                                Date = daily.Date,
                                Head = daily.HeadName,
                                Credit = daily.Credit,
                                Debit = daily.Debit,
                                Net = daily.Net
                            };
                            dailyViewModelList.Add(viewModel);
                        }
                        dailyViewModelEnu = dailyViewModelList;
                       // return PartialView("_FilterDaily", dailyViewModelEnu);
                        string modelString = RenderRazorViewToString("_FilterDaily", dailyViewModelEnu);
                        return Json(new { success = true, modelString }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        foreach (var daily in dailyByHeadName)
                        {
                            DailyViewModel viewModel = new DailyViewModel()
                            {
                                Id = daily.Id,
                                Invoice = daily.Invoice,
                                Ledger = daily.Contact.FullName,
                                Particular = daily.Particular.Name,
                                Date = daily.Date,
                                Head = daily.HeadName,
                                Credit = daily.Credit,
                                Debit = daily.Debit,
                                Net = daily.Net
                            };
                            dailyViewModelList.Add(viewModel);
                        }
                        dailyViewModelEnu = dailyViewModelList;
                      //  return PartialView("_FilterDaily", dailyViewModelEnu);
                        string modelString = RenderRazorViewToString("_FilterDaily", dailyViewModelEnu);
                        return Json(new { success = true, modelString }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else if (particulartId != null)
            {
                var dailyByParticular = _dbContext.Daily.Where(s => s.ParticularId == particulartId).OrderBy(d => d.CreatedDate).ToList();
                List<DailyViewModel> violationViewModelList = new List<DailyViewModel>();
                IEnumerable<DailyViewModel> dailyViewModelEnu;
                if (todate != null && fromdate != null)
                {
                    var tofromList = dailyByParticular.Where(d1 => d1.CreatedDate >= todate && d1.CreatedDate <= fromdate).ToList();
                    foreach (var daily in tofromList)
                    {
                        DailyViewModel viewModel = new DailyViewModel()
                        {
                            Id = daily.Id,
                            Invoice = daily.Invoice,
                            Ledger = daily.Contact.FullName,
                            Particular = daily.Particular.Name,
                            Date = daily.Date,
                            Head = daily.HeadName,
                            Credit = daily.Credit,
                            Debit = daily.Debit,
                            Net = daily.Net
                        };
                        violationViewModelList.Add(viewModel);
                    }
                    dailyViewModelEnu = violationViewModelList;
                   // return PartialView("_FilterDaily", violationViewModelEnu);
                    string modelString = RenderRazorViewToString("_FilterDaily", dailyViewModelEnu);
                    return Json(new { success = true, modelString }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var daily in dailyByParticular)
                    {
                        DailyViewModel viewModel = new DailyViewModel()
                        {
                            Id = daily.Id,
                            Invoice = daily.Invoice,
                            Ledger = daily.Contact.FullName,
                            Particular = daily.Particular.Name,
                            Date = daily.Date,
                            Head = daily.HeadName,
                            Credit = daily.Credit,
                            Debit = daily.Debit,
                            Net = daily.Net
                        };
                        violationViewModelList.Add(viewModel);
                    }
                    dailyViewModelEnu = violationViewModelList;
                  //  return PartialView("_FilterDaily", violationViewModelEnu);
                    string modelString = RenderRazorViewToString("_FilterDaily", dailyViewModelEnu);
                    return Json(new { success = true, modelString }, JsonRequestBehavior.AllowGet);
                }

            }
            else if (contactId != null)
            {
                var dailyByContact = _dbContext.Daily.Where(s => s.ContactId == contactId).OrderBy(d => d.CreatedDate).ToList();
                List<DailyViewModel> DailyViewModelList = new List<DailyViewModel>();
                IEnumerable<DailyViewModel> dailyViewModelEnu;
                if (todate != null && fromdate != null)
                {
                    var tofromList = dailyByContact.Where(d1 => d1.CreatedDate >= todate && d1.CreatedDate <= fromdate).ToList();
                    foreach (var daily in tofromList)
                    {
                        DailyViewModel viewModel = new DailyViewModel()
                        {
                            Id = daily.Id,
                            Invoice = daily.Invoice,
                            Ledger = daily.Contact.FullName,
                            Particular = daily.Particular.Name,
                            Date = daily.Date,
                            Head = daily.HeadName,
                            Credit = daily.Credit,
                            Debit = daily.Debit,
                            Net = daily.Net
                        };
                        DailyViewModelList.Add(viewModel);
                    }
                    dailyViewModelEnu = DailyViewModelList;
                   // return PartialView("_FilterDaily", dailyViewModelEnu);
                    string modelString = RenderRazorViewToString("_FilterDaily", dailyViewModelEnu);
                    return Json(new { success = true, modelString }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var daily in dailyByContact)
                    {
                        DailyViewModel viewModel = new DailyViewModel()
                        {
                            Id = daily.Id,
                            Invoice = daily.Invoice,
                            Ledger = daily.Contact.FullName,
                            Particular = daily.Particular.Name,
                            Date = daily.Date,
                            Head = daily.HeadName,
                            Credit = daily.Credit,
                            Debit = daily.Debit,
                            Net = daily.Net
                        };
                        DailyViewModelList.Add(viewModel);
                    }
                    dailyViewModelEnu = DailyViewModelList;

                    string modelString = RenderRazorViewToString("_FilterDaily", dailyViewModelEnu);
                    return Json(new { success = true, modelString }, JsonRequestBehavior.AllowGet);
                    //   return Json(new { ModelString = modelString, JsonRequestBehavior.AllowGet });
                    // return Json(result, JsonRequestBehavior.AllowGet);
                    // return PartialView("_FilterDaily", dailyViewModelEnu);
                }

            }
            else if (productId != null)
            {
                var dailyItems = _dbContext.DailyItem.Where(s => s.StoreProduct.ProductId == productId).OrderBy(d => d.CreatedDate).ToList();
                List<DailyViewModel> violationViewModelList = new List<DailyViewModel>();
                IEnumerable<DailyViewModel> dailyViewModelEnu;
                if (todate != null && fromdate != null)
                {
                    var tofromList = dailyItems.Where(d1 => d1.CreatedDate >= todate && d1.CreatedDate <= fromdate).ToList();
                    foreach (var daily in tofromList.Select(x => x.Daily))
                    {
                        DailyViewModel viewModel = new DailyViewModel()
                        {
                            Id = daily.Id,
                            Invoice = daily.Invoice,
                            Ledger = daily.Contact.FullName,
                            Particular = daily.Particular.Name,
                            Date = daily.Date,
                            Head = daily.HeadName,
                            Credit = daily.Credit,
                            Debit = daily.Debit,
                            Net = daily.Net
                        };
                        violationViewModelList.Add(viewModel);
                    }
                    dailyViewModelEnu = violationViewModelList;
                  //  return PartialView("_FilterDaily", violationViewModelEnu);
                    string modelString = RenderRazorViewToString("_FilterDaily", dailyViewModelEnu);
                    return Json(new { success = true, modelString }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var daily in dailyItems.Select(x => x.Daily))
                    {
                        DailyViewModel viewModel = new DailyViewModel()
                        {
                            Id = daily.Id,
                            Invoice = daily.Invoice,
                            Ledger = daily.Contact.FullName,
                            Particular = daily.Particular.Name,
                            Date = daily.Date,
                            Head = daily.HeadName,
                            Credit = daily.Credit,
                            Debit = daily.Debit,
                            Net = daily.Net
                        };
                        violationViewModelList.Add(viewModel);
                    }
                    dailyViewModelEnu = violationViewModelList;
                  //  return PartialView("_FilterDaily", violationViewModelEnu);
                    string modelString = RenderRazorViewToString("_FilterDaily", dailyViewModelEnu);
                    return Json(new { success = true, modelString }, JsonRequestBehavior.AllowGet);
                }

            }
            else if (todate != null && fromdate != null)
            {
                //  string toformatted = todate.Value.ToString("yyyy-MM-dd");                
                //   string fromformatted = fromdate.Value.ToString("yyyy-MM-dd");
                //  DateTime todateFromatted = DateTime.Parse(toformatted);
                //   DateTime fromdateFormatted = DateTime.Parse(fromdate.ToString());

                var dailyList = _dbContext.Daily.ToList();
                var tofromList = dailyList.Where(d1 => d1.CreatedDate >= todate && d1.CreatedDate <= fromdate).ToList();
                List<DailyViewModel> violationViewModelList = new List<DailyViewModel>();
                IEnumerable<DailyViewModel> dailyViewModelEnu;
                foreach (var daily in tofromList)
                {
                    DailyViewModel viewModel = new DailyViewModel()
                    {
                        Id = daily.Id,
                        Invoice = daily.Invoice,
                        Ledger = daily.Contact.FullName,
                        Particular = daily.Particular.Name,
                        Date = daily.Date,
                        Head = daily.HeadName,
                        Credit = daily.Credit,
                        Debit = daily.Debit,
                        Net = daily.Net
                    };
                    violationViewModelList.Add(viewModel);
                }
                dailyViewModelEnu = violationViewModelList;
              //  return PartialView("_FilterDaily", violationViewModelEnu);
                string modelString = RenderRazorViewToString("_FilterDaily", dailyViewModelEnu);
                return Json(new { success = true, modelString }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var dailies = _dbContext.Daily.OrderBy(d => d.CreatedDate).ToList();
                List<DailyViewModel> dailyViewModelList = new List<DailyViewModel>();
                IEnumerable<DailyViewModel> dailyViewModelEnu;
                foreach (var daily in dailies)
                {
                    DailyViewModel viewModel = new DailyViewModel()
                    {
                        Id = daily.Id,
                        Invoice = daily.Invoice,
                        Ledger = daily.Contact.FullName,
                        Particular = daily.Particular.Name,
                        Date = daily.Date,
                        Head = daily.HeadName,
                        Credit = daily.Credit,
                        Debit = daily.Debit,
                        Net = daily.Net
                    };
                    dailyViewModelList.Add(viewModel);
                }
                dailyViewModelEnu = dailyViewModelList;
              //  return PartialView("_FilterDaily", dailyViewModelEnu);
                string modelString = RenderRazorViewToString("_FilterDaily", dailyViewModelEnu);
                return Json(new { success = true, modelString }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Daily daily = _dbContext.Daily.Find(id);
            if (daily == null)
            {
                return HttpNotFound();
            }
            return View(daily);
        }

        public ActionResult GetInvoice(string headName)
        {
            DailyViewModel viewModel = new DailyViewModel();
            var dailyInvoice = _dbContext.Daily.Where(d => d.HeadName == headName).
                OrderBy(d => d.Invoice).ToList().LastOrDefault();
            decimal invoice;
            if (dailyInvoice == null)
            {
                invoice = 1;
            }
            else
            {
                invoice = dailyInvoice.Invoice + 1;
            }
            return Json(new { success = true, invoice = invoice }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMembershipDue(Guid contactId, Guid membershipId)
        {
            DailyViewModel viewModel = new DailyViewModel();
            Membership membership = _dbContext.Memberships.Find(membershipId);
            if (membershipId != null)
            {
                // decimal totalCustomerFees = 0;
                var dailyMemberShipFeeOfContact = _dbContext.Daily.Where(d => d.ContactId == contactId && d.MembershipId == membershipId).ToList();
                if (dailyMemberShipFeeOfContact.Count == 0)
                {
                    viewModel.Total = membership.Fees;
                }
                if (dailyMemberShipFeeOfContact.Count != 0)
                {
                    var dailyLastDue = dailyMemberShipFeeOfContact.LastOrDefault();
                    viewModel.Total = dailyLastDue.Due;
                }
            }

            if (membership == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                viewModel.Due = membership.Fees;
            }
            return Json(new { success = true, Total = viewModel.Total }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            DailyViewModel viewModel = new DailyViewModel();
            ViewBag.BatchId = new SelectList(_dbContext.Batch, "Id", "Name");
            viewModel.GetHeadList = viewModel.getList();
            ViewBag.Memberships = new SelectList(_dbContext.Memberships, "Id", "MembershipName");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string data, string HeadName, decimal Invoice, DateTime Date, Guid ContactId, Guid ParticularId, Guid? MembershipId, decimal Total)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Daily daily = new Daily();
                    Guid dailyId = Guid.NewGuid();
                    daily.HeadName = HeadName;
                    daily.Id = dailyId;
                    daily.Invoice = Invoice;
                    daily.Date = Date;
                    daily.ContactId = ContactId;
                    daily.ParticularId = ParticularId;
                    daily.MembershipId = MembershipId;
                    daily.DailyTotal = Total;
                    if (HeadName == "Service")
                    {
                        daily.Debit = Total;
                        daily.Credit = 0;
                        //if (Net != null)
                        //{
                        //    daily.Debit = Total;
                        //    daily.Net = 0 - daily.Debit + Net;
                        //}
                        //else
                        //{
                        //    daily.Net = 0 - daily.Debit;
                        //}

                    }
                    if (HeadName == "Sales")
                    {
                        daily.Debit = Total;
                        daily.Credit = 0;
                        //if (Net != null)
                        //{
                        //    daily.Debit = Total;
                        //    daily.Net = 0 - daily.Debit + Net;
                        //}
                        //else
                        //{
                        //    daily.Net = 0 - daily.Debit;
                        //}
                    }
                    if (HeadName == "Expense")
                    {
                        daily.Debit = Total;
                        daily.Credit = 0;
                        //if (Net != null)
                        //{
                        //    daily.Debit = Total;
                        //    daily.Net = 0 - daily.Debit + Net;
                        //}
                        //else
                        //{
                        //    daily.Net = 0 - daily.Debit;
                        //}
                    }

                    if (HeadName == "Revenue")
                    {
                        daily.Credit = Total;
                        daily.Debit = 0;
                        //if (Net != null)
                        //{
                        //    daily.Net = Net + daily.Credit;
                        //}
                        //else
                        //{
                        //    daily.Net = daily.Credit - 0;
                        //}
                    }

                    SaveDailyItems(dailyId, data);

                    _dbContext.Daily.Add(daily);
                    _dbContext.SaveChanges();
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    var msg = new ModelStateException(ex);
                    return Json(new { success = false, message = msg.Message });
                    //ModelState.AddModelError("", msg);
                    //TempData["MessageToClientError"] = msg;
                }

            }
            return View();
        }

        private void SaveDailyItems(Guid dailyId, string data)
        {
            var deserialiseList = JsonConvert.DeserializeObject<List<DailyItem>>(data);
            foreach (var item in deserialiseList)
            {
                DailyItem dailyItem = new DailyItem()
                {
                    Id = Guid.NewGuid(),
                    MRPPerUnit = item.MRPPerUnit,
                    Quantity = item.Quantity,
                    ItemAmount = item.ItemAmount,
                    StoreProductId = item.StoreProductId,
                    DailyId = dailyId
                };
                StoreProduct storeProduct = _dbContext.StoreProducts.Where(s => s.Id == item.StoreProductId).FirstOrDefault();
                storeProduct.Quantity = storeProduct.Quantity - item.Quantity;
                _dbContext.Entry(storeProduct).State = EntityState.Modified;
                _dbContext.DailyItem.Add(dailyItem);
            }
        }

        public ActionResult GetContactDueHistory(Guid contactId)
        {
            ContactPaymentHistory contactPaymentHistory = new ContactPaymentHistory();
            decimal? total = 0;
            decimal? totalAmtTaken = 0;
            decimal? outStanding = 0;
            var contactDues = _dbContext.Daily.
                Where(d => d.ContactId == contactId).OrderBy(d => d.CreatedDate).ToList();

            List<DailyViewModel> customerDueFeesList = new List<DailyViewModel>();
            IEnumerable<DailyViewModel> dailyViewModelEnu;
            decimal? creditTotal = 0;
            decimal? debitTotal = 0;
            foreach (var contactPayment in contactDues)
            {
                creditTotal += contactPayment.Credit;
                debitTotal += contactPayment.Debit;
                DailyViewModel viewModel = new DailyViewModel()
                {
                    Head = contactPayment.HeadName,
                    Invoice = contactPayment.Invoice,
                    CustomerName = contactPayment.Contact.FullName,
                    Particular = contactPayment.Particular.Name,
                    Date = contactPayment.Date,
                    Total = contactPayment.DailyTotal,
                    Credit = contactPayment.Credit,
                    Debit = contactPayment.Debit,
                    Due = contactPayment.Due,
                    Net = creditTotal - debitTotal
                };

                total = viewModel.Total + total;
                totalAmtTaken = viewModel.Credit + totalAmtTaken;
                customerDueFeesList.Add(viewModel);
            }
            contactPaymentHistory.Total = total;
            contactPaymentHistory.TotalAmountTaken = totalAmtTaken;
            outStanding = total - totalAmtTaken;
            contactPaymentHistory.OutStanding = outStanding;
            dailyViewModelEnu = customerDueFeesList;
            contactPaymentHistory.DailyItemViewModel = dailyViewModelEnu;
            string modelString = RenderRazorViewToString("_GetContactDueHistory", contactPaymentHistory);
            return Json(new
            {
                ModelString = modelString
            }, JsonRequestBehavior.AllowGet);
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext =
                     new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Daily daily = _dbContext.Daily.Find(id);
            if (daily == null)
            {
                return HttpNotFound();
            }
            DailyViewModel viewModel = new DailyViewModel()
            {
                Head = daily.HeadName,
                Id = daily.Id,
                Invoice = daily.Invoice,
                Date = daily.Date,
                ContactId = daily.ContactId,
                CustomerName = daily.Contact.FullName,
                ParticularId = daily.ParticularId,
                Particular = daily.Particular.Name,
                Total = daily.DailyTotal,
                Due = daily.Due,
                Note = daily.Note,
                // Net = daily.Net
            };
            List<DailyItemViewModel> dailyItemViewModelList = new List<DailyItemViewModel>();
            foreach (var item in daily.DailyItems)
            {
                DailyItemViewModel dailyItemViewModel = new DailyItemViewModel()
                {
                    Id = item.Id,
                    BatchId = item.StoreProduct.BatchId,
                    BatchName = item.StoreProduct.Batch.Name,
                    AutoGenerateName = item.StoreProduct.Product.AutoGenerateName,
                    StoreProductId = item.StoreProductId,
                    MRPPerUnit = item.MRPPerUnit,
                    Quantity = item.Quantity,
                    ItemAmount = item.ItemAmount
                };
                dailyItemViewModelList.Add(dailyItemViewModel);
            }
            viewModel.DailyItemViewModels = dailyItemViewModelList;
            ViewBag.BatchId = new SelectList(_dbContext.Batch, "Id", "Name");

            ViewBag.Memberships = new SelectList(_dbContext.Memberships, "Id", "MembershipName");
            viewModel.Head = daily.HeadName;
            if (viewModel.Head == "Service")
            {
                viewModel.X = 1;
            }
            else if (viewModel.Head == "Sales")
            {
                viewModel.X = 2;
            }
            else if (viewModel.Head == "Expense")
            {
                viewModel.X = 3;
            }
            else
            {
                viewModel.X = 4;
            }
            viewModel.GetHeadList = viewModel.getList();
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string data, Guid dailyid, string HeadName, decimal Invoice, DateTime Date, Guid ContactId, Guid ParticularId, Guid? MembershipId, decimal Total)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Daily daily = _dbContext.Daily.Find(dailyid);
                    daily.HeadName = HeadName;
                    daily.Invoice = Invoice;
                    daily.Date = Date;
                    daily.ContactId = ContactId;
                    daily.ParticularId = ParticularId;
                    //  daily.Net = Net;
                    daily.MembershipId = MembershipId;
                    daily.DailyTotal = Total;

                    if (HeadName == "Service")
                    {
                        daily.Debit = Total;
                        daily.Credit = 0;
                        //if (Net != null)
                        //{
                        //    daily.Debit = Total;
                        //    daily.Net = 0 - daily.Debit + Net;
                        //}
                        //else
                        //{
                        //    daily.Net = 0 - daily.Debit;
                        //}

                    }
                    if (HeadName == "Sales")
                    {
                        daily.Debit = Total;
                        daily.Credit = 0;
                        //if (Net != null)
                        //{
                        //    daily.Debit = Total;
                        //    daily.Net = 0 - daily.Debit + Net;
                        //}
                        //else
                        //{
                        //    daily.Net = 0 - daily.Debit;
                        //}
                    }
                    if (HeadName == "Expense")
                    {
                        daily.Debit = Total;
                        daily.Credit = 0;
                        //if (Net != null)
                        //{
                        //    daily.Debit = Total;
                        //    daily.Net = 0 - daily.Debit + Net;
                        //}
                        //else
                        //{
                        //    daily.Net = 0 - daily.Debit;
                        //}
                    }
                    if (HeadName == "Revenue")
                    {
                        daily.Credit = Total;
                        daily.Debit = 0;
                        //if (Net != null)
                        //{
                        //    daily.Net = Net + daily.Credit;
                        //}
                        //else
                        //{
                        //    daily.Net = daily.Credit - 0;
                        //}
                    }

                    var dailyItems = _dbContext.DailyItem.Where(i => i.DailyId == dailyid).ToList();
                    foreach (var item in dailyItems)
                    {
                        _dbContext.DailyItem.Remove(item);
                        _dbContext.SaveChanges();
                    }

                    SaveDailyItems(dailyid, data);
                    _dbContext.Entry(daily).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    var msg = new ModelStateException(ex);
                    return Json(new { success = false, message = msg.Message });
                    //ModelState.AddModelError("", msg);
                    //TempData["MessageToClientError"] = msg;
                }
            }
            return View();
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Daily daily = _dbContext.Daily.Find(id);
            if (daily == null)
            {
                return HttpNotFound();
            }
            return View(daily);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Daily daily = _dbContext.Daily.Find(id);
            _dbContext.Daily.Remove(daily);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
