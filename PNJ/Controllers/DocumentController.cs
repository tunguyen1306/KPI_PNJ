using Microsoft.Reporting.WebForms;
using System;
using System.Web.Mvc;
using PNJ.Models;
namespace PNJ.Controllers
{
    public class DocumentController : Controller
    {
        private Admin_TCBEntities db = new Admin_TCBEntities();
        private FunctionService fn = new FunctionService();
        // GET: Document
        public ActionResult PaperProposed(int? type)
        {
            ViewBag.id = type ?? 0;
            return View();
        }
        [HttpPost]
        public ActionResult Theform(string type, string id)
        {
            ObjectRequests ore = new ObjectRequests();
            ore.nameReport = type;
            ore.id = id;
            return ReportRequests(ore);
        }
        
        private ActionResult ReportRequests(ObjectRequests data)
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
                new ReportParameter("std", data.std ?? "none" ),
                new ReportParameter("it",data.it ?? "none"),
                new ReportParameter("pd",data.pd?? "none"),
                new ReportParameter("group",data.group ?? "none"),
                new ReportParameter("id",data.id ?? "none"),
                new ReportParameter("rqt",data.rqt ?? "none"),
                //new ReportParameter("requester",data.requester?? "none"),
                //new ReportParameter("tech",data.tech ?? "none"),
                //new ReportParameter("isoverdue",data.isoverdue ?? "none")
            };
            ViewBag.Report = fn.ContentReport(rsp, data.nameReport, db);
            return PartialView("../Partial/ReportViewer");
        }

    }
}