using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Library_Management_System_.Controllers
{
    public class StudentController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=RAMPRAVESH\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;");
        // GET: Student
       public ActionResult Dashboard()
        {
          //  string query = $"select * from tbl_register2 ";
            //SqlDataAdapter sda = new SqlDataAdapter(query, con);
           // DataTable dt = new DataTable();
           // sda.Fill(dt);
           // ViewBag.data = dt;
            string query1 = $"select count(sr)  from tbl_publication  ";
            SqlDataAdapter sda1 = new SqlDataAdapter(query1, con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            ViewBag.data1 = dt1;
            string query2 = $"select  sum(price) from tbl_bookStocks ";
            SqlDataAdapter sda2 = new SqlDataAdapter(query2, con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            ViewBag.data2 = dt2;
            string query3 = $"select count(sr) from tbl_bookStocks";
            SqlDataAdapter sda3 = new SqlDataAdapter(query3, con);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            ViewBag.data3 = dt3;
            string query4 = $"select COUNT(*) from tbl_register2";
            SqlDataAdapter sda4 = new SqlDataAdapter(query4, con);
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4);
            ViewBag.data4 = dt4;
            string query5 = $"select COUNT(sr) from tbl_issuebook";
            SqlDataAdapter sda5 = new SqlDataAdapter(query5, con);
            DataTable dt5 = new DataTable();
            sda5.Fill(dt5);
            ViewBag.data5 = dt5;

            return View();

        }
        public ActionResult book()
        {
            /*string query = $" select * from tbl_branch left join tbl_publication on tbl_branch.sr=tbl_publication.sr left join tbl_bookStocks1 on tbl_branch.sr=tbl_bookStocks.sr";*/
            string query = $"select * from tbl_bookStocks order by sr desc";
            SqlDataAdapter sda3 = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda3.Fill(dt);
            ViewBag.data = dt;

            return View();
        }
        public ActionResult issuebook()
        {
            string query = $" select *from tbl_issuebook order by sr desc";
            SqlDataAdapter sda3 = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda3.Fill(dt);
            ViewBag.data = dt;
            string query1 = $" select *from tbl_bookStocks order by sr desc";
            SqlDataAdapter sda4 = new SqlDataAdapter(query1, con);
            DataTable dt1 = new DataTable();
            sda4.Fill(dt1);
            ViewBag.data1 = dt1;
            return View();
        }
        public ActionResult issuedetails()
        {
            return View();
        }
        [HttpPost]

        public ActionResult issuedetailss(long? issueid, string bookname, string duedate,  string memberid, string issuedate, string returndate, string issueby)
        {
            string query = $"insert into tbl_issuebook values({issueid},'{bookname}','{duedate}', '{memberid}','{issuedate}','{returndate}','{issueby}','1')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result > 0)
            {

                return Content("<script>alert('Issue Books');location.href='/student/dashboard'</script>");
            }
            else
            {
                return Content("<script>alert(' Not Issue Books');location.href='/student/dashboard'</script>");
            }
        }
        public ActionResult returnbook()
        {
            string query = $" select *from tbl_issuebook order by sr desc";
            SqlDataAdapter sda3 = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda3.Fill(dt);
            ViewBag.data = dt;
            return View();
        }
        public ActionResult returnbookss(string bookname, string rdate, string bookcon, long? latefee, string issueby, string returnby, string remark)
        {
            string query = $"insert into returnbooks values('{rdate}','{bookcon}',{latefee},'{issueby}','{returnby}','{remark}','{bookname}')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result > 0)
            {

                return Content("<script>alert('Book Return Successfully');location.href='/student/Dashboard'</script>");
            }
            else
            {
                return Content("<script>alert(' Not Return Book');location.href='/student/Dashboard'</script>");
            }
          
        }
       
    }
}