//using PecoPallet.Data.ServiceFactory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SampleTracking.Models;
using PecoPallet.Data.ServiceFactory;
using System.Configuration;

namespace SampleTracking.Controllers
{
    public class DataController : Controller
    {
        string connStr = ""; // System.Configuration.ConfigurationManager.ConnectionStrings["PecoDBConnection"].ConnectionString;

        [HttpGet]
        public JsonResult PieChartData()
        {

            //    public static List<PieChartData> GetPieChartData(string connStr)
            //{
            var pieData = new List<PieChartData>();

            //DbContext.DBContext.dbConnectionString = connStr;
            //var ds = DbContext.DBContext.GetDataSetFromQuery("select * from PicChartData");



            PieChartData pcdata = null;

            pcdata = new PieChartData
            {
                label = "Amazon",
                value = 55
            };
            pieData.Add(pcdata);

            pcdata = new PieChartData
            {
                label = "Staples",
                value = 10
            };
            pieData.Add(pcdata);


            pcdata = new PieChartData
            {
                label = "BestBuy",
                value = 25
            };
            pieData.Add(pcdata);


            pcdata = new PieChartData
            {
                label = "Walmart",
                value = 15
            };
            pieData.Add(pcdata);


            //create chart data object.
            var lst = pieData; // ChartDataFactory.GetPieChartData(connStr);

            //retun json data back to the caller
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDeviceManagerData()
        {
            //create davice manager data object.
            var lst = DeviceManagerFactory.GetDeviceManagerData(connStr);

            //retun json data back to the caller
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SavePushPinData(List<PushPin> pushpins)
        {
            string connStr = ConfigurationManager.ConnectionStrings["PecoDbConnection"].ConnectionString;
            return Json(new { data = PushPinsFactory.SavePushPinsData(pushpins, connStr) });
        }


        [HttpGet]
        public JsonResult GetPushPinDataByZip(int zipcode)
        {
            string connStr = ConfigurationManager.ConnectionStrings["PecoDbConnection"].ConnectionString;
            var lst = PushPinsFactory.GetPushPinsDatabyZip(zipcode, connStr);
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllPushPinsData()
        {
            string connStr = ConfigurationManager.ConnectionStrings["PecoDbConnection"].ConnectionString;
            var lst = PushPinsFactory.GetAllPushPinsData(connStr);
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SavePolygonData(List<Polygon> polygons)
        {
            string connStr = ConfigurationManager.ConnectionStrings["PecoDbConnection"].ConnectionString;
            return Json(new { data = PolygonFactory.SavePolygonData(polygons, connStr) });
        }
        
        [HttpGet]
        public JsonResult GetAllPolygons(int zipcode)
        {
            string connStr = ConfigurationManager.ConnectionStrings["PecoDbConnection"].ConnectionString;
            var lst = PolygonFactory.GetAllPolygonsByZipCode(zipcode,connStr);
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }

    }

}
