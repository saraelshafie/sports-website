using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.NetworkInformation;

namespace Fantasy
{
    public partial class Fanpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void onViewMatches(object sender, EventArgs e)

        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            tableView.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "visible";


            conn.Open();

            String date = Date.Text;


            if (date.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }


            SqlCommand availableMatchesToAttend2 = new SqlCommand("SELECT * FROM dbo.availableMatchesToAttend2(@date)", conn);

            availableMatchesToAttend2.Parameters.Add(new SqlParameter("@date", DateTime.Parse(date)));

            SqlDataAdapter da = new SqlDataAdapter(availableMatchesToAttend2);
            DataTable dt = new DataTable();
            da.Fill(dt);

            GridView.DataSource = dt;
            GridView.DataBind();


            conn.Close();


        }

        protected void onPurchaseTicket(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

           



            String s = (String)Session["username"];

            SqlCommand getNID = new SqlCommand("SELECT dbo.getNID(@username)", conn);
            getNID.Parameters.Add(new SqlParameter("@username", s));

            String NID = (String)getNID.ExecuteScalar();

            String host = Host.Text;
            String guest = Guest.Text;
            String startTime = StartTime.Text;


            if (host.Equals("") || guest.Equals("") || startTime.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }

            SqlCommand checkPurchaseTicket = new SqlCommand("SELECT dbo.checkPurchaseTicket (@host, @guest, @starttime)", conn);
            checkPurchaseTicket.Parameters.Add(new SqlParameter("@host", host));
            checkPurchaseTicket.Parameters.Add(new SqlParameter("@guest", guest));
            checkPurchaseTicket.Parameters.Add(new SqlParameter("@starttime", DateTime.Parse(startTime)));


            Boolean res = (Boolean)checkPurchaseTicket.ExecuteScalar();

            if (res)
            {
                SqlCommand purchaseTicket = new SqlCommand("purchaseTicket", conn);
                purchaseTicket.CommandType = CommandType.StoredProcedure;

                purchaseTicket.Parameters.Add(new SqlParameter("@nationalid", NID));
                purchaseTicket.Parameters.Add(new SqlParameter("@hostClub", Host.Text));
                purchaseTicket.Parameters.Add(new SqlParameter("@guestClub", Guest.Text));
                purchaseTicket.Parameters.Add(new SqlParameter("@starttime", DateTime.Parse(StartTime.Text)));
                purchaseTicket.ExecuteNonQuery();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Ticket Successfully Purchased')", true);


            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Match does not exist')", true);

            }



            conn.Close();


        }

    }
}