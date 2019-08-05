using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using msgraph_sdk_raptor_dashboard.Models;
using MsGraphSDKSnippetsCompiler;
using MsGraphSDKSnippetsCompiler.Data;
using MsGraphSDKSnippetsCompiler.Models;
using MsGraphSDKSnippetsCompiler.Services;

namespace msgraph_sdk_raptor_dashboard.Controllers
{
    public class HomeController : Controller
    {
        private static readonly IConfigurationRoot _configuration = AppSettings.Config();

        public IActionResult Index()
        {
            string dbConnectioSettings = GetSettingsValue("ConnectionStrings", "Raptor");
            CompileCycleData compileCycleData = new CompileCycleData(new RaptorDbContext(dbConnectioSettings));
            CompileCycle compileCycleResults = compileCycleData.GetAll().LastOrDefault();

            CompileResultsViewData compileResultsViewData = new CompileResultsViewData(new RaptorDbContext(dbConnectioSettings));
            IEnumerable<CompileResultsView> compileResultsViewResults = compileResultsViewData.GetAll();

            ViewBag.CompileResults = compileResultsViewResults;
            return View(compileCycleResults);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetSettingsValue(string section, string key)
        {
            string targetDirectoryPath = _configuration.GetSection(section).GetSection(key).Value;
            return targetDirectoryPath;
        }
    }
}