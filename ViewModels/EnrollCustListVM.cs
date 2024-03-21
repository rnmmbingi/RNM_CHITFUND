using RNM_CHITFUND.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RNM_CHITFUND.ViewModels
{
    public class EnrollCustListVM
    {
        [Required(ErrorMessage = "Chitti ID is required")]
        public string Chitt_ID { get; set; }
        public List<SelectListItem> ChittiSchemes { get; set; }        
        public List<SelectListItem> CustomersList { get; set; }
        public char? IsLifted { get; set; }
        [Required(ErrorMessage = "Cust ID is required")]
        public string Cust_ID { get; set; }
        
        public tbl_Cust Customers { get; set; }
        
        public tbl_Enroll_Cust EnrollCustList { get; set; }
        [Required]
        public int Due_Amt { get; set; }
        [Required(ErrorMessage = "Montyhly Amount is required")]
        public string MontyhlyAmount { get; set; }
        [Required(ErrorMessage = "Mode Of Payment is required")]
        public string ModeOfPayment { get; set; }
        [Required(ErrorMessage = "Month Of Payment is required")]
        public string MonthOfPayment { get; set; }
        [Required(ErrorMessage = "Year Of Payment is required")]
        public string YearOfPayment { get; set; }
    }

}