using PNJ.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace PNJ.Controllers
{
    public class PNJController : ApiController
    {
        private Admin_TCBEntities db = new Admin_TCBEntities();
        private string ConnectSDP = ConfigurationManager.AppSettings["ConnectSDP"];

        private JObject ConnectDB(string query)
        {
            var conn = new SqlConnection();
            conn.ConnectionString = ConnectSDP;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = query;
            var reader = cmd.ExecuteReader();
            JObject model = JObject.FromObject(new { data = Readers(reader) });
            return model;
        }
      
        private IEnumerable<IDictionary<object, object>> Readers(DbDataReader reader)
        {
            while (reader.Read())
            {
                IDictionary<object, object> values = new Dictionary<object, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));
                }
                yield return values;
            }
        }

        [HttpGet]
        [ActionName("getDomain")]
        public JToken getDomain()
        {
            var temp = db.ConfigADs.Where(x => x.isActive == true).Select(x => new { x.title, x.directoryPath });
            if (temp.Any())
            {
                return JObject.FromObject(new { status = 1, data = temp, mgs = "" });
            }
            else
                return JObject.FromObject(new { status = 0, data = "", mgs = "" });
        }

        [HttpGet]
        [ActionName("getMenu")]
        public JToken getMenu(string id)
        {
            var temp = db.sp_Navigation(id).ToList();
            if (temp.Any())
            {
                return JObject.FromObject(new { status = 0, data = temp, mgs = "" });
            }
            else
                return JObject.FromObject(new { status = 0, data = "", mgs = "" });
        }

        [HttpGet]
        [ActionName("getservicecategorymap")]
        public JToken getservicecategorymap(int id)
        {
            var temp = db.sp_getIdServicecategorymapcategory2(id).ToList();
            if (temp.Any())
            {
               
                return JObject.FromObject(new { status = 0, data2 = temp, mgs = "" });
               
            }
            else
                return JObject.FromObject(new { status = 0, data2 = "", mgs = "" });
        }

        [HttpGet]
        [ActionName("getFilter_1")]
        public JToken getFilter_1(int type, string condition)
        {
            StringBuilder query = new StringBuilder("");
            switch (type)
            {
                case 0:
                    query = new StringBuilder("SELECT dpt.DEPTID,dpt.DEPTNAME AS Department  FROM WorkOrder wo LEFT JOIN DepartmentDefinition dpt ON wo.DEPTID=dpt.DEPTID WHERE (wo.ISPARENT='1') and dpt.DEPTID is not null group by dpt.DEPTID,dpt.DEPTNAME");
                    break;
                case 1:
                    query = new StringBuilder("SELECT cd.CATEGORYNAME AS Category,cd.CATEGORYID  FROM WorkOrder wo LEFT JOIN ServiceDefinition serdef ON wo.SERVICEID=serdef.SERVICEID LEFT JOIN WorkOrderStates wos ON wo.WORKORDERID=wos.WORKORDERID LEFT JOIN CategoryDefinition cd ON wos.CATEGORYID=cd.CATEGORYID WHERE (wo.ISPARENT='1') and serdef.SERVICEID='"+condition+ "' group by cd.CATEGORYNAME,cd.CATEGORYID");
                    break;
                case 2:
                    query = new StringBuilder("select subct.SUBCATEGORYID,subct.NAME from SubCategoryDefinition subct where subct.CATEGORYID='"+condition+"' group by subct.SUBCATEGORYID,subct.NAME");
                    break;
                case 3:
                    query = new StringBuilder("select it.ITEMID,it.NAME from ItemDefinition it where it.SUBCATEGORYID='"+condition+"' group by it.ITEMID,it.NAME");
                    break;
                case 4:
                    query = new StringBuilder("SELECT  ti.USER_ID,ti.FIRST_NAME AS Technician  FROM WorkOrder wo LEFT JOIN WorkOrderStates wos ON wo.WORKORDERID=wos.WORKORDERID LEFT JOIN SDUser td ON wos.OWNERID=td.USERID LEFT JOIN AaaUser ti ON td.USERID=ti.USER_ID LEFT JOIN WorkOrder_Queue woq ON wo.WORKORDERID=woq.WORKORDERID LEFT JOIN QueueDefinition qd ON woq.QUEUEID=qd.QUEUEID WHERE (wo.ISPARENT='1') and ti.USER_ID IS NOT NULL and qd.QUEUEID='" + condition+"' group by ti.USER_ID,ti.FIRST_NAME");
                    break;
            }
            try
            {
                return ConnectDB(query.ToString());
            }
            catch (System.Exception ex)
            {
                return JObject.FromObject(new { mgs = ex.Message });
            }
        }

        [HttpGet]
        [ActionName("getFilter")]
        public JToken getFilter(int type, string condition)
        {
            StringBuilder query = new StringBuilder("");
            switch (type)
            {
                //Request
                #region
                case 0:
                    
                    query = new StringBuilder("SELECT DISTINCT sd.STATUSID, sd.STATUSNAME FROM StatusDefinition sd");
                    break;

                case 1:
                    query = new StringBuilder("SELECT DISTINCT wos.ISOVERDUE  FROM WorkOrder wo  LEFT JOIN WorkOrderStates wos  ON wo.WORKORDERID = wos.WORKORDERID  WHERE wos.ISOVERDUE IS NOT NULL");
                    break;

                case 2:
                    query = new StringBuilder("SELECT DISTINCT  aau.FIRST_NAME, aau.USER_ID FROM WorkOrder wo LEFT JOIN SDUser sdu ON wo.REQUESTERID=sdu.USERID LEFT JOIN AaaUser aau ON sdu.USERID=aau.USER_ID ");
                    break;

                case 3:
                    query = new StringBuilder("select distinct rtd.REQUESTTYPEID,rtd.NAME from RequestTypeDefinition rtd");
                    break;

                case 4:
                    query = new StringBuilder("SELECT DISTINCT QueueDefinition.QUEUEID,QueueDefinition.QUEUENAME FROM QueueDefinition");
                    break;

                case 5:
                    query = new StringBuilder("select distinct  aa.FIRST_NAME,aa.USER_ID, que.QUEUENAME, que.QUEUEID from AaaUser aa left join SDUser sd on aa.USER_ID = sd.USERID left join	HelpDeskCrew he on he.TECHNICIANID = sd.USERID left join Queue_Technician quetech on quetech.TECHNICIANID = he.TECHNICIANID left join QueueDefinition que on que.QUEUEID = quetech.QUEUEID where que.QUEUEID = " + condition);
                    break;

                case 6:
                    query = new StringBuilder("select distinct pd.PRIORITYID,pd.PRIORITYNAME from PriorityDefinition pd");
                    break;

                case 7:
                    query = new StringBuilder("select distinct cd.CATEGORYID, cd.CATEGORYNAME from CategoryDefinition cd");
                    break;

                case 8:
                    query = new StringBuilder("SELECT distinct scd.NAME , scd.SUBCATEGORYID FROM  SubCategoryDefinition scd LEFT JOIN CategoryDefinition cd ON scd.CATEGORYID=cd.CATEGORYID where scd.NAME is not null  and cd.CATEGORYID = " + condition);
                    break;

                case 9:
                    query = new StringBuilder("SELECT DISTINCT icd.NAME, icd.ITEMID FROM ItemDefinition icd LEFT JOIN SubCategoryDefinition scd ON scd.SUBCATEGORYID = icd.SUBCATEGORYID WHERE icd.NAME IS NOT NULL AND icd.SUBCATEGORYID = " + condition);
                    break;
                #endregion
                //Contract
                #region
                case 10:
                    query = new StringBuilder("SELECT DISTINCT cc.CATEGORYID, cc.CATEGORYNAME FROM ContractCategory cc");
                    break;

                case 11:
                    query = new StringBuilder("SELECT DISTINCT cs.STATUSID, cs.STATUSNAME FROM ContractStatus cs");
                    break;
                case 12:
                    query = new StringBuilder("SELECT DISTINCT s.ORG_ID, s.NAME FROM SDOrganization s");
                    break;
                case 13:
                    query = new StringBuilder("select DISTINCT top 50000  qd.QUEUENAME AS [Group], qd.QUEUEID FROM WorkOrder wo LEFT JOIN WorkOrder_Queue woq ON wo.WORKORDERID=woq.WORKORDERID LEFT JOIN QueueDefinition qd ON woq.QUEUEID=qd.QUEUEID WHERE (wo.ISPARENT='1') group by qd.QUEUENAME, qd.QUEUEID");
                    break;
                case 14:
                    query = new StringBuilder("SELECT DISTINCT serdef.SERVICEID,serdef.NAME AS ServiceCategory FROM WorkOrder wo LEFT JOIN ServiceDefinition serdef ON wo.SERVICEID=serdef.SERVICEID LEFT JOIN WorkOrderStates wos ON wo.WORKORDERID=wos.WORKORDERID LEFT JOIN CategoryDefinition cd ON wos.CATEGORYID=cd.CATEGORYID WHERE (wo.ISPARENT='1') group by serdef.NAME, serdef.SERVICEID");
                    break;
                case 15:
                    query = new StringBuilder("SELECT DISTINCT sd.NAME, sd.SERVICEID FROM dbo.ServiceDefinition sd WHERE sd.STATUS ='ACTIVE'  GROUP BY sd.NAME, sd.SERVICEID");
                    break;
                case 16:
                    query = new StringBuilder("SELECT DISTINCT  AaaUser.FIRST_NAME ,SDUser.Employeeid ,DepartmentDefinition.DEPTNAME FROM AaaUser LEFT JOIN UserDepartment ON AaaUser.USER_ID = UserDepartment.USERID  LEFT JOIN DepartmentDefinition ON UserDepartment.DEPTID = DepartmentDefinition.DEPTID LEFT JOIN SiteDefinition ON DepartmentDefinition.SITEID = SiteDefinition.SITEID LEFT JOIN SDOrganization ON SiteDefinition.SITEID = SDOrganization.ORG_ID INNER JOIN SDUser ON AaaUser.USER_ID = SDUser.USERID LEFT JOIN HelpDeskCrew ON SDUser.USERID = HelpDeskCrew.TECHNICIANID LEFT JOIN Requester_Fields ON SDUser.USERID = Requester_Fields.USERID LEFT JOIN AaaLogin ON AaaUser.USER_ID = AaaLogin.USER_ID WHERE DepartmentDefinition.DEPTID = " + condition + " AND SDUser.STATUS = 'ACTIVE'  ORDER BY 1");
                    break;
                #endregion
                default:
                    break;
            }
            try
            {
                return ConnectDB(query.ToString());
            }
            catch (System.Exception ex)
            {
                return JObject.FromObject(new { mgs = ex.Message });
            }
        }
        
    }
}