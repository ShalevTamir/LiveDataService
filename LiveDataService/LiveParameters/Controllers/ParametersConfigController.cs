using LiveDataService.LiveParameters.Models;
using LiveDataService.LiveParameters.Models.Dtos;
using LiveDataService.LiveParameters.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveDataService.LiveParameters.Controllers
{
    [Route("parameters-config")]
    [ApiController]
    public class ParametersConfigController : Controller
    {
        private readonly IEnumerable<IcdParameter> _icdParameters;
        public ParametersConfigController(JsonUtilsService jsonUtilsService) 
        {
            _icdParameters = jsonUtilsService.DeserializeIcdFile();
        }

        [HttpPost("parameter-ranges")]
        public ActionResult GetSensorsRanges([FromBody] ParametersListDto parametersList)
        {
            return Ok(JsonConvert.SerializeObject(_icdParameters
                .Where((parameter) => parametersList.ParameterNames.Contains(parameter.ParameterName))
                .Select(parameter => new ParameterRangeDto() { 
                    ParameterName = parameter.ParameterName,
                    MinValue = parameter.MinValue,
                    MaxValue = parameter.MaxValue 
                })));
             
        }

        [HttpGet("parameter-names")]
        public ActionResult GetParameterNames()
        {
            return Ok(JsonConvert.SerializeObject(_icdParameters
                .Select((parameter) => parameter.ParameterName)
                ));
        }
    }
}
