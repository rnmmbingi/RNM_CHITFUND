using RNM_CHITFUND.Models;
using RNM_CHITFUND.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RNM_CHITFUND.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        RNM_CHITFUNDEntities _objDB;
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {

                IEnumerable<tbl_Chitti> chitiSchemesList = _objDB.tbl_Chitti.ToList();
                var model = new ChittiIDVM
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
        [HttpGet]
        public ActionResult GetEnrollList(string cid)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                List<tbl_Cust> customers = _objDB.tbl_Cust.ToList();
                List<tbl_Enroll_Cust> enrolCustList = _objDB.tbl_Enroll_Cust.Where(e=>e.Chitt_ID== cid).ToList();
                var enrollList = from c in customers
                                 join ecl in enrolCustList on c.Cust_ID equals ecl.Cust_ID into table1
                                     from d in table1.ToList()
                                     select new EnrollCustListVM
                                     {
                                         Customers = c,
                                         EnrollCustList = d                    
                                     };
                return View(enrollList);
            }
        }
        //[HttpGet]
        //public ActionResult GetEnrollList(object obj)
        //{
        //    return View(obj);
        //}

        [HttpGet]
        public ActionResult EnrollChitti()
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                IEnumerable<tbl_Cust> cust = _objDB.tbl_Cust.ToList();
                IEnumerable<tbl_Chitti> chitiSchemes = _objDB.tbl_Chitti.ToList();
                var model = new EnrollChittiVM
                {
                    Customers = cust.Select(x => new SelectListItem
                    {
                        Value = x.Cust_ID,
                        Text = x.Cust_Name+ " " +x.Cust_Surname+" - "+x.Cust_ID
                    }).ToList(),
                    ChittiSchemes = chitiSchemes.Select(x => new SelectListItem
                    {
                        Value = x.Chitt_ID,
                        Text = x.Chitt_Value + " / " + x.Chitt_ID
                    }).ToList()
                };

                return View(model);
            }
        }
        [HttpPost]
        public ActionResult EnrollChitti(EnrollChittiVM objVM)
        {
            if (ModelState.IsValid)
            {
                using (_objDB = new RNM_CHITFUNDEntities())
                {
                    try
                    {                   
                        RNM_DBContext.InsertToEnrollScheme(objVM.Cust_ID,objVM.Chitt_ID);
                        _objDB.SaveChanges();
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}