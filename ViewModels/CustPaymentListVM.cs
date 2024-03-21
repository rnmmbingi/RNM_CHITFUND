using RNM_CHITFUND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RNM_CHITFUND.ViewModels
{
    public class CustPaymentListVM
    {

        public tbl_Cust Customer { get; set; }
        public tbl_MonthlyPayment Payment { get; set; }
        
    }
}