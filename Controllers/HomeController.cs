using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RNM_CHITFUND.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "About Chit scheme";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Ramesh Bingi";

			return View();
		}
	}
}