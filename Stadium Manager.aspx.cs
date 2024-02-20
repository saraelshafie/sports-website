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
    public partial class Stadium_Manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                conn.Open();

                SqlDataAdapter stadiumsAdap = new SqlDataAdapter("SELECT * FROM unassignedStadiums", conn);
                DataTable stadiums = new DataTable();
                stadiumsAdap.Fill(stadiums);


                dropdownstadium.DataTextField = "name";
                dropdownstadium.DataValueField = "name";
                dropdownstadium.DataSource = stadiums;
                dropdownstadium.DataBind();

                conn.Close();
            }

        }


        protected void onRegisterSM(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();


            String Name = name.Text;
            String stadium = dropdownstadium.SelectedValue;
            String user = username.Text;
            String pass = password.Text;



            if (Name.Equals("") || stadium.Equals("") || user.Equals("") || pass.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }

            SqlCommand usernameNotTaken = new SqlCommand("SELECT dbo.usernameNotTaken(@username)", conn);
            usernameNotTaken.Parameters.Add(new SqlParameter("@username", username.Text));

            Boolean result = (Boolean)usernameNotTaken.ExecuteScalar();

            if (result)
            {

                SqlCommand addSMProc = new SqlCommand("addStadiumManager", conn);
                addSMProc.CommandType = CommandType.StoredProcedure;

                addSMProc.Parameters.Add(new SqlParameter("@name", name.Text));
                addSMProc.Parameters.Add(new SqlParameter("@stadiumName", dropdownstadium.Text));
                addSMProc.Parameters.Add(new SqlParameter("@username", username.Text));
                addSMProc.Parameters.Add(new SqlParameter("@password", password.Text));

                addSMProc.ExecuteNonQuery();


                Response.Redirect("Login.aspx");
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Registration Failed! Username taken')", true);
                //not tested


            conn.Close();
        }

    }
}