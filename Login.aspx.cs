using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Fantasy
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void onLogin(object sender, EventArgs e)
        {

            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String u = username.Value;
            String p = password.Value;

            if (u.Equals("") || p.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }

            SqlCommand loginFn = new SqlCommand("SELECT dbo.login(@username, @password)", conn);
            loginFn.Parameters.Add(new SqlParameter("@username", u));
            loginFn.Parameters.Add(new SqlParameter("@password", p));

            conn.Open();
            Boolean correctUsername = (Boolean)loginFn.ExecuteScalar();

            if (correctUsername)
            {
                SqlCommand getUser = new SqlCommand("SELECT dbo.getUser(@username)", conn);
                getUser.Parameters.Add(new SqlParameter("@username", u));


                Session["username"] = username.Value;

                String r = (String)getUser.ExecuteScalar();
                if (r == "Fanpage")
                {
                    SqlCommand checkFanBlocked = new SqlCommand("SELECT dbo.checkFanBlocked (@username)", conn);
                    checkFanBlocked.Parameters.Add(new SqlParameter("@username", u));
                    Boolean blocked = (Boolean)checkFanBlocked.ExecuteScalar();
                    if (blocked)
                      ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your account is temporarily blocked')", true);

                    else
                        Response.Redirect(r + ".aspx");
                }
                else
                    Response.Redirect(r + ".aspx");
            }
            else
              ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Login Failed! Incorrect username or password')", true);






            /* SqlCommand testFn = new SqlCommand("SELECT * FROM dbo.tester(10)", conn);

             SqlDataReader rdr = testFn.ExecuteReader(CommandBehavior.CloseConnection);
             while (rdr.Read())
             {
                 int i = (int) rdr[rdr.GetOrdinal("id")];
                 Response.Write(i);
             }*/


            //SqlDataAdapter da = new SqlDataAdapter(testFn);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //string str = dt.Rows[1][0].ToString();
            //Response.Write(str);

            conn.Close();

        }












    }
}