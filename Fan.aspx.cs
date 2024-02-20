using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Policy;
using System.Xml.Linq;

namespace Fantasy
{
    public partial class Fan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void onRegisterFan(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            String Name = name.Text;
            String user = username.Text;
            String pass = password.Text;
            String NID = nid.Text;
            String date = dob.Text;
            String Address = address.Text;
            String number = phone.Text;



            if (Name.Equals("") || user.Equals("") || pass.Equals("") || NID.Equals("") || date.Equals("") || Address.Equals("") || number.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }

            SqlCommand usernameNotTaken = new SqlCommand("SELECT dbo.usernameNotTaken(@username)", conn);
            usernameNotTaken.Parameters.Add(new SqlParameter("@username", username.Text));

            Boolean result = (Boolean)usernameNotTaken.ExecuteScalar();


            SqlCommand nidNotTaken = new SqlCommand("SELECT dbo.nidNotTaken(@nationalid)", conn);
            nidNotTaken.Parameters.Add(new SqlParameter("@nationalid", nid.Text));

            Boolean nid_notTaken = (Boolean)nidNotTaken.ExecuteScalar();


            if (result && nid_notTaken)
            {
                SqlCommand addFanProc = new SqlCommand("addFan", conn);
                addFanProc.CommandType = CommandType.StoredProcedure;

                addFanProc.Parameters.Add(new SqlParameter("@username", username.Text));
                addFanProc.Parameters.Add(new SqlParameter("@password", password.Text));
                addFanProc.Parameters.Add(new SqlParameter("@nationalid", nid.Text));
                addFanProc.Parameters.Add(new SqlParameter("@birthdate", DateTime.Parse(dob.Text)));
                addFanProc.Parameters.Add(new SqlParameter("@address", address.Text));
                addFanProc.Parameters.Add(new SqlParameter("@phone", phone.Text));
                addFanProc.Parameters.Add(new SqlParameter("@name", name.Text));

                addFanProc.ExecuteNonQuery();

                Response.Redirect("Login.aspx");
            }
            else
                if (!result)
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Registration Failed! Username taken')", true);

                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Registration Failed! National Id taken')", true);


            conn.Close();
        }


    }
}