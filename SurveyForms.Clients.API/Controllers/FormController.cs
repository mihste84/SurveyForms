using Microsoft.AspNetCore.Mvc;
using SurveyForms.API.Controllers;
using SurveyForms.Application.Forms.Commands.AddNewForm;
using SurveyForms.Application.Forms.Commands.DeleteForm;
using SurveyForms.Application.Forms.Models;
using SurveyForms.Application.Forms.Queries.GetFormsById;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurveyForms.Clients.API.Controllers
{
    public class FormController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IEnumerable<FormDto>> Get(int? id) => await Mediator.Send(new GetFormsByIdQuery { FormAreaId = id });

        [HttpDelete("{id}")]
        public async Task<int> Delete(int? id) => await Mediator.Send(new DeleteFormCommand { FormId = id });

        [HttpPut]
        public async Task<int> Put([FromBody]AddNewFormCommand model) => await Mediator.Send(model);
    }
}
