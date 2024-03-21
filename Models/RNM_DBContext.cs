using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RNM_CHITFUND.Models;
using RNM_CHITFUND.ViewModels;

namespace RNM_CHITFUND.Models
{
    public class RNM_DBContext:DbContext
    {
        //public virtual DbSet<DueCustListVM> DueCustListVM { get; set; }
        public static void InsertCustomer(tbl_Cust customer)
        {
            using (var context = new RNM_CHITFUNDEntities())
            {
                context.Database.ExecuteSqlCommand("EXEC Pro_InsertCust @Cust_name,@surname,@father,@gender,@addr,@mob1,@mob2,@refBy,@refMob",
                    new SqlParameter("@Cust_name", customer.Cust_Name),
                    new SqlParameter("@surname", customer.Cust_Surname),
                    new SqlParameter("@father", customer.Cust_Father),
                    new SqlParameter("@gender", customer.Gender),
                    new SqlParameter("@addr", customer.Addres),
                    new SqlParameter("@mob1", customer.Mobile_1),
                    new SqlParameter("@mob2", customer.Mobile_2),
                    new SqlParameter("@refBy", customer.Refer_By),
                    new SqlParameter("@refMob", customer.Refer_Mobile));
            }
        }
        public static void InsertChittiScheme(tbl_Chitti chitti)
        {
            using (var context = new RNM_CHITFUNDEntities())
            {
                context.Database.ExecuteSqlCommand("EXEC Pro_InsertChit @c_value,@months,@instl_amt,@c_type,@members,@payableAmt,@auctionAmt,@commAmt,@dateAuction,@datestart",
                    new SqlParameter("@c_value", chitti.Chitt_Value),
                    new SqlParameter("@months", chitti.Months),
                    new SqlParameter("@instl_amt", chitti.Installmt_Amt),
                    new SqlParameter("@c_type", chitti.Chitt_Type),
                    new SqlParameter("@members", chitti.Members),
                    new SqlParameter("@payableAmt", chitti.Payable_Amt),
                    new SqlParameter("@auctionAmt", chitti.Auction_Amt),
                    new SqlParameter("@commAmt", chitti.Commission_Amt),
                    new SqlParameter("@dateAuction", Convert.ToDateTime(chitti.DateOfAuction)),
                    new SqlParameter("@datestart", Convert.ToDateTime(chitti.DateOfStart)));
            }
        }
        public static void GetChittiSchemeById(string id)
        {
            using (var context = new RNM_CHITFUNDEntities())
            {
              context.Database.ExecuteSqlCommand("EXEC Pro_GetChittiSchemeByID @chit_ID",
                    new SqlParameter("@chit_ID", id));

            }
        }
        public static void UpdateChittiScheme(tbl_Chitti chitti)
        {
            using (var context = new RNM_CHITFUNDEntities())
            {
                context.Database.ExecuteSqlCommand("EXEC Pro_UpdateChit @chitt_ID, @c_value,@months,@instl_amt,@c_type,@members,@payableAmt,@auctionAmt,@commAmt,@dateAuction,@datestart",
                    new SqlParameter("@chitt_ID", chitti.Chitt_ID),
                    new SqlParameter("@c_value", chitti.Chitt_Value),
                    new SqlParameter("@months", chitti.Months),
                    new SqlParameter("@instl_amt", chitti.Installmt_Amt),
                    new SqlParameter("@c_type", chitti.Chitt_Type),
                    new SqlParameter("@members", chitti.Members),
                    new SqlParameter("@payableAmt", chitti.Payable_Amt),
                    new SqlParameter("@auctionAmt", chitti.Auction_Amt),
                    new SqlParameter("@commAmt", chitti.Commission_Amt),
                    new SqlParameter("@dateAuction", Convert.ToDateTime(chitti.DateOfAuction)),
                    new SqlParameter("@datestart", Convert.ToDateTime(chitti.DateOfStart)));
            }
        }
        public static void InsertToEnrollScheme(string custID,string chitID)
        {
            using (var context = new RNM_CHITFUNDEntities())
            {
                context.Database.ExecuteSqlCommand("EXEC Pro_InsertEnrollScheme @Cust_ID,@Chitt_ID",
                    new SqlParameter("@Cust_ID", custID),
                    new SqlParameter("@Chitt_ID", chitID));
            }
        }
        public static void InsertPayLiftedAmount(PayLiftedAmountVM obj)
        {
            using (var context = new RNM_CHITFUNDEntities())
            {
            
                context.Database.ExecuteSqlCommand("EXEC pro_InsertLiftedCustomer @enrolId,@custId,@chitiId,@liftedDate,@paidAmount,@surity,@witness1,@witness2,@modeofPay ",
                    new SqlParameter("@enrolId", obj.Enroll_ID),
                    new SqlParameter("@custId", obj.Cust_ID),
                    new SqlParameter("@chitiId", obj.Chitt_ID),
                    new SqlParameter("@liftedDate", obj.Lifted_Date),
                    new SqlParameter("@paidAmount", obj.Paid_Amt),
                    new SqlParameter("@surity", obj.Surity),
                    new SqlParameter("@witness1", obj.Witness1),
                    new SqlParameter("@witness2", obj.Witness2),
                    new SqlParameter("@modeofPay", obj.ModeOfPayment));

            }
        }
        public static void RegisterUser(tbl_Users obj)
        {
            using (var context = new RNM_CHITFUNDEntities())
            {

                context.Database.ExecuteSqlCommand("EXEC Pro_RegisterUser @name, @surname, @username, @password,@phone, @email",
                    new SqlParameter("@name", obj.Name),
                    new SqlParameter("@surname", obj.Surname),
                    new SqlParameter("@username", obj.UserName),
                    new SqlParameter("@password", obj.Password),
                    new SqlParameter("@phone", obj.Phone),
                    new SqlParameter("@email", obj.Email));
            }
        }        
        public static IEnumerable<DueCustListVM> DueMonthlyPay_CustList(string cid, string monthOfPay)
        {
            using (var context = new RNM_CHITFUNDEntities())
            {
                
               return context.Database.SqlQuery<DueCustListVM>("EXEC Pro_DuePayment_Cust_List_ByMonthYear @chitti_Id, @MonthOfPament",
                    new SqlParameter("@chitti_Id", cid),
                    new SqlParameter("@MonthOfPament", monthOfPay)).ToList();           
                
            }
        }

    }
}
