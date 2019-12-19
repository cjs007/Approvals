using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Approvals.ViewModels;
using Approvals.Models;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using System.Web.UI.WebControls;
using System.Data.Entity;
using OfficeOpenXml;
using PagedList;

namespace Approvals.Controllers
{
    public class PaymentRequestsController : Controller
    {

        private ApplicationDbContext _context;

        public PaymentRequestsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: PaymentRequest
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.ClientSortParm = sortOrder == "Client" ? "client_desc" : "Client";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";
            ViewBag.AmountSortParm = sortOrder == "Amount" ? "amount_desc" : "Amount";

            ViewBag.CurrentFilter = searchString;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var paymentRequests = from r in _context.PaymentRequests.ToList()
                                  select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                paymentRequests = paymentRequests.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }
                switch (sortOrder)
            {
                case "name_desc":
                    paymentRequests = paymentRequests.OrderByDescending(s => s.Name);
                    break;
                case "client_desc":
                    paymentRequests = paymentRequests.OrderByDescending(s => s.ClientId);
                    break;
                case "Client":
                    paymentRequests = paymentRequests.OrderBy(s => s.ClientId);
                    break;
                case "Date":
                    paymentRequests = paymentRequests.OrderBy(s => s.SubmitDateTime);
                    break;
                case "date_desc":
                    paymentRequests = paymentRequests.OrderByDescending(s => s.SubmitDateTime);
                    break;
                case "Status":
                    paymentRequests = paymentRequests.OrderBy(s => s.Status);
                    break;
                case "status_desc":
                    paymentRequests = paymentRequests.OrderByDescending(s => s.Status);
                    break;
                case "Amount":
                    paymentRequests = paymentRequests.OrderBy(s => s.Amount);
                    break;
                case "amount_desc":
                    paymentRequests = paymentRequests.OrderByDescending(s => s.Amount);
                    break;
                default:
                    paymentRequests = paymentRequests.OrderBy(s => s.Name);
                    break;
            }

            var viewModel = new PaymentRequestIndexViewModel
            {
                PaymentRequests = paymentRequests.ToList(),
                User = currentUser
            };

            return View(viewModel);

