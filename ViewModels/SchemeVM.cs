using RNM_CHITFUND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RNM_CHITFUND.ViewModels
{
    public class SchemeVM
    {
        public string Chitt_ID { get; set; }
        public List<SelectListItem> ChittiSchemes { get; set; }
        public string SelectedSchemeValue { get; set; }
    }
    public class CustomerList
    {
        public List<tbl_Cust> Customers { get; set; }
    }
}