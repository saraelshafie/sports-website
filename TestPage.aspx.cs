using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fantasy
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            
        }

        protected void onPrint(object sender, EventArgs e)
        {
            Debug.WriteLine("Print:" + test.Text);

            Debug.WriteLine(test.Text.Equals(""));


        }

       
    }
}