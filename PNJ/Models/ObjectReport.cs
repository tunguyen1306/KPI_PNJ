using System;

namespace PNJ.Models
{

   
    public class ObjectRequests
    {
        public string tungaytao { get; set; }
        public string denngaytao { get; set; }
        public string tungayre { get; set; }
        public string denngayre { get; set; }
        public string cd { get; set; }
        public string scd { get; set; }
        public string std { get; set; }
        public string it { get; set; }
        public string pd { get; set; }
        public string group { get; set; }
        public string id { get; set; }
        public string rqt { get; set; }
        public string requester { get; set; }
        public string tech { get; set; }
        public string isoverdue { get; set; }
        public string nameReport { get; set; }
        public string depart { get; set; }
        public string itd { get; set; }

        public string dateTime_thang { get; set; }
        public string dateTime_ThangNam { get; set; }
        public string dateTime_quynam { get; set; }
        public string Quy { get; set; }
        public string dateTime_nam { get; set; }
        public string condition { get; set; }
        public string typeaction { get; set; }
        public string optradio1 { get; set; }
        public string subcd { get; set; }
        public string departname { get; set; }
        public string itname { get; set; }
        public string techname { get; set; }

    }
    public class GetMonth
    {
        public string TuThang { get; set; }
        public string DenThang { get; set; }
     
    }
    public class ObjectContract
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ContractType { get; set; }
        public string Status { get; set; }
        public string Vender { get; set; }
        public string ContractID { get; set; }
        public string nameReport { get; set; }
    }
    public class ObjectChange
    {
        public string CreateFrom { get; set; }
        public string CreateTo { get; set; }
        public string ScheduleStarFrom { get; set; }
        public string ScheduleStartTo { get; set; }
        public string ScheduleEndFrom { get; set; }
        public string ScheduleEndTo { get; set; }
        public string Service { get; set; }
        public string Group { get; set; }
        public string Technician { get; set; }
        public string CRStatus { get; set; }
        public string TypeCR { get; set; }
        public string nameReport { get; set; }
    }
}