using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fantasy
{
    public partial class Sports_Association_Manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void onRegisterSAM(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();


            String Name = name.Text;
            String user = username.Text;
            String pass = password.Text;



            if (Name.Equals("") || user.Equals("") || pass.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }

            SqlCommand usernameNotTaken = new SqlCommand("SELECT dbo.usernameNotTaken(@username)", conn);
            usernameNotTaken.Parameters.Add(new SqlParameter("@username", username.Text));

            Boolean result = (Boolean) usernameNotTaken.ExecuteScalar();

            if (result)
            {
                SqlCommand addAMProc = new SqlCommand("addAssociationManager", conn);
                addAMProc.CommandType = CommandType.StoredProcedure;

                addAMProc.Parameters.Add(new SqlParameter("@name", name.Text));
                addAMProc.Parameters.Add(new SqlParameter("@username", username.Text));
                addAMProc.Parameters.Add(new SqlParameter("@password", password.Text));

                addAMProc.ExecuteNonQuery();

                Response.Redirect("Login.aspx");

            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Registration Failed! Username taken')", true);
                //not tested

            conn.Close();
        }
    }
}