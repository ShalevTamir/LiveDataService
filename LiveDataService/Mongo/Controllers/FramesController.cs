using LiveDataService.Mongo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LiveDataService.Mongo.Controllers
{
    [Authorize]
    [Route("[Controller]")]
    [ApiController]
    public class FramesController: Controller
    {
        private MongoFramesService _mongoFramesService;
        public FramesController(MongoFramesService mongoFramesService)
        {
            _mongoFramesService = mongoFramesService;
        }

        [HttpGet("count")]
        public async Task<ActionResult> CountFrames([Required] long MinTimeStamp, [Required] long MaxTimeStamp)
        {
            return Ok(JsonConvert.SerializeObject(new {
                Count = await _mongoFramesService.CountFrames(MinTimeStamp, MaxTimeStamp)
                }));
        }

        [HttpGet()]
        public async Task<ActionResult> GetFrames(
                                                  [Required]
                                                  long MinTimeStamp,
                                                  [Required]
                                                  long MaxTimeStamp,
                                                  [Required]
                                                  int MaxSamplesInPage,
                                                  [Required]
                                                  int PageNumber)
        {            
            return Ok(JsonConvert.SerializeObject(await _mongoFramesService.GetFrames(
                MinTimeStamp,
                MaxTimeStamp,
                MaxSamplesInPage,
                PageNumber
                )));   
        }
    }
}
