using Microsoft.Reporting.WebForms;
using PNJ.Cerdential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PNJ.Models
{
    public class FunctionService
    {
        public ReportViewer ContentReport(ReportParameter[] rpt, string NameReport, Admin_TCBEntities db)
        {
            var temp = db.ConfigReportServers.Where(x => x.isActive == true).FirstOrDefault();
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            reportViewer.ServerReport.ReportPath = temp.reportPath + NameReport;
            //reportViewer.ServerReport.ReportPath = temp.reportPath + "Request_TiLeDungSLA_Response";
            reportViewer.ServerReport.ReportServerUrl = new Uri(temp.reportUri);
            reportViewer.ServerReport.ReportServerCredentials = new CustomReportCredentials(temp.userName, temp.passWord, temp.domain);
            reportViewer.Height = 550;
            reportViewer.ServerReport.SetParameters(rpt);
            reportViewer.ServerReport.Refresh();
            return reportViewer;
        }
    }
}