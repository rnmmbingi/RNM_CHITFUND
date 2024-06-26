//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RNM_CHITFUND.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Chitti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Chitti()
        {
            this.tbl_Enroll_Cust = new HashSet<tbl_Enroll_Cust>();
            this.tbl_LiftedCustomer = new HashSet<tbl_LiftedCustomer>();
            this.tbl_MonthlyPayment = new HashSet<tbl_MonthlyPayment>();
        }
    
        public int ID { get; set; }
        public string Chitt_ID { get; set; }
        public decimal Chitt_Value { get; set; }
        public Nullable<byte> Months { get; set; }
        public Nullable<decimal> Installmt_Amt { get; set; }
        public string Chitt_Type { get; set; }
        public Nullable<int> Members { get; set; }
        public Nullable<decimal> Payable_Amt { get; set; }
        public Nullable<decimal> Auction_Amt { get; set; }
        public Nullable<decimal> Commission_Amt { get; set; }
        public Nullable<System.DateTime> DateOfAuction { get; set; }
        public Nullable<System.DateTime> DateOfStart { get; set; }
        public Nullable<System.DateTime> DateOfEnd { get; set; }
        public byte Members_Count_For_Chitti { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Enroll_Cust> tbl_Enroll_Cust { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_LiftedCustomer> tbl_LiftedCustomer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_MonthlyPayment> tbl_MonthlyPayment { get; set; }
    }
}
