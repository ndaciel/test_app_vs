/* documentation
 *001
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class ReasonFacade : Base.Manager
    {
        
        public static List<Model.mdlReason> LoadReason()
        {
            var mdlReasonList = new List<Model.mdlReason>();
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtReason = Manager.DataFacade.DTSQLCommand("SELECT * FROM Reason", sp);
            foreach (DataRow drReason in dtReason.Rows)
            {
                var mdlReason = new Model.mdlReason();
                mdlReason.ReasonID = drReason["ReasonID"].ToString();
                mdlReason.ReasonType = drReason["ReasonType"].ToString();
                mdlReason.Value = drReason["Value"].ToString();
                mdlReasonList.Add(mdlReason);
            }
            return mdlReasonList;
        }

        public static List<Model.mdlReason> GetSearchReason(string keyword)
        {
            var pReason = DataContext.Reasons.Where(fld => (fld.ReasonType.Contains(keyword)) || (fld.Value.Contains(keyword))).OrderBy(fld => fld.ReasonType).ToList();

            var mdlReasonList = new List<Model.mdlReason>();
            foreach (var reason in pReason)
            {
                var mdlReason = new Model.mdlReason();
                mdlReason.ReasonID = reason.ReasonID;
                mdlReason.ReasonType = reason.ReasonType;
                mdlReason.Value = reason.Value;

                mdlReasonList.Add(mdlReason);
            }

            return mdlReasonList;
        }

        public static Model.Reason GetReasonbyID(string lReasonID)
        {
            var pReason = DataContext.Reasons.FirstOrDefault(fld => fld.ReasonID.Equals(lReasonID));
            return pReason;
        }

        public static void DeleteReason(string lReasonID)
        {
            var pReason = GetReasonbyID(lReasonID);
            DataContext.Reasons.DeleteOnSubmit(pReason);
            DataContext.SubmitChanges();
        }

        public static void UpdateReason(string lReasonID, string lValue, string lReasonType)
        {
            var pReason = GetReasonbyID(lReasonID);
            pReason.Value = lValue;
            pReason.ReasonType = lReasonType;
            DataContext.SubmitChanges();
        }

        public static void InsertReason(string lReasonID, string lReasonName, string lReasonType)
        {
            Model.Reason pReason = new Model.Reason();
            pReason.Value = lReasonName;
            pReason.ReasonID = lReasonID;
            pReason.ReasonType = lReasonType;
            DataContext.Reasons.InsertOnSubmit(pReason);
            DataContext.SubmitChanges();
        }
        
    }
}
