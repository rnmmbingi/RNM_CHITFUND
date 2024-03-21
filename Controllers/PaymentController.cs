using RNM_CHITFUND.Models;
using RNM_CHITFUND.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace RNM_CHITFUND.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        RNM_CHITFUNDEntities _objDB;
        // GET: Payment
        [HttpGet]
        public ActionResult Index()
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                IEnumerable<tbl_Chitti> chitiSchemes = _objDB.tbl_Chitti.ToList();
                var model = new EnrollCustListVM
                {
                    ChittiSchemes = chitiSchemes.Select(x => new SelectListItem
                    {
                        Value = x.Chitt_ID,
                        Text = x.Chitt_Value + " / " + x.Chitt_ID
                    }).ToList(),
                    CustomersList = new List<SelectListItem>()
                };
                return View(model);
            }
        }
        [HttpPost]
        public JsonResult GetCustomers(string schemeId)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                //IEnumerable<tbl_Enroll_Cust> cust = _objDB.tbl_Enroll_Cust.Where(c => c.Chitt_ID == schemeId).ToList();

                List<tbl_Cust> customers = _objDB.tbl_Cust.ToList();
                List<tbl_Enroll_Cust> enrolCustList = _objDB.tbl_Enroll_Cust.Where(e => e.Chitt_ID == schemeId).ToList();
                var enrollList = from c in customers
                                 join ecl in enrolCustList on c.Cust_ID equals ecl.Cust_ID into table1
                                 from d in table1.ToList()
                                 select new EnrollCustListVM
                                 {
                                     Customers = c,
                                     EnrollCustList = d
                                 };
                var customersList = enrollList.Select(x => new SelectListItem
                {
                    Value = x.EnrollCustList.Enroll_ID.ToString(),
                    Text = x.Customers.Cust_ID + " - " + x.Customers.Cust_Name + " " + x.Customers.Cust_Surname + " - " + x.EnrollCustList.Enroll_ID
                }).ToList();
                return Json(customersList, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetDueAmount(int custId)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                decimal due = 0;
                tbl_MonthlyPayment res = _objDB.tbl_MonthlyPayment.Where(c => c.Enroll_ID == custId).OrderByDescending(c=>c.Recieved_Date).ToList()
                                        .FirstOrDefault(c => c.Enroll_ID == custId);                
                if(res!=null)
                {
                    due = res.Due_Amt;
                }    

                return Json(due, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult MonthlyPayment(EnrollCustListVM obj)
        {
            ViewBag.ErrorMsg = "";

                using (_objDB = new RNM_CHITFUNDEntities())
                {
                   tbl_Enroll_Cust cust= _objDB.tbl_Enroll_Cust.FirstOrDefault(c => c.Enroll_ID == obj.EnrollCustList.Enroll_ID);
                    tbl_MonthlyPayment payment = new tbl_MonthlyPayment()
                    {
                        Enroll_ID=obj.EnrollCustList.Enroll_ID,
                        Cust_ID= cust.Cust_ID,
                        Chitt_ID =obj.Chitt_ID,
                        Recieved_Amt=Convert.ToInt32(obj.MontyhlyAmount),
                        ModeOfPayment=obj.ModeOfPayment,
                        Recieved_Date=DateTime.Now,
                        MonthOfPament=obj.MonthOfPayment+"-"+obj.YearOfPayment,
                        Due_Amt = obj.Due_Amt                        
                    };
                    _objDB.tbl_MonthlyPayment.Add(payment);
                    try
                    {
                        _objDB.SaveChanges();


                    //MailMessage mail = new MailMessage();
                    //mail.To.Add("rameshrnmm.143@gmail.com");
                    //mail.From = new MailAddress("rameshbingi.practice@gmail.com");
                    //mail.Subject = "Test";
                    //string Body = " Hi Ramesh<br/> How are you?";
                    //mail.Body = Body;
                    //mail.IsBodyHtml = true;
                    //SmtpClient smtp = new SmtpClient();
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587;
                    //smtp.UseDefaultCredentials = false;
                    //smtp.Credentials = new System.Net.NetworkCredential("rameshbingi.practice@gmail.com", "rnbingi123"); // Enter seders User name and password       
                    //smtp.EnableSsl = false;
                    //smtp.Send(mail);

                    return RedirectToAction("CustPaymentList",new {id=payment.Enroll_ID,cid=payment.Cust_ID });
                    }
                    catch(Exception ex)
                    {
                        TempData["ErrorMsg"] = ex.InnerException ;
                    }
                }            
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public ActionResult PaymentList(EnrollCustListVM obj)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                IEnumerable<tbl_MonthlyPayment> paymentList = _objDB.tbl_MonthlyPayment.ToList();              
                return View(paymentList);
            }
        }
        [HttpGet]
        public ActionResult GetDueCustList()
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {

                IEnumerable<tbl_Chitti> chitiSchemesList = _objDB.tbl_Chitti.ToList();
                var model = new GetDueCustVM
                {
                    ChittiSchemes = chitiSchemesList.Select(x => new SelectListItem
                    {
                        Value = x.Chitt_ID,
                        Text = x.Chitt_Value + " / " + x.Chitt_ID
                    }).ToList()
                };

                return View(model);
            }

        }
        [HttpPost]
        public ActionResult GetDueCustList(GetDueCustVM obj)
        {
            //TempData["Month"] = obj.Month;
            //TempData["Year"] = obj.Year;
            //TempData["Chtid"] = obj.Year;
            return RedirectToAction("DuePaymentList_ByMonth",new { cid =obj.Chitt_ID,month=obj.Month,year=obj.Year});
        }
        [HttpGet]
        public ActionResult DuePaymentList_ByMonth(string cid, string month, string year)
        {            
            //string mop = TempData["Month"].ToString() + "-" + TempData["Year"].ToString();
            
            string mop = month + "-" +year;
            //string chid=TempData["Chtid"].ToString();
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                var obj=RNM_DBContext.DueMonthlyPay_CustList(cid, mop);
                ViewBag.Month = mop;
                ViewBag.chID = cid;
                return View(obj);
            }
              
        }
        [HttpGet]
        public ActionResult PayAuctionAmount(int id)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                tbl_Enroll_Cust custlift = _objDB.tbl_Enroll_Cust.FirstOrDefault(c => c.Enroll_ID == id);
                tbl_Cust cu = _objDB.tbl_Cust.FirstOrDefault(e => e.Cust_ID == custlift.Cust_ID);

                PayLiftedAmountVM liftedCustomer = new PayLiftedAmountVM()
                {
                    Enroll_ID = custlift.Enroll_ID,
                    Cust_ID = custlift.Cust_ID,
                    Chitt_ID = custlift.Chitt_ID,
                    Lifted_Date = DateTime.Now,
                    Customer = cu.Cust_Name + " " + cu.Cust_Surname
                };
                return View(liftedCustomer);
            }
        }


        
        [HttpPost]
        public ActionResult PayAuctionAmount(PayLiftedAmountVM obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (_objDB = new RNM_CHITFUNDEntities())
                    {
                        var cust = _objDB.tbl_Enroll_Cust.FirstOrDefault(c => c.Enroll_ID == obj.Enroll_ID);
                        if (cust.IsLifted == "N")
                        {
                            RNM_DBContext.InsertPayLiftedAmount(obj);
                            _objDB.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "This customer is already lifted chiti amount.");
                            return View(obj);
                        }
                    }                    
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error");
                }
            }
            return View(obj);
        }
        [HttpGet]
        public ActionResult LiftedCustList(string cid)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
               IEnumerable<tbl_LiftedCustomer> liftedCust= _objDB.tbl_LiftedCustomer.Where(c=>c.Chitt_ID==cid).ToList();
                return View(liftedCust);
            }
                
            
        }
        [HttpGet]
        public ActionResult CustPaymentList(int id, string cid)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                IEnumerable<tbl_MonthlyPayment> paymentList = _objDB.tbl_MonthlyPayment.Where(p => p.Enroll_ID == id).ToList();


                List<tbl_Cust> customers = _objDB.tbl_Cust.ToList();
                List<tbl_MonthlyPayment> paylist = _objDB.tbl_MonthlyPayment.ToList();


                var custPayRecord = from c in customers
                                    join pl in paylist on c.Cust_ID equals pl.Cust_ID into table1
                                    from pl in table1.Where(pl => pl.Enroll_ID == id).ToList().OrderBy(pl => pl.Recieved_Date)
                                    select new CustPaymentListVM
                                    {
                                        Customer = c,
                                        Payment = pl
                                    };

                return View(custPayRecord);



            }
        }


        [HttpGet]
        public ActionResult GetSchemesForAction()
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                IEnumerable<tbl_Chitti> chitiSchemes = _objDB.tbl_Chitti.ToList();
                var model = new ChittiIDVM
                {
                    ChittiSchemes = chitiSchemes.Select(x => new SelectListItem
                    {
                        Value = x.Chitt_ID,
                        Text = x.Chitt_Value + " / " + x.Chitt_ID
                    }).ToList()                  
                };
                return View(model);
            }
        }
    }
}