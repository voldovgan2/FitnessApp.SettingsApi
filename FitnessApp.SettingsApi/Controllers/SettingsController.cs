using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FitnessApp.SettingsApi.Contracts.Input;
using FitnessApp.SettingsApi.Contracts.Output;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Services.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.SettingsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]

    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;
        private readonly IMapper _mapper;

        public SettingsController(ISettingsService settingsService, IMapper mapper)
        {
            _settingsService = settingsService;
            _mapper = mapper;
        }

        [HttpGet("GetSettings/{userId}")]
        public async Task<IActionResult> GetSettings([FromRoute] string userId)
        {
            if (nameof(SettingsController).Length > 0)
                return Ok(new SettingsContract { CanFollow = Enums.PrivacyType.Followers });

            var response = await _settingsService.GetSettingsByUserId(userId);
            if (response != null)
            {
                var result = _mapper.Map<SettingsContract>(response);
                return Ok(result);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("CreateSettings")]
        public async Task<IActionResult> CreateSettings([FromBody]CreateSettingsContract contract)
        {
            var model = _mapper.Map<CreateSettingsGenericModel>(contract);
            var created = await _settingsService.CreateSettings(model);
            if (created != null)
            {
                var result = _mapper.Map<SettingsContract>(created);
                return Ok(result);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("UpdateSettings")]
        public async Task<IActionResult> UpdateSettings([FromBody]UpdateSettingsContract contract)
        {
            var model = _mapper.Map<UpdateSettingsGenericModel>(contract);
            var updated = await _settingsService.UpdateSettings(model);
            if (updated != null)
            {
                var result = _mapper.Map<SettingsContract>(updated);
                return Ok(result);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("DeleteSettings/{userId}")]
        public async Task<IActionResult> DeleteSettings([FromRoute] string userId)
        {
            var deleted = await _settingsService.DeleteSettings(userId);
            if (deleted != null)
                return Ok(deleted);
            else
                return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}