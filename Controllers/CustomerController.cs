using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using RNM_CHITFUND.Models;


namespace RNM_CHITFUND.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {

        RNM_CHITFUNDEntities _objDB;
        // GET: Admin
        public ActionResult Index()
        {
            using (_objDB = new RNM_CHITFUNDEntities())
			{			
                IEnumerable<tbl_Cust> cust = _objDB.tbl_Cust.ToList();
                return View(cust);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_Cust customer)
        {
           if(ModelState.IsValid)
			{
                using ( _objDB = new RNM_CHITFUNDEntities())
                {
                    RNM_DBContext.InsertCustomer(customer);
                    _objDB.SaveChanges();
                }
     
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult GetDetails(string id)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                tbl_Cust cust= _objDB.tbl_Cust.FirstOrDefault(c=>c.Cust_ID==id);
                return View(cust);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public FileResult ExportToPdf(string ExportData)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader reader = new StringReader(ExportData);
                Document PdfFile = new Document(PageSize.A4, 50f, 30f, 20f, 10f); 
                PdfWriter writer = PdfWriter.GetInstance(PdfFile, stream);                
                PdfFile.Open();

                //adding Image
                Image img = Image.GetInstance(Server.MapPath("~/Content/Images/logo4.png"));
                img.ScaleToFit(60,60);
                //0=Left, 1=Centre, 2=Right
                img.Alignment = 1;
                PdfFile.Add(img);
                //adding Header
                Paragraph header = new Paragraph("Customet List", new Font( Font.FontFamily.HELVETICA, 18, Font.BOLD,BaseColor.BLUE));
                header.Alignment = 1;
                header.SpacingAfter = 10f;
                PdfFile.Add(header);
                
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, PdfFile, reader);
                PdfFile.Close();
                string str = DateTime.Now.ToString();
                return File(stream.ToArray(), "application/pdf", "Customers_List_"+str+".pdf");
            }
        }
        public ActionResult Edit(string id)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                tbl_Cust cust = _objDB.tbl_Cust.FirstOrDefault(c => c.Cust_ID == id);
                return View(cust);
            }
        }
        [HttpPost]        
        public ActionResult Edit(tbl_Cust obj)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                tbl_Cust cust = _objDB.tbl_Cust.FirstOrDefault(c => c.Cust_ID == obj.Cust_ID);
                cust.Cust_Name = obj.Cust_Name;
                cust.Cust_Surname = obj.Cust_Surname;
                cust.Cust_Father = obj.Cust_Father;
                cust.Gender = obj.Gender;
                cust.Addres = obj.Addres;
                cust.Mobile_1 = obj.Mobile_1;
                cust.Mobile_2 = obj.Mobile_2;
                cust.Refer_By = obj.Refer_By;
                cust.Refer_Mobile = obj.Refer_Mobile;                
                _objDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                tbl_Cust cust = _objDB.tbl_Cust.FirstOrDefault(c => c.Cust_ID == id);
                return View(cust);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            using (_objDB = new RNM_CHITFUNDEntities())
            {
                tbl_Cust cust = _objDB.tbl_Cust.FirstOrDefault(c => c.Cust_ID == id);
                _objDB.tbl_Cust.Remove(cust);
                _objDB.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}