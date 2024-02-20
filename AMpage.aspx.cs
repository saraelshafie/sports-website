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
    public partial class AMpage : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{

        //    System.Diagnostics.Debug.WriteLine("hi");


        //    if (!IsPostBack) {


        //        String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
        //        SqlConnection conn = new SqlConnection(connStr);

        //        conn.Open();

        //        hostDropdownDelete.Items.Clear();
        //        guestDropdownDelete.Items.Clear();

        //        SqlCommand allClubs = new SqlCommand("SELECT * FROM allClubs", conn);

        //        SqlDataReader rdr = allClubs.ExecuteReader(CommandBehavior.CloseConnection);

        //        int i = 0;

        //        hostDropdownAdd.Items.Clear();
        //        guestDropdownAdd.Items.Clear();

        //        while (rdr.Read())
        //        {
        //            String clubName = rdr.GetString(rdr.GetOrdinal("name"));
        //            hostDropdownAdd.Items.Insert(i, new ListItem(clubName));
        //            guestDropdownAdd.Items.Insert(i, new ListItem(clubName));

        //            i++;
        //        };

        //        rdr.Close();

        //        conn.Open();

        //        SqlCommand allMatches = new SqlCommand("SELECT * FROM allMatches", conn);

        //        SqlDataReader matchRdr = allMatches.ExecuteReader(CommandBehavior.CloseConnection);

        //        int j = 0;



        //        while (matchRdr.Read())
        //        {
        //            String hostClub = matchRdr.GetString(matchRdr.GetOrdinal("HostClub"));
        //            String guestClub = matchRdr.GetString(matchRdr.GetOrdinal("GuestClub"));
        //            hostDropdownDelete.Items.Insert(j, new ListItem(hostClub));
        //            guestDropdownDelete.Items.Insert(j, new ListItem(guestClub));

        //            j++;
        //        }


        //        conn.Close();

        //    }
        //    else
        //    {

        //        String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
        //        SqlConnection conn = new SqlConnection(connStr);

        //        SqlCommand allMatches = new SqlCommand("SELECT * FROM allMatches", conn);
        //        conn.Open();

        //        SqlDataReader matchRdr = allMatches.ExecuteReader(CommandBehavior.CloseConnection);

        //        int j = 0;

        //        hostDropdownDelete.Items.Clear();
        //        guestDropdownDelete.Items.Clear();



        //        while (matchRdr.Read())
        //        {
        //            String hostClub = matchRdr.GetString(matchRdr.GetOrdinal("HostClub"));
        //            String guestClub = matchRdr.GetString(matchRdr.GetOrdinal("GuestClub"));
        //            hostDropdownDelete.Items.Insert(j, new ListItem(hostClub));
        //            guestDropdownDelete.Items.Insert(j, new ListItem(guestClub));

        //            j++;
        //        }


        //        conn.Close();

        //    }

        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;


            updateClubDropdown();
            //updateDeleteDropdown();
        }

        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    updateDeleteDropdown();
        //}

        protected void updateClubDropdown()
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();


            SqlDataAdapter allClubsAdpt = new SqlDataAdapter("SELECT * FROM allClubs", conn);
            DataTable allClubs = new DataTable();
            allClubsAdpt.Fill(allClubs);


            hostDropdownAdd.DataTextField = "name";
            hostDropdownAdd.DataValueField = "name";
            hostDropdownAdd.DataSource = allClubs;
            hostDropdownAdd.DataBind();

            guestDropdownAdd.DataTextField = "name";
            guestDropdownAdd.DataValueField = "name";
            guestDropdownAdd.DataSource = allClubs;
            guestDropdownAdd.DataBind();

            conn.Close();

        }

        //protected void updateDeleteDropdown()
        //{
        //    String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
        //    SqlConnection conn = new SqlConnection(connStr);

        //    conn.Open();

        //    // update the match drop down list manually
        //    SqlDataAdapter allMatchesAdpt = new SqlDataAdapter("SELECT * FROM allMatches", conn);
        //    DataTable allMatches = new DataTable();
        //    allMatchesAdpt.Fill(allMatches);

        //    List<String> allMatchesList = new List<String>();

        //    foreach (DataRow row in allMatches.Rows)
        //    {
        //        String s = (String)row[0] + " VS " + row[1] + ", Starting time : " + row[2];
        //        allMatchesList.Add(s);
        //    }

        //    hostDropdownDelete.DataSource = allMatchesList;
        //    hostDropdownDelete.DataBind();

        //    conn.Close();
        //}


        // if hostClub.equal(guestClub) then error message

        protected void onAddMatch(object sender, EventArgs e)
        {

            gridView.DataBind();

            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand addMatchProc = new SqlCommand("addNewMatch", conn);
            addMatchProc.CommandType = CommandType.StoredProcedure;

            String hostClub = hostDropdownAdd.SelectedValue;
            String guestClub = guestDropdownAdd.SelectedValue;
            String startTime = startTimeTextAdd.Text;
            String endTime = endTimeTextAdd.Text;


            if (hostClub.Equals("") || guestClub.Equals("") || startTime.Equals("") || endTime.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
            }
            else if (hostClub.Equals(guestClub))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cannot add a match with the same host and guest clubs!')", true);

            }

            else
            {
                addMatchProc.Parameters.Add(new SqlParameter("@hostClub", hostClub));
                addMatchProc.Parameters.Add(new SqlParameter("@guestClub", guestClub));
                addMatchProc.Parameters.Add(new SqlParameter("@start_time", DateTime.Parse(startTime)));
                addMatchProc.Parameters.Add(new SqlParameter("@end_time", DateTime.Parse(endTime)));

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully Added!')", true);


                conn.Open();
                addMatchProc.ExecuteNonQuery();
                conn.Close();
            }

        }

        protected void onDeleteMatch(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            gridView.DataBind();


            String hostClub = hostClubTextBox.Text;
            String guestClub = guestClubTextBox.Text;
            String startTime = startTimeText.Text;
            String endTime = endTimeText.Text;

            //DateTime startTime = DateTime.Parse(startTimeText.Text);
            //DateTime endTime = DateTime.Parse(endTimeText.Text);

            if (hostClub.Equals("") || guestClub.Equals("") || startTime.Equals("") || endTime.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }

                SqlCommand checkMatchExists = new SqlCommand("SELECT dbo.checkMatchExists(@hostClub, @guestClub, @start_time, @end_time)", conn);

                SqlCommand deleteMatchProc = new SqlCommand("deleteMatch", conn);
                deleteMatchProc.CommandType = CommandType.StoredProcedure;


                checkMatchExists.Parameters.Add(new SqlParameter("@hostClub", hostClub));
                checkMatchExists.Parameters.Add(new SqlParameter("@guestClub", guestClub));
                checkMatchExists.Parameters.Add(new SqlParameter("@start_time", DateTime.Parse(startTime)));
                checkMatchExists.Parameters.Add(new SqlParameter("@end_time", DateTime.Parse(endTime)));


                deleteMatchProc.Parameters.Add(new SqlParameter("@hostClub", hostClub));
                deleteMatchProc.Parameters.Add(new SqlParameter("@guestClub", guestClub));
                deleteMatchProc.Parameters.Add(new SqlParameter("@start_time", DateTime.Parse(startTime)));
                deleteMatchProc.Parameters.Add(new SqlParameter("@end_time", DateTime.Parse(endTime)));


                Boolean exists = (Boolean)checkMatchExists.ExecuteScalar();

                if (exists)
                {
                    deleteMatchProc.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully Deleted!')", true);

                }

                else
                {
                    // WARNING : NO SUCH MATCH EXISTS  
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Match does not exist!')", true);

                }
            

            conn.Close();
        }

        protected void onUpcomingMatches(object sender, EventArgs e)
        {
            tableView.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "visible";

            generateView("SELECT * FROM allMatches WHERE StartTime > CURRENT_TIMESTAMP");

        }

        protected void onPlayedMatches(object sender, EventArgs e)
        {
            tableView.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "visible";

            generateView("SELECT * FROM allMatches WHERE StartTime < CURRENT_TIMESTAMP");

        }
        protected void onClubsNeverMatched(object sender, EventArgs e)
        {
            tableView.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "visible";

            generateView("SELECT * FROM clubsNeverMatched");
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


    }
}