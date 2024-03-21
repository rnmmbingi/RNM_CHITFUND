using RNM_CHITFUND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RNM_CHITFUND.Controllers
{
    [Authorize]
    public class ChittiSchemeController : Controller
    {
        RNM_CHITFUNDEntities _objDB;
        // GET: Admin
        public ActionResult Index()
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                IEnumerable<tbl_Chitti> schemes = _objDB.tbl_Chitti.ToList();
                return View(schemes);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_Chitti chitti)
        {
            if (ModelState.IsValid)
            {
                using (_objDB = new RNM_CHITFUNDEntities())
                {
                    RNM_DBContext.InsertChittiScheme(chitti);
                    _objDB.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(chitti);
        }
        [HttpGet]
        public ActionResult GetDetails(string id)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                tbl_Chitti chitti = _objDB.tbl_Chitti.FirstOrDefault(c => c.Chitt_ID == id);
                return View(chitti);
            }
        }
        public ActionResult Edit(string id)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                tbl_Chitti chitti = _objDB.tbl_Chitti.FirstOrDefault(c => c.Chitt_ID == id);
                return View(chitti);
            }
        }
        [HttpPost]
        public ActionResult Edit(tbl_Chitti obj)
        {

                using (_objDB = new RNM_CHITFUNDEntities())
                {
                    RNM_DBContext.UpdateChittiScheme(obj);
                    _objDB.SaveChanges();
                    return RedirectToAction("Index");
                }
  
            return View(obj);
        }


        //[HttpGet]
        //public ActionResult Delete(string id)
        //{
        //    using (_objDB = new RNM_CHITFUNDEntities())
        //    {
        //        tbl_Cust cust = _objDB.tbl_Cust.FirstOrDefault(c => c.Cust_ID == id);
        //        return View(cust);
        //    }
        //}
        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult ConfirmDelete(string id)
        //{
        //    using (_objDB = new RNM_CHITFUNDEntities())
        //    {
        //        tbl_Cust cust = _objDB.tbl_Cust.FirstOrDefault(c => c.Cust_ID == id);
        //        _objDB.tbl_Cust.Remove(cust);
        //        _objDB.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //}

    }
}