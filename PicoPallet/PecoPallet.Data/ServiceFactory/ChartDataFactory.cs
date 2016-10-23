using PecoPallet.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PecoPallet.Data.ServiceFactory
{
    public class ChartDataFactory
    {
        public static List<PieChartData> GetPieChartData(string connStr)
        {
            var pieData = new List<PieChartData>();

            DbContext.DBContext.dbConnectionString = connStr;
            var ds = DbContext.DBContext.GetDataSetFromQuery("select * from PicChartData",connStr);
           

            if (ds.Tables.Count > 0)
            {
                PieChartData pcdata = null;
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    pcdata = new PieChartData
                    {
                        label = dr[0].ToString(),
                        value = int.Parse(dr[1].ToString())
                    };

                    pieData.Add(pcdata);
                }
            }

            return pieData;
        }
    }
}