            //int pageNumber = (page ?? 1);
            //return View(students.ToPagedList(pageNumber, pageSize))
            //IPagedList<PaymentRequestIndexViewModel> paymentRequestsModel = new StaticPagedList<PaymentRequestIndexViewModel>(viewModel, pagenumber + 1, 5, totalCount);
        }


        //must be admin is implemented here.
        public ActionResult New()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);

            if (currentUser.IsAdmin)
            {
                var clients = _context.Clients.ToList();
                var viewModel = new NewPaymentRequestViewModel
                {
                    Clients = clients,
                    User = currentUser
                };

                return View(viewModel);
            }
            return RedirectToAction("Access", "Home");
        }


        //Must be admin is implemented here
        [HttpPost]
        public ActionResult Submit(HttpPostedFileBase file, PaymentRequest paymentRequest)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);

            if (currentUser.IsAdmin)
            {
                if (file != null && file.ContentLength > 0)
                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/Files"),
                                                   Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        ViewBag.Message = "File uploaded successfully";

                        var client = _context.Clients.SingleOrDefault(x => x.Id == paymentRequest.ClientId);

                        paymentRequest.Submitter = currentUser.FirstName + " " + currentUser.LastName;
                        paymentRequest.FilePath = path;
                        paymentRequest.SubmitDateTime = DateTime.Now;
                        paymentRequest.ApproveDateTime = null;
                        paymentRequest.Approved = false;
                        paymentRequest.Paid = false;
                        paymentRequest.Status = "Submitted";
                        paymentRequest.Client = client;

                        _context.PaymentRequests.Add(paymentRequest);
                        _context.SaveChanges();

                        return RedirectToAction("Index", "PaymentRequests");

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }
                return View();
            }

            return View();
        }

        public ActionResult Download(int id)
        {
            DownloadFile(id);

            return RedirectToAction("Index", "PaymentRequests");
        }

        protected void DownloadFile(int id)
        {
            var paymentRequest = _context.PaymentRequests.SingleOrDefault(c => c.Id == id);

            var path = paymentRequest.FilePath;

            string filename = @path;
            FileInfo fileInfo = new FileInfo(filename);

            if (fileInfo.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name);
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.TransmitFile(fileInfo.FullName);
                Response.End();
            }
            else
            {
                TempData["msg"] = "<script>alert('Change succesfully');</script>";
            }
        }

        public ActionResult Review(int id)
        {
            var paymentRequest = _context.PaymentRequests.Include(m => m.Client).SingleOrDefault(c => c.Id == id);

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);

            var viewModel = new PaymentRequestReviewViewModel
            {
                PaymentRequest = paymentRequest,
                User = currentUser
            };

            return View(viewModel);
        }

        //is Approver implemented here.
        public ActionResult Approve(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);

            if (currentUser.IsApprover)
            {
                var paymentRequest = _context.PaymentRequests.Single(m => m.Id == id);

                paymentRequest.Approved = true;
                paymentRequest.ApproveDateTime = DateTime.Now;
                paymentRequest.Approver = currentUser.FirstName + " " + currentUser.LastName;
                paymentRequest.Status = "Approved";
                _context.SaveChanges();

                return RedirectToAction("Index", "PaymentRequests");
            }

            return RedirectToAction("Access", "Home");
        }

        //is owner imlemented here. Or User can be admin, creator, and payment request not marked paid to delete.
        public ActionResult Delete(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);

            var paymentRequest = _context.PaymentRequests.Single(m => m.Id == id);

            if (currentUser.IsOwner && paymentRequest.Status != "Paid")
            {
                System.IO.File.Delete(paymentRequest.FilePath);

                _context.PaymentRequests.Remove(paymentRequest);
                _context.SaveChanges();
                return RedirectToAction("Index", "PaymentRequests");
            }

            return RedirectToAction("Access", "Home");
        }

        //is admin implemented here
        [HttpPost]
        public ActionResult Paid(PaymentRequest paymentRequest)
        {

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);

            if (currentUser.IsAdmin)
            {
                var paymentRequestInDB = _context.PaymentRequests.Single(c => c.Id == paymentRequest.Id);

                paymentRequestInDB.Paid = true;
                paymentRequestInDB.Status = "Paid";
                paymentRequestInDB.CheckNumber = paymentRequest.CheckNumber;
                paymentRequestInDB.PaidDate = paymentRequest.PaidDate;

                paymentRequestInDB.Payor = currentUser.FirstName + " " + currentUser.LastName;

                _context.SaveChanges();

                return RedirectToAction("Index", "PaymentRequests");
            }

            return RedirectToAction("Access", "Home");

        }

        //is admin implemented here
        public ActionResult Pay(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);

            if (currentUser.IsAdmin)
            {
                var paymentRequest = _context.PaymentRequests.SingleOrDefault(c => c.Id == id);

                var viewModel = new PaymentRequestEditForm(paymentRequest)
                {

                };

                return View(viewModel);
            }

            return RedirectToAction("Access", "Home");
        }

        public ActionResult ExportToExcel()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);

            if (currentUser.IsOwner)
            {
                var gv = new GridView();
                //gv.DataSource = _context.PaymentRequests.Include(m =>m.Client).ToList();

                gv.DataSource = (from e in _context.PaymentRequests
                                 select new
                                 {
                                     Client = e.Client.ClientNumber,
                                     Payee = e.Name,
                                     Address = e.AddressOne + " " + e.AddressTwo + " " + e.AddressThree,
                                     Amount = e.Amount,
                                     GL = e.GLLine,
                                     Status = e.Status,
                                     SubmitTime = e.SubmitDateTime,
                                     Submitter = e.Submitter,
                                     ApproveTime = e.ApproveDateTime,
                                     Approver = e.Approver,
                                 }).ToList();
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                var filename = DateTime.Today.ToString("ddMMyyyy") + ".xls";
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
                return View("Index");
            }
            return RedirectToAction("Access", "Home");
        }
    }
}