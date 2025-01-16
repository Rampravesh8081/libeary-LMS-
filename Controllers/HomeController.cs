using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Library_Management_System_.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=RAMPRAVESH\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;");
      
        public ActionResult index()
        {
            string query = "select * from tbl_branch order by sr desc";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ViewBag.table1 = dt;
            
            return View();
        }

        [HttpPost]
        public ActionResult addContact(string fname, string lname, string email, long? num, string subject, string course, string message)
        {
            string query = $"insert into tbl_contactUs values('{fname}','{lname}','{email}',{num},'{subject}','{course}','{message}')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result > 0)
            {
                return Content("<script>alert('contact details Added');location.href='/Home/Index1'</script>");
            }
            else
            {
                return Content("<script>alert('Contact Not Added');location.href='/Home/adddetails'</script>");
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SignUp()
        {

            string query = "select * from tbl_branch order by sr desc";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ViewBag.table1 = dt;


            return View();
            
        }
        [HttpPost]
        public ActionResult regdata(string fname,long? num, string state, long? pin , string address, string rdate,string dob,string email, string city,long? branch, string Id, long? passwd)
        {
            string query = $"insert into tbl_register2 values('{fname}','{dob}',{num},'{email}','{state}','{city}',{pin},{branch},'{Id}','{rdate}','{address}',{passwd},'0')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result > 0)
            {
            
                return Content("<script>alert('Deatils added');location.href='/home/index'</script>");
            }
            else
            {
                return Content("<script>alert('Branch not added');location.href='/home/signup'</script>");
            }
        }
        [HttpPost]
        public ActionResult adminsign(string Id,string passwd)
        {
            if (Id.Equals("Rajendra") && passwd.Equals("246"))
            {
                Session["admin"] = Id;

                return Content("<script>alert('welcome');location.href='/admin/dashbord'</script>");
            }
            else
            {
              return   Content("<script>alert('Invalid Id or Password');location.href='/home/adminsign'</script>");
            }
        }
        public ActionResult issuebook()
        {
            return View();
        }
        [HttpPost]
        public ActionResult signin(string email, long? password)
        {

            string query = $"select * from tbl_register2 where email='{email}' and password='{password}'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["status"]) == 1)
                {
                    Session["name"] = dt.Rows[0]["name"];
                    Session["email"] = dt.Rows[0]["email"];
                    Session["dob"] = dt.Rows[0]["dob"];
                    Session["pin"] = dt.Rows[0]["pin"];
                    Session["state"] = dt.Rows[0]["state"];
                    Session["city"] = dt.Rows[0]["city"];
                    Session["Id"] = dt.Rows[0]["Id"];
                    Session["Address"] = dt.Rows[0]["address"];
                  
                    return Content("<script>alert('Welcome');location.href='/Student/Dashboard'</script>");

                }
                else
                {
                    return Content("<script>alert('You are not authorize by admin');location.href='/home/signin'</script>");
                }

            }
            else
            {
                return Content("<script>alert('Invalid Id or Password');location.href='/Home/index'</script>");
            }
        }
        public ActionResult logout()
        {
            Session.Remove("admin");
            return Content("<script>alert('Logged out');location.href='/home/index      '</script>");
        }
        
    }
}