using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GewProductivityAppService.Areas.QiWebApp.Models;
using GewProductivityAppService.DAL.GETNT62.GewPrdAppDB;
using GewProductivityAppService.DAL.MIS01.PDMDB;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace GewProductivityAppService.Areas.QiWebApp.Controllers
{
    public class YieldController : Controller
    {

        private PrdAppDbContext prdAppDb = new PrdAppDbContext();
        private PdmDbContext pdmDb = new PdmDbContext();

        // GET: QiWebApp/Yield
        public ActionResult Index(string codeno)
        {
            ViewBag.codeno = codeno;
            return View();
        }

        public ActionResult Yields_Read([DataSourceRequest] DataSourceRequest request, DateTime beginDate,
            DateTime endDate, string employeeNumber)
        {
            string empoName = prdAppDb.peAppWvUsers.Where(u => u.code.Equals(employeeNumber)).Select(u => u.name).FirstOrDefault();

            var rtnHlOutputs = pdmDb.hlOutputs.Where(h => h.Name.Equals(empoName) && h.InputTime > beginDate && h.InputTime < endDate).Select(h => new QiYieldQueryVm
                {
                    HL_No = h.HL_No,
                    @Class = h.Class,
                    Sys_Score = h.Sys_Score,
                    Dync_Score = h.Dync_Score,
                    InputTime = h.InputTime
                });
                return Json(rtnHlOutputs.OrderByDescending(o=>o.InputTime).ToDataSourceResult(request));
        }
    }
}