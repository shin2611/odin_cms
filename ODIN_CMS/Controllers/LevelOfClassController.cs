using DataAccess.Factory;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ODIN_CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace ODIN_CMS.Controllers
{
    public class LevelOfClassController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IdentityHelper _identityHelper;
        private IOptions<AppsettingModel> appSettings;

        public LevelOfClassController(IConfiguration configuration, IdentityHelper identityHelper)
        {
            _configuration = configuration;
            _identityHelper = identityHelper;
        }


        [HttpPost]
        public async Task<IActionResult> InsertUpdateLevelOfClass(LevelOfClass levelOfClass)
        {
            try
            {

                var status = AbstractDAOFactory.Instance().CreateLevelOfClassDAO().InsertUpdateLevelOfClass(levelOfClass);
                NLogLogger.LogInfo("Status insert update levelOfClass:" + status);

                return Ok(new Response(status.Status, status.Message));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
