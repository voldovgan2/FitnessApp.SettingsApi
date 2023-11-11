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
        public async Task<SettingsContract> GetSettings([FromRoute] string userId)
        {
            if (nameof(SettingsController).Length > 0)
                return new SettingsContract { CanFollow = Enums.PrivacyType.Followers };

            var response = await _settingsService.GetSettingsByUserId(userId);
            return _mapper.Map<SettingsContract>(response);
        }

        [HttpPost("CreateSettings")]
        public async Task<SettingsContract> CreateSettings([FromBody]CreateSettingsContract contract)
        {
            var model = _mapper.Map<CreateSettingsGenericModel>(contract);
            var response = await _settingsService.CreateSettings(model);
            return _mapper.Map<SettingsContract>(response);
        }

        [HttpPut("UpdateSettings")]
        public async Task<SettingsContract> UpdateSettings([FromBody]UpdateSettingsContract contract)
        {
            var model = _mapper.Map<UpdateSettingsGenericModel>(contract);
            var response = await _settingsService.UpdateSettings(model);
            return _mapper.Map<SettingsContract>(response);
        }

        [HttpDelete("DeleteSettings/{userId}")]
        public async Task<string> DeleteSettings([FromRoute] string userId)
        {
            var response = await _settingsService.DeleteSettings(userId);
            return response;
        }
    }
}