using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitnessApp.SettingsApi.Contracts.Input;
using FitnessApp.SettingsApi.Contracts.Output;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.Serializer.JsonMapper;
using FitnessApp.SettingsApi.Services.Settings;
using FitnessApp.SettingsApi.Data.Entities;
using FitnessApp.SettingsApi.Models.Output;

namespace FitnessApp.SettingsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ISettingsService<Settings, SettingsModel, GetUsersSettingsModel, CreateSettingsModel, UpdateSettingsModel> _settingsService;
        private readonly IJsonMapper _mapper;

        public SettingsController
        (
            ISettingsService<Settings, SettingsModel, GetUsersSettingsModel, CreateSettingsModel, UpdateSettingsModel> settingsService,
            IJsonMapper mapper
        )
        {
            _settingsService = settingsService;
            _mapper = mapper;
        }
               
        [HttpGet("GetSettings/{userId}")]       
        public async Task<IActionResult> GetSettingsAsync([FromRoute] string userId)
        {
            var response = await _settingsService.GetItemByUserIdAsync(userId);
            if (response != null)
            {
                var result = _mapper.Convert<SettingsContract>(response);
                return Ok(result);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
                
        [HttpPost("CreateSettings")]
        public async Task<IActionResult> CreateSettingsAsync([FromBody]CreateSettingsContract contract)
        {
            var model = _mapper.Convert<CreateSettingsModel>(contract); 
            var created = await _settingsService.CreateItemAsync(model);
            if (created != null)
            {
                var result = _mapper.Convert<SettingsContract>(created);
                return Ok(result);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
                
        [HttpPut("UpdateSettings")]
        public async Task<IActionResult> UpdateSettingsAsync([FromBody]UpdateSettingsContract contract)
        {
            var model = _mapper.Convert<UpdateSettingsModel>(contract);
            var updated = await _settingsService.UpdateItemAsync(model);
            if (updated != null)
            {
                var result = _mapper.Convert<SettingsContract>(updated);
                return Ok(result);                
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("DeleteSettings/{userId}")]
        public async Task<IActionResult> DeleteSettingsAsync([FromRoute] string userId)
        {
            var deleted = await _settingsService.DeleteItemAsync(userId);
            if (deleted != null)
            {                
                return Ok(deleted);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}