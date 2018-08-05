using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;
using System.Linq;

namespace Web.Controllers
{
    [Route("api/patient-groups/calculate")]
    public class PatientGroupsController : ControllerBase
    {
        readonly IPatientGroupsService patientGroupsService;

        public PatientGroupsController(IPatientGroupsService patientGroupsService)
        {
            this.patientGroupsService = patientGroupsService;
        }

        [HttpPost]
        public IActionResult Calculate(
            [FromBody]PatientGroupsForCalculateDto dto)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (!dto.Matrix.Any())
            {
                return Ok(ResultWith(0));
            }

            return Ok(ResultWith(patientGroupsService.Calculate(dto.MapToMatrix())));
        }

        [HttpOptions]
        public IActionResult PatientGroupsOptions(){
            Response.Headers.Add("Allow", "POST,OPTIONS");
            return Ok();
        }

        static object ResultWith(int numberOfGroups)
        {
            return new
            {
                NumberOfGroups = numberOfGroups
            };
        }
    }
}
