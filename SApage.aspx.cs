using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Fantasy
{
    public partial class SApage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void onAddClub(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String club = clubName.Text;
            String location = clubLoc.Text;

            if (club.Equals("") || clubLoc.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }


            SqlCommand checkClubNameTaken = new SqlCommand("SELECT dbo.checkClubNameTaken(@name)", conn);
            checkClubNameTaken.Parameters.Add(new SqlParameter("@name", clubName.Text));

            conn.Open();
            Boolean result = (Boolean)checkClubNameTaken.ExecuteScalar();

           

            if (result)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Club name is already taken!')", true);

            }
            else
            {

                SqlCommand addClub = new SqlCommand("addClub", conn);
                addClub.CommandType = CommandType.StoredProcedure;
                addClub.Parameters.Add(new SqlParameter("@clubname", club));
                addClub.Parameters.Add(new SqlParameter("@location", location));
                addClub.ExecuteNonQuery();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Club successfully added!')", true);
            }
            conn.Close();

        }
        protected void onDeleteClub(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);


            String club = dClubName.Text;

            if (club.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }

            SqlCommand checkClubNameTaken = new SqlCommand("SELECT dbo.checkClubNameTaken(@name)", conn);
            checkClubNameTaken.Parameters.Add(new SqlParameter("@name", club));

            conn.Open();
            Boolean result = (Boolean)checkClubNameTaken.ExecuteScalar();


            if (result)
            {
                
                SqlCommand deleteClub = new SqlCommand("deleteClub", conn);
                deleteClub.CommandType = CommandType.StoredProcedure;
                deleteClub.Parameters.Add(new SqlParameter("@clubName", club));
                deleteClub.ExecuteNonQuery();

                //not tested
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Club successfully deleted!')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Club does not exist!')", true);


            }
            conn.Close();

        }
        protected void onAddStadium(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String stadium = stadiumName.Text;
            String location = stadiumLoc.Text;
            String capacity = stadiumCapacity.Text;

            if (stadium.Equals("") || location.Equals("") || capacity.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }

            SqlCommand checkStadiumNameTaken = new SqlCommand("SELECT dbo.checkStadiumNameTaken(@name)", conn);
            checkStadiumNameTaken.Parameters.Add(new SqlParameter("@name", stadium));

            conn.Open();
            Boolean result = (Boolean)checkStadiumNameTaken.ExecuteScalar();


            if (result)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Stadium name already taken!')", true);

            }
            else
            {
                SqlCommand addStadium = new SqlCommand("addStadium", conn);
                addStadium.CommandType = CommandType.StoredProcedure;
                addStadium.Parameters.Add(new SqlParameter("@stadiumName", stadium));
                addStadium.Parameters.Add(new SqlParameter("@location", location));
                addStadium.Parameters.Add(new SqlParameter("@capacity", capacity));
                addStadium.ExecuteNonQuery();

                //not tested
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Stadium successfully added!')", true);
            }
            conn.Close();

        }
        protected void onDeleteStadium(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);


            String stadium = dStadiumName.Text;
          

            if (stadium.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }

            SqlCommand checkStadiumNameTaken = new SqlCommand("SELECT dbo.checkStadiumNameTaken(@name)", conn);
            checkStadiumNameTaken.Parameters.Add(new SqlParameter("@name", stadium));

            conn.Open();
            Boolean result = (Boolean)checkStadiumNameTaken.ExecuteScalar();


            if (result)
            {
                SqlCommand deleteStadium = new SqlCommand("deleteStadium", conn);
                deleteStadium.CommandType = CommandType.StoredProcedure;
                deleteStadium.Parameters.Add(new SqlParameter("@stadiumName", dStadiumName.Text));
                deleteStadium.ExecuteNonQuery();

                //not tested
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Stadium successfully deleted!')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Stadium does not exist!')", true);



            }
            conn.Close();

        }
        protected void onBlockFan(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Fantasy"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String nid = bFanNID.Text;


            if (nid.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields cannot be empty')", true);
                return;
            }

            SqlCommand checkFanExists = new SqlCommand("SELECT dbo.checkFanExists(@nationalID)", conn);
            checkFanExists.Parameters.Add(new SqlParameter("@nationalID", nid));

            conn.Open();
            Boolean result = (Boolean)checkFanExists.ExecuteScalar();


            if (result)
            {
                SqlCommand blockFan = new SqlCommand("blockFan", conn);
                blockFan.CommandType = CommandType.StoredProcedure;
                blockFan.Parameters.Add(new SqlParameter("@nid", bFanNID.Text));
                blockFan.ExecuteNonQuery();

                //not tested
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fan successfully blocked!')", true);

            }
            else
            {
                //Response.Write("");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fan does not exist!')", true);

            }
            conn.Close();

        }


    }
}