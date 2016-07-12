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
            //if (true)
            //{
            //    if (Session["user"] == null)
            //    {
            //        return RedirectToAction("Index", "Login");
            //        //Response.Redirect("~/Login");
            //    }
            //}
            ViewBag.NameReport = type;
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ReportsR(ObjectRequests data, FormCollection form)
        {


            string radio = form["optradio"];
            DateTime tungaytao;
            DateTime denngaytao;
            DateTime tungayre;
            DateTime denngayre;
            var hdtungaytao = string.Empty;
            var hddenngaytao = string.Empty;
            var hdtungayre = string.Empty;
            var hddenngayre = string.Empty;
            var hdconditione = string.Empty;
            var hdtypeaction = string.Empty;
            if (radio == "ngay" || radio == null)
            {


                DateTime.TryParseExact(data.tungaytao ?? "01/01/2000", "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tungaytao);
                DateTime.TryParseExact(data.denngaytao ?? DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out denngaytao);
                DateTime.TryParseExact(data.tungayre ?? "01/01/2000", "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tungayre);
                DateTime.TryParseExact(data.denngayre ?? DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out denngayre);
                hdtungaytao = tungaytao.ToString("yyyy-MM-dd");
                hddenngaytao = denngaytao.ToString("yyyy-MM-dd");
                hdtungayre = tungayre.ToString("yyyy-MM-dd");
                hddenngayre = denngayre.ToString("yyyy-MM-dd");
                hdconditione = "1";
            }
            if (radio == "thang" || radio == null)
            {
                var thang = data.dateTime_thang;
                var fithang = thang.Split(',');
                DateTime createDate = new DateTime(Int32.Parse(data.dateTime_ThangNam), Int32.Parse(fithang[fithang.Length-1]), 1).AddMonths(1).AddDays(-1);

                data.tungaytao = ("01/" + fithang[0] + "/" + data.dateTime_ThangNam);
                data.denngaytao = Convert.ToString(createDate.ToString("yyyy-MM-dd"));
                hdtungaytao = DateTime.Parse(data.tungaytao).ToString("yyyy-MM-dd");
                hddenngaytao = data.denngaytao;

                data.tungayre = ("01/" + fithang[0] + "/" + data.dateTime_ThangNam);
                data.denngayre = Convert.ToString(createDate.ToString("yyyy-MM-dd"));
                hdtungayre = DateTime.Parse(data.tungayre).ToString("yyyy-MM-dd");
                hddenngayre = data.denngaytao;
                hdconditione = "2";

            }
            if (radio == "quy")
            {
                var quy = data.Quy;
                var fiquy = quy.Split(',');
                var quybd = fiquy[0];
                var quykt = fiquy[fiquy.Length-1];
                GetMonth gtmkt = GetTime(quykt);
                GetMonth gtmbd = GetTime(quybd);
                DateTime createDate = new DateTime(Int32.Parse(data.dateTime_quynam), Int32.Parse(gtmkt.DenThang), 1).AddMonths(1).AddDays(-1);

                data.tungaytao = ("01/" + gtmbd.TuThang + "/" + data.dateTime_quynam);
                data.denngaytao = Convert.ToString(createDate.ToString("yyyy-MM-dd"));
                hdtungaytao = DateTime.Parse(data.tungaytao).ToString("yyyy-MM-dd");
                hddenngaytao = data.denngaytao;

                data.tungayre = ("01/" + gtmbd.TuThang + "/" + data.dateTime_quynam);
                data.denngayre = Convert.ToString(createDate.ToString("yyyy-MM-dd"));
                hdtungayre = DateTime.Parse(data.tungayre).ToString("yyyy-MM-dd");
                hddenngayre = data.denngaytao;
                hdconditione = "3";
            }
            if (radio == "nam")
            {
                var nam = data.dateTime_nam;
                var finam = nam.Split(',');

                DateTime createDate = new DateTime(Int32.Parse(finam[finam.Length-1]), 12, 1).AddMonths(1).AddDays(-1);

                data.tungaytao = "01/01/" + finam[0];
                data.denngaytao = Convert.ToString(createDate.ToString("yyyy-MM-dd"));
                hdtungaytao = DateTime.Parse(data.tungaytao).ToString("yyyy-MM-dd");
                hddenngaytao = data.denngaytao;



                data.tungayre = "01/01/" + finam[0];
                data.denngayre = Convert.ToString(createDate.ToString("yyyy-MM-dd"));
                hdtungayre = DateTime.Parse(data.tungayre).ToString("yyyy-MM-dd");
                hddenngayre = data.denngayre;
                hdconditione = "4";
            }
            if (data.optradio1 == "ITSuportgroup")
            {
                if (data.group == null)
                {
                    hdtypeaction = "1";

                }
                else
                {
                    if (data.tech != null)
                    {
                        hdtypeaction = "2";
                    }
                    else
                    {
                        hdtypeaction = "1";
                    }
                }
            }
            if (data.optradio1 == "services")
            {


                if (data.scd == null)
                {
                    hdtypeaction = "3";
                }
                else
                {
                    if (data.cd != null)
                    {
                        if (data.subcd != null)
                        {

                            if (data.it != null)
                            {
                                hdtypeaction = "6";
                            }
                            else
                            {
                                hdtypeaction = "5";
                            }
                        }
                        else
                        {

                            hdtypeaction = "4";

                        }

                    }
                    else
                    {
                        hdtypeaction = "3";
                    }
                }
            }
            if (data.optradio1 == "customer")
            {
                if (data.depart == null)
                {
                    hdtypeaction = "7";

                }
                else
                {
                    if (data.requester != null)
                    {
                        hdtypeaction = "8";
                    }
                    else
                    {
                        hdtypeaction = "7";
                    }
                }
            }
            if (hdtypeaction=="")
            {
                hdtypeaction = "1";
            }
            var hdrqt = string.Empty;
     
            if (data.nameReport.Contains("ReportKPI_PNJ_R"))
            {
                hdrqt = "301";
            }
            if (data.nameReport.Contains("ReportKPI_PNJ_I"))
            {
                hdrqt = "302";
            }
            if (data.nameReport.Contains("ReportKPI_PNJ_ALL01"))
            {
                hdrqt = "302,301,303";
            }
            if (data.nameReport.Contains("ReportKPI_PNJ_OLA01"))

            {
                hdrqt = "302,301,303";
            }

            ReportParameter[] rsp = new[] {
                new ReportParameter("tungaytao", hdtungaytao),
                new ReportParameter("denngaytao", hddenngaytao),
                 new ReportParameter("tungayre", hdtungayre),
                new ReportParameter("denngayre",hddenngayre),
                new ReportParameter("cd",data.cd ?? "none"),
                new ReportParameter("scd",data.scd ?? "none"),
                new ReportParameter("std", data.itd ?? "none" ),
                new ReportParameter("it",data.it ?? "none"),
                 new ReportParameter("depart",data.depart ?? "none"),
                new ReportParameter("group",data.group ?? "none"),
                new ReportParameter("id",data.id ?? "none"),
                new ReportParameter("rqt",hdrqt ?? "none"),
                new ReportParameter("condition",hdconditione ?? "1"),
                 new ReportParameter("typeaction",hdtypeaction ?? "1"),
                new ReportParameter("requester",data.requester?? "none"),
                new ReportParameter("tech",data.tech ?? "none"),
                  new ReportParameter("subcd",data.subcd ?? "none"),
                new ReportParameter("pd",data.isoverdue ?? "none")
             
            };
            ViewBag.Report = fn.ContentReport(rsp, data.nameReport, db);


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