using Microsoft.AspNetCore.Mvc;
using SurveyForms.API.Controllers;
using SurveyForms.Application.FormAreas.Commands.CreateFormArea;
using SurveyForms.Application.FormAreas.Commands.EditFormArea;
using SurveyForms.Application.FormAreas.Models;
using SurveyForms.Application.FormAreas.Queries.GetUsersFormAreas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurveyForms.Clients.API.Controllers
{
    public class FormAreaController : BaseApiController
    {
        [HttpPut]
        public async Task<int> Put([FromBody] CreateFormAreaCommand model) => await Mediator.Send(model);

        [HttpPost]
        public async Task<int> Post([FromBody] EditFormAreaCommand model) => await Mediator.Send(model);

        [HttpGet]
        public async Task<IEnumerable<FormAreaDto>> Get() => await Mediator.Send(new GetUsersFormAreasQuery());
    }
}
