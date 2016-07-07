using Microsoft.Reporting.WebForms;
using System;
using System.Web.Mvc;
using PNJ.Models;
using System.Collections.Generic;

namespace PNJ.Controllers
{
    public class ReportPnjController : Controller
    {
        private FunctionService fn = new FunctionService();
        private Admin_TCBEntities db = new Admin_TCBEntities();
        // GET: Report
        public ActionResult Requests(string type)
        {
            if (true)
            {
                if (Session["user"] == null)
                {
                    return RedirectToAction("Index", "Login");
                    //Response.Redirect("~/Login");
                }
            }
            ViewBag.NameReport = type;
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportsR([Bind(Include = "tungaytao,denngaytao,cd,it,scd,std,pd,group,nameReport,rqt,isoverdue,tech,id,requester,Item,dateTime_thang,dateTime_ThangNam,dateTime_quynam,Quy,dateTime_nam")] ObjectRequests data,FormCollection form)
        {
           

            string radio = form["optradio"];
           
            if (radio == "ngay" || radio==null)
            {
                DateTime tungaytao;
                DateTime denngaytao;
                DateTime tungayre;
                DateTime denngayre;

                DateTime.TryParseExact(data.tungaytao ?? "01/01/2000", "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tungaytao);
                DateTime.TryParseExact(data.denngaytao ?? DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out denngaytao);
                DateTime.TryParseExact(data.tungayre ?? "01/01/2000", "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tungayre);
                DateTime.TryParseExact(data.denngayre ?? DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out denngayre);

                ReportParameter[] rsp = new[] {
                new ReportParameter("tungaytao", tungaytao.ToString("yyyy-MM-dd")),
                new ReportParameter("denngaytao", denngaytao.ToString("yyyy-MM-dd")),
                 new ReportParameter("tungayre", tungayre.ToString("yyyy-MM-dd")),
                new ReportParameter("denngayre",denngayre.ToString("yyyy-MM-dd")),
                new ReportParameter("cd",data.cd ?? "none"),
                new ReportParameter("scd",data.scd ?? "none"),
                new ReportParameter("std", data.itd ?? "none" ),
                new ReportParameter("it",data.it ?? "none"),
                new ReportParameter("group",data.group ?? "none"),
                new ReportParameter("id",data.id ?? "none"),
                new ReportParameter("rqt",data.rqt ?? "none"),
                //new ReportParameter("requester",data.requester?? "none"),
                //new ReportParameter("tech",data.tech ?? "none"),
                //new ReportParameter("pd",data.isoverdue ?? "none")
            };
                ViewBag.Report = fn.ContentReport(rsp, data.nameReport, db);
            }
            else if (radio == "thang")
            {
                DateTime createDate = new DateTime(Int32.Parse(data.dateTime_ThangNam), Int32.Parse(data.dateTime_thang), 1).AddMonths(1).AddDays(-1);
                data.tungaytao = "01/" + data.dateTime_thang + "/" + data.dateTime_ThangNam;
                data.denngaytao = Convert.ToString(createDate.ToString("dd/MM/yyyy"));


                DateTime tungaytao;
                DateTime denngaytao;
                DateTime tungayre;
                DateTime denngayre;

                DateTime.TryParseExact(data.tungaytao ?? "01/01/2000", "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tungaytao);
                DateTime.TryParseExact(data.denngaytao ?? DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out denngaytao);
                DateTime.TryParseExact(data.tungayre ?? "01/01/2000", "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tungayre);
                DateTime.TryParseExact(data.denngayre ?? DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out denngayre);

                ReportParameter[] rsp = new[] {
                new ReportParameter("tungaytao", tungaytao.ToString("yyyy-MM-dd")),
                new ReportParameter("denngaytao", denngaytao.ToString("yyyy-MM-dd")),
                 new ReportParameter("tungayre", tungayre.ToString("yyyy-MM-dd")),
                new ReportParameter("denngayre",denngayre.ToString("yyyy-MM-dd")),
                new ReportParameter("cd",data.cd ?? "none"),
                new ReportParameter("scd",data.scd ?? "none"),
                new ReportParameter("std", data.itd ?? "none" ),
                new ReportParameter("it",data.it ?? "none"),
                new ReportParameter("group",data.group ?? "none"),
                new ReportParameter("id",data.id ?? "none"),
                new ReportParameter("rqt",data.rqt ?? "none"),
                //new ReportParameter("requester",data.requester?? "none"),
               // new ReportParameter("tech",data.tech ?? "none"),
                new ReportParameter("pd",data.isoverdue ?? "none")
            };
                ViewBag.Report = fn.ContentReport(rsp, data.nameReport, db);
            }
            else if (radio == "quy")
            {

                GetMonth gtm = GetTime(data.Quy);
                DateTime createDate = new DateTime(Int32.Parse(data.dateTime_quynam), Int32.Parse(gtm.DenThang), 1).AddMonths(1).AddDays(-1);

                data.tungaytao = "01/" + gtm.TuThang + "/" + data.dateTime_quynam;
                data.denngaytao = Convert.ToString(createDate.ToString("dd/MM/yyyy"));


                DateTime tungaytao;
                DateTime denngaytao;
                DateTime tungayre;
                DateTime denngayre;

                DateTime.TryParseExact(data.tungaytao ?? "01/01/2000", "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tungaytao);
                DateTime.TryParseExact(data.denngaytao ?? DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out denngaytao);
                DateTime.TryParseExact(data.tungayre ?? "01/01/2000", "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tungayre);
                DateTime.TryParseExact(data.denngayre ?? DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out denngayre);

                ReportParameter[] rsp = new[] {
                new ReportParameter("tungaytao", tungaytao.ToString("yyyy-MM-dd")),
                new ReportParameter("denngaytao", denngaytao.ToString("yyyy-MM-dd")),
                 new ReportParameter("tungayre", tungayre.ToString("yyyy-MM-dd")),
                new ReportParameter("denngayre",denngayre.ToString("yyyy-MM-dd")),
                new ReportParameter("cd",data.cd ?? "none"),
                new ReportParameter("scd",data.scd ?? "none"),
                new ReportParameter("std", data.itd ?? "none" ),
                new ReportParameter("it",data.it ?? "none"),
                new ReportParameter("group",data.group ?? "none"),
                new ReportParameter("id",data.id ?? "none"),
                new ReportParameter("rqt",data.rqt ?? "none"),
                //new ReportParameter("requester",data.requester?? "none"),
               // new ReportParameter("tech",data.tech ?? "none"),
                new ReportParameter("pd",data.isoverdue ?? "none")
            };
                ViewBag.Report = fn.ContentReport(rsp, data.nameReport, db);

            }
            else if (radio == "nam")
            {
              
                DateTime createDate = new DateTime(Int32.Parse(data.dateTime_nam), 12, 1).AddMonths(1).AddDays(-1);

                data.tungaytao = "01/01/" + data.dateTime_nam;
                data.denngaytao = Convert.ToString(createDate.ToString("dd/MM/yyyy"));


                DateTime tungaytao;
                DateTime denngaytao;
                DateTime tungayre;
                DateTime denngayre;

                DateTime.TryParseExact(data.tungaytao ?? "01/01/2000", "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tungaytao);
                DateTime.TryParseExact(data.denngaytao ?? DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out denngaytao);
                DateTime.TryParseExact(data.tungayre ?? "01/01/2000", "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tungayre);
                DateTime.TryParseExact(data.denngayre ?? DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out denngayre);

                ReportParameter[] rsp = new[] {
                new ReportParameter("tungaytao", tungaytao.ToString("yyyy-MM-dd")),
                new ReportParameter("denngaytao", denngaytao.ToString("yyyy-MM-dd")),
                 new ReportParameter("tungayre", tungayre.ToString("yyyy-MM-dd")),
                new ReportParameter("denngayre",denngayre.ToString("yyyy-MM-dd")),
                new ReportParameter("cd",data.cd ?? "none"),
                new ReportParameter("scd",data.scd ?? "none"),
                new ReportParameter("std", data.itd ?? "none" ),
                new ReportParameter("it",data.it ?? "none"),
                new ReportParameter("group",data.group ?? "none"),
                new ReportParameter("id",data.id ?? "none"),
                new ReportParameter("rqt",data.rqt ?? "none"),
                //new ReportParameter("requester",data.requester?? "none"),
               // new ReportParameter("tech",data.tech ?? "none"),
                new ReportParameter("pd",data.isoverdue ?? "none")
            };
                ViewBag.Report = fn.ContentReport(rsp, data.nameReport, db);
            }
            return PartialView("../Partial/ReportViewer");
        }
        public GetMonth GetTime(string value)
        {
            GetMonth mt = new GetMonth();
            if (value == "1")
            {

                mt.TuThang = "01";
                mt.DenThang = "03";

            }
            else if (value == "2")
            {
                mt.TuThang = "04";
                mt.DenThang = "06";
            }
            else if (value == "3")
            {
                mt.TuThang = "07";
                mt.DenThang = "09";
            }
            else if (value == "4")
            {
                mt.TuThang = "10";
                mt.DenThang = "12";
            }
            return mt;
        }
    }
}