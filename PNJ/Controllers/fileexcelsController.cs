using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace PNJ.Controllers
{
    public class fileexcelsController : Controller
    {
        private string ConnectSDP = ConfigurationManager.AppSettings["ConnectSDP"];
        // GET: fileexcels
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            DataSet ds1 = new DataSet();
            DataSet ds = new DataSet();
            string sql = "SELECT  sd.NAME, sd.SERVICEID FROM dbo.ServiceDefinition sd WHERE sd.STATUS ='ACTIVE'  GROUP BY sd.NAME, sd.SERVICEID";
            SqlConnection _connection = new SqlConnection(ConnectSDP);
           
            SqlCommand com = new SqlCommand(sql, _connection);
            SqlDataAdapter adap = new SqlDataAdapter(com);
            adap.Fill(ds1);

        
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension =
                                     System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {

                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {

                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                }
                if (fileExtension.ToString().ToLower().Equals(".xml"))
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

                    Request.Files["FileUpload"].SaveAs(fileLocation);
                    XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                    // lay danh muc service category tu excel len
                    ds.ReadXml(xmlreader);
                    xmlreader.Close();
                }
                //Lay service catalog
                DataTable dtb = ds1.Tables[0];
              
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    
                    for (int j= 0; j < ds.Tables[0].Rows.Count; j++)
                    {

                        if (dtb.Rows[i][0].ToString().Contains(ds.Tables[0].Rows[j][0].ToString()))
                        {
                            //lay danh muc dich vu catagory
                            DataSet ds2 = new DataSet();
                            string sql2 = "select distinct cd.CATEGORYID, cd.CATEGORYNAME from CategoryDefinition cd where cd.CATEGORYNAME = N'" + ds.Tables[0].Rows[j][2].ToString()+"'";
                            SqlConnection _connection2 = new SqlConnection(ConnectSDP);

                            SqlCommand com2 = new SqlCommand(sql2, _connection);
                            SqlDataAdapter adap2 = new SqlDataAdapter(com2);
                            adap2.Fill(ds2);
                            DataTable dtb2 = ds2.Tables[0];
                           
                                //insert du lieu vao
                                string conn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                                SqlConnection con = new SqlConnection(conn);
                                string query = "Insert into ServiceCategoryMapCategory (idServices,IdCategory,NameCategory) Values('" + dtb.Rows[i][1].ToString() + "','" + dtb2.Rows[0]["CATEGORYID"].ToString() + "',N'" + dtb2.Rows[0]["CATEGORYNAME"].ToString() + "')";
                                con.Open();
                                SqlCommand cmd = new SqlCommand(query, con);
                                cmd.ExecuteNonQuery();
                                con.Close();
                            
                           
                        }


                        //string conn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                        //SqlConnection con = new SqlConnection(conn);
                        //string query = "Insert into ServiceCategoryMapCategory (idServices,IdCategory,NameCategory) Values('" + ds.Tables[0].Rows[i][0].ToString() + "','" + ds.Tables[0].Rows[i][1].ToString() + "',N'" + ds.Tables[0].Rows[i][2].ToString() + "')";
                        //con.Open();
                        //SqlCommand cmd = new SqlCommand(query, con);
                        //cmd.ExecuteNonQuery();
                        //con.Close();
                    }
                }
               

            }
            return View();
        }

        // GET: fileexcels
        public ActionResult Indexfileexcel()
        {
            return View();
        }
    }
}