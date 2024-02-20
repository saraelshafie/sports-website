using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Fantasy
{
    public partial class SMpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            updateRequestsDropdown();
        }

        protected void generateView(String query)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            SqlDataAdapter dataAdpt = new SqlDataAdapter(query, conn);
            DataTable dataTable = new DataTable();
            dataAdpt.Fill(dataTable);

            gridView.DataSource = dataTable;
            gridView.DataBind();

            conn.Close();
        }

        protected void onStadiumInfo(object sender, EventArgs e)
        {
            String username = (String) Session["username"];
            tableView.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "visible";

            generateView($"SELECT S.name, S.capacity, S.location, S.status FROM Stadium_Manager SM INNER JOIN Stadium S ON SM.stadium_id = S.id WHERE SM.username = '{username}'");
        }

        protected void onRequests(object sender, EventArgs e)
        {
            String username = (String)Session["username"];
            tableView.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "visible";


            generateView($"SELECT * FROM dbo.getRequests('{username}')");
        }


        protected void onAcceptRequest(object sender, EventArgs e)
        {
            gridView.DataBind();

            String username = (String) Session["username"];

            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            String selected = requestsDropdown.SelectedValue;

            if (!selected.Equals(""))
            {
                String[] selectedOption = requestsDropdown.SelectedValue.Split(new String[] { " VS ", " ON " }, StringSplitOptions.None);

                String hostClub = selectedOption[0];
                String guestClub = selectedOption[1];
                String startTime = selectedOption[2];

                SqlCommand acceptRequestProc = new SqlCommand("acceptRequest", conn);
                acceptRequestProc.CommandType = CommandType.StoredProcedure;

                acceptRequestProc.Parameters.Add(new SqlParameter("@SM_username", username));
                acceptRequestProc.Parameters.Add(new SqlParameter("@hostClub", hostClub));
                acceptRequestProc.Parameters.Add(new SqlParameter("@guestClub", guestClub));
                acceptRequestProc.Parameters.Add(new SqlParameter("@starttime", startTime));

                acceptRequestProc.ExecuteNonQuery();

                updateRequestsDropdown();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Accepted')", true);
            }

            conn.Close();




        }

        protected void onRejectRequest(object sender, EventArgs e)
        {
            gridView.DataBind();

            String username = (String)Session["username"];

            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();


            String selected = requestsDropdown.SelectedValue;

            if (!selected.Equals(""))
            {
                String[] selectedOption = selected.Split(new String[] { " VS ", " ON " }, StringSplitOptions.None);

                String hostClub = selectedOption[0];
                String guestClub = selectedOption[1];
                String startTime = selectedOption[2];


                SqlCommand rejectRequestProc = new SqlCommand("rejectRequest", conn);
                rejectRequestProc.CommandType = CommandType.StoredProcedure;

                rejectRequestProc.Parameters.Add(new SqlParameter("@SM_username", username));
                rejectRequestProc.Parameters.Add(new SqlParameter("@hostClub", hostClub));
                rejectRequestProc.Parameters.Add(new SqlParameter("@guestClub", guestClub));
                rejectRequestProc.Parameters.Add(new SqlParameter("@starttime", startTime));

                rejectRequestProc.ExecuteNonQuery();

                updateRequestsDropdown();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Rejected')", true);
            }

            conn.Close();

        }

        protected void updateRequestsDropdown()
        {
            String username = (String)Session["username"];
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            // update the match drop down list manually
            SqlDataAdapter allRequestsAdpt = new SqlDataAdapter($"SELECT * FROM dbo.getRequests('{username}') WHERE status = 'Unhandled' ", conn);
            DataTable allRequestsTable = new DataTable();
            allRequestsAdpt.Fill(allRequestsTable);

            List<String> allRequestsList = new List<String>();

            foreach (DataRow row in allRequestsTable.Rows)
            {
                String s = (String) row[1] + " VS " + row[2] + " ON " + row[3];
                allRequestsList.Add(s);
            }

            requestsDropdown.DataSource = allRequestsList;
            requestsDropdown.DataBind();

            conn.Close();
        }

    }
}