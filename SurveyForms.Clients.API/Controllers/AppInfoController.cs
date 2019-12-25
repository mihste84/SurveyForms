using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SurveyForms.API.Controllers;

namespace SurveyForms.Clients.API.Controllers
{
    public class AppInfoController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AppInfoController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Get() => Ok(new { 
            Environment = _webHostEnvironment.EnvironmentName
        });
    }
}
