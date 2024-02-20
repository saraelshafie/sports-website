using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Fantasy
{
    public partial class CRpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String session = (String)Session["username"];
        }
        protected void onViewClub(object sender, EventArgs e)
        {
            tableView.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "visible";

            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String s = (String)Session["username"];

            conn.Open();

            SqlCommand getClub = new SqlCommand("SELECT dbo.getClub(@username)", conn);
            getClub.Parameters.Add(new SqlParameter("@username", s));

            String club = (String)getClub.ExecuteScalar();

            SqlCommand viewInfoOfClub = new SqlCommand("SELECT * FROM dbo.viewInfoOfClub(@clubName)", conn);
            viewInfoOfClub.Parameters.Add(new SqlParameter("@clubName", club));

            SqlDataAdapter da = new SqlDataAdapter(viewInfoOfClub);
            DataTable dt = new DataTable();
            da.Fill(dt);

            GridView.DataSource = dt;
            GridView.DataBind();


            conn.Close();

        }

        protected void onViewMatch(object sender, EventArgs e)
        {
            tableView.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "visible";

            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            String s = (String)Session["username"];

            SqlCommand getClub = new SqlCommand("SELECT dbo.getClub(@username)", conn);
            getClub.Parameters.Add(new SqlParameter("@username", s));

            String club = (String)getClub.ExecuteScalar();

            SqlCommand upcomingMatchesOfClub = new SqlCommand("SELECT * FROM dbo.upcomingMatchesOfClub(@clubName)", conn); //test it later
            upcomingMatchesOfClub.Parameters.Add(new SqlParameter("@clubName", club));

            SqlDataAdapter da = new SqlDataAdapter(upcomingMatchesOfClub);
            DataTable dt = new DataTable();
            da.Fill(dt);

            GridView.DataSource = dt;
            GridView.DataBind();



        }

        protected void onViewStadium(object sender, EventArgs e)
        {
            tableView.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "visible";

            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            String date = DateAvailableStadiums.Text;

            if (date.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }


            SqlCommand viewAvailableStadiumsOn = new SqlCommand("SELECT * FROM dbo.viewAvailableStadiumsOn(@time)", conn); //test it later
            viewAvailableStadiumsOn.Parameters.Add(new SqlParameter("@time", DateTime.Parse(date)));

            SqlDataAdapter da = new SqlDataAdapter(viewAvailableStadiumsOn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            GridView.DataSource = dt;
            GridView.DataBind();


            conn.Close();

        }


        protected void onSendRequest(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            String s = (String)Session["username"];

            SqlCommand getClub = new SqlCommand("SELECT dbo.getClub(@username)", conn);
            getClub.Parameters.Add(new SqlParameter("@username", s));

            String club = (String) getClub.ExecuteScalar();


            String stadium = Stadium.Text;
            String date = DateHostRequest.Text;

            if (stadium.Equals("") || date.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }

            SqlCommand checkStadiumExists = new SqlCommand("SELECT dbo.checkStadiumExists(@StadiumName)", conn);
            checkStadiumExists.Parameters.Add(new SqlParameter("@StadiumName", stadium));

            SqlCommand checkReqMatchExists = new SqlCommand("SELECT dbo.checkReqMatchExists(@hostClub, @startTime)", conn);
            checkReqMatchExists.Parameters.Add(new SqlParameter("@hostClub", club));
            checkReqMatchExists.Parameters.Add(new SqlParameter("@startTime", DateTime.Parse(date)));


            Boolean matchExists = (Boolean)checkReqMatchExists.ExecuteScalar();
            Boolean stadiumExists = (Boolean) checkStadiumExists.ExecuteScalar();


            if (!matchExists)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Match does not exist!')", true);
            }
            else if(!stadiumExists) 
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Stadium does not exist')", true);
            }
            else
            {
                SqlCommand addHostRequest = new SqlCommand("addHostRequest", conn);
                addHostRequest.CommandType = CommandType.StoredProcedure;

                addHostRequest.Parameters.Add(new SqlParameter("@clubName", club));
                addHostRequest.Parameters.Add(new SqlParameter("@stadiumName", Stadium.Text));
                addHostRequest.Parameters.Add(new SqlParameter("@starttime", DateTime.Parse(DateHostRequest.Text)));

                addHostRequest.ExecuteNonQuery();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Sent')", true);


            }


            conn.Close();

        }

    }
}