using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RNM_CHITFUND.Models;

namespace RNM_CHITFUND.ViewModels
{
    public class PayLiftedAmountVM
    {
        
        public int Enroll_ID { get; set; }
        public string Cust_ID { get; set; }
        public string Customer { get; set; }
        public string Chitt_ID { get; set; }
        public Nullable<System.DateTime> Lifted_Date { get; set; }
        [Required]
        public Nullable<decimal> Paid_Amt { get; set; }
        [Required]
        public string Surity { get; set; }
        public string Witness1 { get; set; }
        public string Witness2 { get; set; }
        [Required]
        public string ModeOfPayment { get; set; }
    }
}