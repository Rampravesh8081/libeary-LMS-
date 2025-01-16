using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace LMS_Library_Management_System_.Controllers
{
    public class AdminController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=RAMPRAVESH\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;");
        // GET: Admin
       
        public ActionResult Dashbord()
        {
            
            string query = "select * from tbl_branch order by sr desc";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ViewBag.table1 = dt;


            return View();
        }
        [HttpPost]
        public ActionResult addbranch(string branch,string sem)
        {
            string query = $"insert into tbl_branch values('{branch}','1','{sem}')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result > 0)
            {
                return Content("<script>alert('Branch added');location.href='/admin/dashbord'</script>");
            }
            else
            {
                return Content("<script>alert('Branch not added');location.href='/admin/dashbord'</script>");
            }
          
        }
       
        public ActionResult publication()
        {
            string query = $"select *from tbl_publication order by sr desc";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ViewBag.table2 = dt;
            return View();
        }
        [HttpPost]
        public ActionResult addpublication(string Aname,HttpPostedFileBase Apic)
        {
            string query = $"insert into tbl_publication values('{Aname}','{Apic.FileName}','{DateTime.Now.ToString("yyyy-MM-dd")}')";
            SqlCommand cmd = new SqlCommand(query, con);    
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result > 0)
            {
                Apic.SaveAs(Server.MapPath("/Content/AuthorPic/" + Apic.FileName));
                return Content("<script>alert('Publication added');location.href='/admin/publication'</script>");
            }
            else
            {
                return Content("<script>alert('Publication Not added');location.href='/admin/publication'</script>");
            }
        }
        public ActionResult BookStocks()
        {
            string query = "select * from tbl_branch order by sr desc";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ViewBag.table2 = dt;
            string query1 = $"select *from tbl_publication order by sr desc";
            SqlDataAdapter sda1 = new SqlDataAdapter(query1, con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            ViewBag.table1 = dt1;
            string query2 = $"select * from tbl_bookStocks order by sr desc";
            SqlDataAdapter sda2 = new SqlDataAdapter(query2, con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            ViewBag.table3 = dt2;
            return View();
        }
        [HttpPost]
        public ActionResult bookstock(long? BID, long? Bprice, string branch , string Aname,string pdate, string Btitle, string bcat, long? Bq, string badddate,HttpPostedFileBase Pic,string lang, long? page, string Disc)
        {
            string query = $"insert into tbl_bookStocks values({BID},'{Aname}',{Bprice},'{pdate}','{Btitle}','{bcat}','{branch}',{Bq},'{badddate}','1','{Pic.FileName}','{lang}',{page},'{Disc}')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result > 0)
            {
                Pic.SaveAs(Server.MapPath("/Content/BookPhotos/" + Pic.FileName));
                return Content("<script>alert('Book Stocks added');location.href='/admin/BookStocks'</script>");
            }
            else
            {
                return Content("<script>alert('Book Stocks Not added');location.href='/admin/BookStocks'</script>");
            }
        }
        public ActionResult managestudent()
        {
            string query = $"select * from tbl_register2 order by branch desc";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ViewBag.data = dt;
            return View();
        }
        public ActionResult issuebook()
        {
            string query = $"select *from tbl_issuebook order by issueid desc";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ViewBag.data = dt;
            return View();
        }
        
   
        public ActionResult returnbook()
        {
            string query = $"select *from returnbooks order by sr desc";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ViewBag.data = dt;
            return View();
        }
        /*
        [HttpPost]
        public ActionResult returnbooks(string rdate,string bookcon,long? latefee , string issueby , string returnby, string  remark)
        {
            string query = $"insert into returnbooks values('{rdate}','{bookcon}',{latefee},'{issueby}','{returnby}','{remark}')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result > 0)
            {

                return Content("<script>alert('Return Books');location.href='/admin/returnbook'</script>");
            }
            else
            {
                return Content("<script>alert(' Not Return Books');location.href='/admin/returnbook'</script>");
            }
        }*/
        public ActionResult contact()
        {
            string query1 = "select * from tbl_contactUs order by sr desc";
            SqlDataAdapter sda1 = new SqlDataAdapter(query1, con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            ViewBag.table2 = dt1;


            return View();
        }
        
        public ActionResult updatestatus(string email, long? status)
        {
            if (!email.IsEmpty() && status.HasValue)
            {
                int s = status == 0 ? 1 : 0;
                string query = $"update tbl_register2 set status={s} where email='{email}'";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int r = cmd.ExecuteNonQuery();
                con.Close();
                return Content("<script>alert('Status updated');location.href='/admin/ManageStudent'</script>");
            }
            else
            {
                return Content("<script>alert('Try again');location.href='/admin/ManageStudent'</script>");
            }
        }
    }
}
