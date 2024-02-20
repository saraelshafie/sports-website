using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace Fantasy
{
    public partial class Club_Representative : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                conn.Open();


                SqlDataAdapter clubsAdap = new SqlDataAdapter("SELECT * FROM unassignedClubs", conn);
                DataTable clubs = new DataTable();
                clubsAdap.Fill(clubs);


                dropdownclub.DataTextField = "name";
                dropdownclub.DataValueField = "name";
                dropdownclub.DataSource = clubs;
                dropdownclub.DataBind();

                conn.Close();

            }
        }

        protected void onRegisterCR(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            
            conn.Open();

            String Name = name.Text;
            String club = dropdownclub.SelectedValue;
            String user = username.Text;
            String pass = password.Text;
            
           

            if (Name.Equals("") || club.Equals("") || user.Equals("") || pass.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }

            SqlCommand usernameNotTaken = new SqlCommand("SELECT dbo.usernameNotTaken(@username)", conn);
            usernameNotTaken.Parameters.Add(new SqlParameter("@username", username));

            Boolean result = (Boolean)usernameNotTaken.ExecuteScalar();

            if (result)
            {
                SqlCommand addRepProc = new SqlCommand("addRepresentative", conn);
                addRepProc.CommandType = CommandType.StoredProcedure;

                addRepProc.Parameters.Add(new SqlParameter("@name", Name));
                addRepProc.Parameters.Add(new SqlParameter("@clubName", club));
                addRepProc.Parameters.Add(new SqlParameter("@username", user));
                addRepProc.Parameters.Add(new SqlParameter("@password", pass));

                addRepProc.ExecuteNonQuery();

                Response.Redirect("Login.aspx");
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Registration Failed! Username taken')", true);


            conn.Close();
        }
    }
}